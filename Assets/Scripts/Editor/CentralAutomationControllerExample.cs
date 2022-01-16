// Copyright (c) 2021-2022 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections.Generic;
using Unity.AutomatedQA;
using Unity.RecordedPlayback;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace Editor
{
    public static class CentralAutomationControllerExample
    {
        [MenuItem("Automated QA Examples/Automated Run/Run GameCrawlerExample.asset")]
        private static void RunGameCrawlerExample()
        {
            Assert.IsTrue(EditorApplication.isPlaying, "Required playmode");
            // TODO: Enter playmode and run

            var automatedRun =
                AssetDatabase.LoadAssetAtPath<AutomatedRun>("Assets/AutomatedRun/GameCrawlerExample.asset");
            CentralAutomationController.Instance.Run(automatedRun.config);
        }

        [MenuItem("Automated QA Examples/Automated Run/Playback without config")]
        private static void PlaybackWithoutConfig()
        {
            Assert.IsTrue(EditorApplication.isPlaying, "Required playmode");
            // TODO: Enter playmode and run

            var json = AssetDatabase.LoadAssetAtPath<TextAsset>("Assets/Recordings/Keypad2013.json");
            var config = new AutomatedRun.RunConfig();
            config.automators.AddRange(new List<AutomatorConfig>
            {
                new RecordedPlaybackAutomatorConfig() { recordingFile = json },
            });
            CentralAutomationController.Instance.Run(config);
        }
    }
}
