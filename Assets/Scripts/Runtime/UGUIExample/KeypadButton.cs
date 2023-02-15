// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UGUIExample
{
    [RequireComponent(typeof(Button))]
    public class KeypadButton : MonoBehaviour
    {
        [SerializeField]
        private int number = 0;

        private Text _historyText;

        private void Start()
        {
            _historyText = FindObjectsOfType<Text>().FirstOrDefault(x => x.name.Equals("HistoryText"));

            var button = gameObject.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                Debug.Log($"Tap {number}");
                if (_historyText)
                {
                    _historyText.text = $"{_historyText.text}{number}";
                }
            });

            var text = gameObject.GetComponentInChildren<Text>();
            text.text = $"{number}";
        }
    }
}
