# Copyright (c) 2021 Koji Hasegawa.
# This software is released under the MIT License.

PROJECT_HOME?=$(PWD)
BUILD_DIR?=$(PROJECT_HOME)/Build
LOG_DIR?=$(PROJECT_HOME)/Logs
UNITY_VERSION?=$(shell grep 'm_EditorVersion:' $(PROJECT_HOME)/ProjectSettings/ProjectVersion.txt | grep -o -E '\d{4}\.[1-4]\.\d+[abfp]\d+')

UNAME := $(shell uname)
ifeq ($(UNAME), Darwin)
# macOS
UNITY_HOME=/Applications/Unity/HUB/Editor/$(UNITY_VERSION)/Unity.app/Contents
UNITY?=$(UNITY_HOME)/MacOS/Unity
UNITY_YAML_MERGE?=$(UNITY_HOME)/Tools/UnityYAMLMerge
STANDALONE_PLAYER=StandaloneOSX
else
ifeq ($(UNAME), Linux)
# Linux: not test yet
UNITY_HOME=$HOME/Unity/Hub/Editor/<version>
UNITY?=$(UNITY_HOME)/Unity
UNITY_YAML_MERGE?=$(UNITY_HOME)/ # unknown
STANDALONE_PLAYER=StandaloneLinux64
else
# Windows: not test yet
UNITY_HOME=C:\Program Files\Unity\Hub\Editor\$(UNITY_VERSION)\Editor
UNITY?=$(UNITY_HOME)\Unity.exe
UNITY_YAML_MERGE?=$(UNITY_HOME)\Data\Tools\UnityYAMLMerge.exe
STANDALONE_PLAYER=StandaloneWindows64
endif
endif

# Code Coverage report filter (comma separated)
# see: https://docs.unity3d.com/Packages/com.unity.testtools.codecoverage@1.1/manual/CoverageBatchmode.html
COVERAGE_ASSEMBLY_FILTERS?=+<user>,-*.Tests,-GeneratedTests

define test_arguments
  -projectPath $(PROJECT_HOME) \
  -batchmode \
  -nographics \
  -silent-crashes \
  -stackTraceLogType Full \
  -runTests \
  -testPlatform $(TEST_PLATFORM) \
  -testResults $(LOG_DIR)/test_$(TEST_PLATFORM)_results.xml \
  -logFile $(LOG_DIR)/test_$(TEST_PLATFORM).log
endef

define test
  $(eval TEST_PLATFORM=$1)
  $(eval TEST_ARGUMENTS=$(call test_arguments))
  mkdir -p $(LOG_DIR)
  $(UNITY) \
    $(TEST_ARGUMENTS)
endef

define cover
  $(eval TEST_PLATFORM=$1)
  $(eval TEST_ARGUMENTS=$(call test_arguments))
  mkdir -p $(LOG_DIR)
  $(UNITY) \
    $(TEST_ARGUMENTS) \
    -burst-disable-compilation \
    -debugCodeOptimization \
    -enableCodeCoverage \
    -coverageResultsPath $(LOG_DIR) \
    -coverageOptions 'generateAdditionalMetrics;assemblyFilters:$(COVERAGE_ASSEMBLY_FILTERS)'
endef

define cover_report
  mkdir -p $(LOG_DIR)
  $(UNITY) \
    -projectPath $(PROJECT_HOME) \
    -batchmode \
    -quit \
    -enableCodeCoverage \
    -coverageResultsPath $(LOG_DIR) \
    -coverageOptions 'generateAdditionalMetrics;generateHtmlReport;assemblyFilters:+<project>'
endef

.PHONY: usage
usage:
	@echo "Tasks:"
	@echo "  open_editor: Open this project in Unity editor."
	@echo "  apply_unityyamlmerge: Apply UnityYAMLMerge as mergetool in .git/config."
	@echo "  clean: Clean /Build and /Logs directories."
	@echo "  test_editmode: Run Edit Mode tests."
	@echo "  test_playmode: Run Play Mode tests."
	@echo "  cover_report: Create code coverage HTML report."
	@echo "  test: Run test_editmode, test_playmode, and cover_report. Recommended to use with '-k' option."
	@echo "  test_standalone_player: Run Play Mode tests on standalone player."
	@echo "  test_android: Run Play Mode tests on Android device."
	@echo "  test_ios: Run Play Mode tests on iOS device."

.PHONY: open_editor
open_editor:
	$(UNITY) -projectPath $(PROJECT_HOME) -logFile $(LOG_DIR)/editor.log &

# Apply UnityYAMLMerge as mergetool in .git/config
.PHONY: apply_unityyamlmerge
apply_unityyamlmerge:
	git config --local merge.tool "unityyamlmerge"
	git config --local mergetool.unityyamlmerge.trustExitCode false
	git config --local mergetool.unityyamlmerge.cmd '$(UNITY_YAML_MERGE) merge -p "$$BASE" "$$REMOTE" "$$LOCAL" "$$MERGED"'

.PHONY: clean
clean:
	rm -rf $(BUILD_DIR)
	rm -rf $(LOG_DIR)

.PHONY: test_editmode
test_editmode:
	$(call cover,editmode)

.PHONY: test_playmode
test_playmode:
	$(call cover,playmode)

.PHONY: cover_report
cover_report:
	$(call cover_report)

# Run Edit Mode and Play Mode tests with coverage and html-report by code coverage package
# このターゲットを`-k`オプション付きで実行すれば、もしEdit/ Play Modeテストがfailしても
# Htmlレポート生成まで実行した上でエラーを示すexit codeが返されます
.PHONY: test
test: test_editmode test_playmode cover_report

# Run Play Mode tests on standalone player
# Code Coverage packageはプレイヤー実行に対応していないのでテストのみ
.PHONY: test_standalone_player
test_standalone_player:
	$(call test,$(STANDALONE_PLAYER))

# Run Play Mode tests on Android device
# 端末はUSB接続され、adbコマンドで操作可能であること
# Code Coverage packageはプレイヤー実行に対応していないのでテストのみ
.PHONY: test_android
test_android:
	$(call test,Android)

# Run Play Mode tests on iOS device
# 実行するとXcodeが起動し、USB接続された端末上にインストール・実行されます
# Code Coverage packageはプレイヤー実行に対応していないのでテストのみ
.PHONY: test_ios
test_ios:
	$(call test,iOS)
