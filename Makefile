PROJECT_HOME?=$(PWD)
UNITY_VERSION?=$(shell grep 'm_EditorVersion:' $(PROJECT_HOME)/ProjectSettings/ProjectVersion.txt | grep -o -E '\d{4}\.[1-4]\.\d+[abfp]\d+')
UNITY?=/Applications/Unity/HUB/Editor/$(UNITY_VERSION)/Unity.app/Contents/MacOS/Unity
BUILD_DIR?=$(PROJECT_HOME)/Build
LOG_DIR?=$(PROJECT_HOME)/Logs

# Code Coverage report filter
# see: https://docs.unity3d.com/Packages/com.unity.testtools.codecoverage@0.2/manual/UsingCodeCoverage.html#using-code-coverage-in-batchmode
COVERAGE_ASSEMBLY_FILTERS?=+Assembly-CSharp,-*Tests

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
    -debugCodeOptimization \
    -enableCodeCoverage \
    -coverageResultsPath $(LOG_DIR) \
    -coverageOptions 'enableCyclomaticComplexity;assemblyFilters:$(COVERAGE_ASSEMBLY_FILTERS)'
endef

define cover_report
  mkdir -p $(LOG_DIR)
  $(UNITY) \
    -projectPath $(PROJECT_HOME) \
    -batchmode \
    -debugCodeOptimization \
    -enableCodeCoverage \
    -coverageResultsPath $(LOG_DIR) \
    -coverageOptions 'generateHtmlReport;generateBadgeReport' \
    -quit
endef

.PHONY: clean
clean:
	rm -rf $(BUILD_DIR)
	rm -rf $(LOG_DIR)

.PHONY: test
test:
	$(call cover,playmode)

.PHONY: cover_report
cover_report:
	$(call cover_report)

.PHONY: cover
cover: test cover_report
