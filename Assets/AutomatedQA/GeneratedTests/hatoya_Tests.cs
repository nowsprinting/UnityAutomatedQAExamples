using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using Unity.AutomatedQA;
using Unity.CloudTesting;

namespace GeneratedAutomationTests
{
    /// <summary>
    /// These tests were generated by Unity Automated QA for the recording from the Assets folder at "/Recordings/hatoya.json".
    /// You can regenerate this file from the Unity Editor Menu: Automated QA > Generate Recorded Tests
    /// </summary>
    public class hatoya_Tests : AutomatedQATestsBase
    {
        /// Generated from recording file: "/Recordings/hatoya.json".
        [UnityTest]
        [CloudTest]
        public IEnumerator CanPlayToEnd()
        {
            yield return Driver.Perform.Action(hatoya_Steps.Actions["PRESS_Button_(4)"]); // Do a "press" action on "Canvas/Button (4)" in scene "Keypad".
            yield return Driver.Perform.Action(hatoya_Steps.Actions["RELEASE_Button_(4)"]); // Do a "release" action on "Canvas/Button (4)" in scene "Keypad".
            yield return Driver.Perform.Action(hatoya_Steps.Actions["PRESS_Button_(1)"]); // Do a "press" action on "Canvas/Button (1)" in scene "Keypad".
            yield return Driver.Perform.Action(hatoya_Steps.Actions["RELEASE_Button_(1)"]); // Do a "release" action on "Canvas/Button (1)" in scene "Keypad".
            yield return Driver.Perform.Action(hatoya_Steps.Actions["PRESS_Button_(2)"]); // Do a "press" action on "Canvas/Button (2)" in scene "Keypad".
            yield return Driver.Perform.Action(hatoya_Steps.Actions["RELEASE_Button_(2)"]); // Do a "release" action on "Canvas/Button (2)" in scene "Keypad".
            yield return Driver.Perform.Action(hatoya_Steps.Actions["PRESS_Button_(6)"]); // Do a "press" action on "Canvas/Button (6)" in scene "Keypad".
            yield return Driver.Perform.Action(hatoya_Steps.Actions["RELEASE_Button_(6)"]); // Do a "release" action on "Canvas/Button (6)" in scene "Keypad".
        }

        // Steps defined by recording.
        protected override void SetUpTestClass()
        {
            Driver.Perform.RegisterStep(hatoya_Steps.Actions["PRESS_Button_(4)"]);
            Driver.Perform.RegisterStep(hatoya_Steps.Actions["RELEASE_Button_(4)"]);
            Driver.Perform.RegisterStep(hatoya_Steps.Actions["PRESS_Button_(1)"]);
            Driver.Perform.RegisterStep(hatoya_Steps.Actions["RELEASE_Button_(1)"]);
            Driver.Perform.RegisterStep(hatoya_Steps.Actions["PRESS_Button_(2)"]);
            Driver.Perform.RegisterStep(hatoya_Steps.Actions["RELEASE_Button_(2)"]);
            Driver.Perform.RegisterStep(hatoya_Steps.Actions["PRESS_Button_(6)"]);
            Driver.Perform.RegisterStep(hatoya_Steps.Actions["RELEASE_Button_(6)"]);
        }

        // Initialize test run data.
        protected override void SetUpTestRun()
        {
            Test.entryScene = "Keypad";
            Test.recordedAspectRatio = new Vector2(878f,1068f);
            Test.recordedResolution = new Vector2(6016f,3384f);
        }
    }
}
