PROJECT_HOME?=$(PWD)
BUILD_DIR?=$(PROJECT_HOME)/Build
LOG_DIR?=$(PROJECT_HOME)/Logs
UNITY_VERSION?=$(shell grep 'm_EditorVersion:' $(PROJECT_HOME)/ProjectSettings/ProjectVersion.txt | grep -o -E '\d{4}\.[1-4]\.\d+[abfp]\d+')

# macOS
UNITY_HOME=/Applications/Unity/HUB/Editor/$(UNITY_VERSION)/Unity.app/Contents
UNITY?=$(UNITY_HOME)/MacOS/Unity
UNITY_YAML_MERGE?=$(UNITY_HOME)/Tools/UnityYAMLMerge
STANDALONE_PLAYER=StandaloneOSX

# Code Coverage report filter (comma separated)
# see: https://docs.unity3d.com/Packages/com.unity.testtools.codecoverage@1.1/manual/CoverageBatchmode.html
COVERAGE_ASSEMBLY_FILTERS?=+<user>,-*Tests

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
    -coverageOptions 'generateAdditionalMetrics;generateHtmlReport;assemblyFilters:$(COVERAGE_ASSEMBLY_FILTERS)'
endef

.PHONY: clean
clean:
	rm -rf $(BUILD_DIR)
	rm -rf $(LOG_DIR)

.PHONY: test
test:
	$(call cover,playmode)

.PHONY: test_standalone_player
test_standalone_player:
	$(call test,$(STANDALONE_PLAYER))
