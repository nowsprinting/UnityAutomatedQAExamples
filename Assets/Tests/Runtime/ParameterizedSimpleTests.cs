// Copyright (c) 2021-2023 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GeneratedAutomationTests;
using NUnit.Framework;
using Unity.RecordedPlayback;
using Unity.RecordedTesting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace Tests.Runtime
{
    /// <summary>
    /// Automated QAパッケージの *Simple Test*（JSONファイルを読むタイプ）をパラメタライズドテスト対応にしたサンプル
    ///
    /// 実装のポイント
    /// - Async Testにすることでパラメチャライズドテストを可能に
    /// - RecordedTestSuite基底クラスおよびRecordedTest属性は使用せず、テストメソッド内でSceneロードを行なうことで、擬似乱数シード値を固定可能に
    /// - Test Report に名前付きでレポートを残すようにReportingManager.CurrentTestNameを設定
    /// </summary>
    [TestFixture]
    public class ParameterizedSimpleTests : AutomatedTestSuite
    {
        [UnitySetUp]
        public override IEnumerator Setup()
        {
            yield return base.Setup();

            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            // Automated QAのTest Reportに名前付きでレポートを残す
            ReportingManager.CurrentTestName =
                $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}";
            ReportingManager.InitializeDataForNewTest();
        }

        [TestCase("Assets/Recordings/Keypad1997.json", "1997")]
        [TestCase("Assets/Recordings/Keypad2013.json", "2013")]
        public async Task ParameterizedPlayback(string recordingJsonPath, string expectedHistoryText)
        {
            await SceneManager.LoadSceneAsync("Keypad");
            // Note: ランダム要素のあるSceneの場合、ここで擬似乱数シード値を固定できる

            await Playback(recordingJsonPath);

            // Verify
            var historyText = Object.FindObjectsOfType<Text>().FirstOrDefault(x => x.name.Equals("HistoryText"));
            Assert.That(historyText, Is.Not.Null);
            Assert.That(historyText.text, Is.EqualTo(expectedHistoryText));
        }

        private static async Task Playback(string recordingJsonPath)
        {
            // RecordedTestSuite.Setup に実装されている初期化をSceneロード後に実行
            RecordedPlaybackController.Instance.Reset();
            RecordedPlaybackPersistentData.SetRecordingMode(RecordingMode.Playback);
            RecordedPlaybackPersistentData.SetRecordingData(File.ReadAllText(Path.GetFullPath(recordingJsonPath)));
            RecordedPlaybackController.Instance.Begin();
            while (!RecordedPlaybackController.Exists() || !RecordedPlaybackController.Instance.IsInitialized())
            {
                await UniTask.NextFrame();
            }

            // Playback
            await RecordedTesting.TestPlayToEnd();
        }
    }
}
