using System.Collections;
using System.Linq;
using NUnit.Framework;
using Unity.AutomatedQA;
using Unity.CloudTesting;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

namespace GeneratedAutomationTests
{
    /// <summary>
    /// These tests were generated by Unity Automated QA for the recording from the Assets folder at "/Recordings/Keypad2013.json".
    /// You can regenerate this file from the Unity Editor Menu: Automated QA > Generate Recorded Tests
    /// </summary>
    public class FullTestsWithPageObjects_Keypad2013 : AutomatedQATestsBase
    {
        /// Generated from recording file: "/Recordings/Keypad2013.json".
        [UnityTest]
        [CloudTest]
        public IEnumerator CanPlayToEnd()
        {
            yield return Driver.Perform.Click(Scene_Keypad_PageObject.Clickable_Keypad_Button2);
            yield return Driver.Perform.Click(Scene_Keypad_PageObject.Clickable_Keypad_Button0);
            yield return Driver.Perform.Click(Scene_Keypad_PageObject.Clickable_Keypad_Button1);
            yield return Driver.Perform.Click(Scene_Keypad_PageObject.Clickable_Keypad_Button3);

            // Verify
            var historyText = Object.FindObjectsOfType<Text>().FirstOrDefault(x => x.name.Equals("HistoryText"));
            Assert.That(historyText, Is.Not.Null);
            Assert.That(historyText.text, Is.EqualTo("2013"));
        }

        // Initialize test run data.
        protected override void SetUpTestRun()
        {
            Test.entryScene = Scene_Keypad_PageObject.SceneName;
            Test.recordedAspectRatio = new Vector2(770f, 1626f);
            Test.recordedResolution = new Vector2(6016f, 3384f);
        }
    }
}
