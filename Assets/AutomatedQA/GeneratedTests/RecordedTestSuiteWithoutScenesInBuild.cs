#if UNITY_INCLUDE_TESTS
using System.Collections;
using System.Linq;
using NUnit.Framework;
using Unity.AutomatedQA;
using Unity.RecordedPlayback;
using Unity.RecordedTesting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

#endif

namespace GeneratedRecordedTests
{
    /// <summary>
    /// This class copied from <c>Unity.RecordedTesting.TestTools.RecordedTestSuite</c>.
    /// And modify for not need registered in "Scenes in Build" for scene loading.
    /// </summary>
    public abstract class RecordedTestSuiteWithoutScenesInBuild
    {
        [UnitySetUp]
        public virtual IEnumerator Setup()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            if (AutomatedQARuntimeSettings.hostPlatform == HostPlatform.Cloud &&
                AutomatedQARuntimeSettings.buildType == BuildType.UnityTestRunner)
            {
                RecordedTesting.SetupCloudUTFTests(TestContext.CurrentContext.Test.FullName);
            }
            else
            {
                RecordedTesting.SetupRecordedTest(TestContext.CurrentContext.Test.FullName);
            }

            // Load scene
            var recordingData = RecordedPlaybackPersistentData
                .GetRecordingData<RecordingInputModule.InputModuleRecordingData>();
            AsyncOperation loadSceneAsync = null;
#if UNITY_EDITOR
            // Use EditorSceneManager at run on Unity-editor
            var entryScenePath = AssetDatabase.FindAssets("t:SceneAsset", new string[] {"Assets"})
                .Select(AssetDatabase.GUIDToAssetPath)
                .FirstOrDefault(x => x.EndsWith($"{recordingData.entryScene}.unity"));
            loadSceneAsync = EditorSceneManager.LoadSceneAsyncInPlayMode(
                entryScenePath,
                new LoadSceneParameters(LoadSceneMode.Single));
#else
            // Use ITestPlayerBuildModifier to change the "Scenes in Build" list before run on Standalone-player
            // see: Editor/BuildAllScenesForTestPlayer.cs
            loadSceneAsync = SceneManager.LoadSceneAsync(recordingData.entryScene);
#endif
            while (!loadSceneAsync.isDone)
            {
                yield return null;
            }

            // Start playback
            RecordedPlaybackController.CreateInstance();
        }
    }
}
#endif
