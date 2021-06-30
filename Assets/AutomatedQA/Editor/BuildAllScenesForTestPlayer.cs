// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using System.Collections.Generic;
using System.Linq;
using AutomatedQA.Editor;
using UnityEditor;
using UnityEditor.TestTools;

[assembly: TestPlayerBuildModifier(typeof(BuildAllScenesForTestPlayer))]

namespace AutomatedQA.Editor
{
    /// <summary>
    /// Change "Scenes in Build" list for the run tests on Standalone-player.
    /// Build all scenes that in under the Assets/ directory.
    /// 
    /// Required: Unity Test Framework 1.1.13+
    /// see: https://forum.unity.com/threads/testplayerbuildmodifier-not-working.844447/
    /// </summary>
    public class BuildAllScenesForTestPlayer : ITestPlayerBuildModifier
    {
        public BuildPlayerOptions ModifyOptions(BuildPlayerOptions playerOptions)
        {
            var scenes = AssetDatabase.FindAssets("t:SceneAsset", new string[] {"Assets"})
                .Select(AssetDatabase.GUIDToAssetPath).ToArray();

            var buildSceneList = new List<string>(playerOptions.scenes);
            foreach (var s in scenes)
            {
                if (!buildSceneList.Contains(s))
                {
                    buildSceneList.Add(s);
                }
            }
            // Only missing scenes are added to the array.
            // If change the scene order in the `playerOptions.scenes` array, the test may not start.

            playerOptions.scenes = buildSceneList.ToArray();
            return playerOptions;
        }
    }
}
