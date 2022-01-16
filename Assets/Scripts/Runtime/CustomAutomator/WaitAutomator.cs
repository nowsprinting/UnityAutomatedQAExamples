// Copyright (c) 2021-2022 Koji Hasegawa.
// This software is released under the MIT License.

using System;
using System.Collections;
using Unity.AutomatedQA;
using UnityEngine;

namespace CustomAutomator
{
    [Serializable]
    public class WaitAutomatorConfig : AutomatorConfig<WaitAutomator>
    {
        public int waitSeconds = 1;
    }

    public class WaitAutomator : Automator<WaitAutomatorConfig>
    {
        public override void BeginAutomation()
        {
            base.BeginAutomation();
            StartCoroutine(Wait());
        }

        private IEnumerator Wait()
        {
            Debug.Log("Begin wait");
            yield return new WaitForSeconds(config.waitSeconds);
            Debug.Log("End wait");
            EndAutomation();
        }
    }
}
