// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UGUIExample.Keypad
{
    [RequireComponent(typeof(Button))]
    public class TransitionButton : MonoBehaviour
    {
        [SerializeField]
        private string transitionSceneName;

        private void Start()
        {
            var button = gameObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(transitionSceneName, LoadSceneMode.Single);
            });

            var text = gameObject.GetComponentInChildren<Text>();
            text.text = $"Go {transitionSceneName}";
        }
    }
}
