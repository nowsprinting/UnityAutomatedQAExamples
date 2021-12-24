// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UGUIExample.Keypad
{
    [RequireComponent(typeof(Text))]
    public class InputDisplay : MonoBehaviour
    {
        private readonly string[] _triggers = new[] { "Fire1", "Fire2", "Fire3" };
        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
        }

        private void Update()
        {
            var hAxis = RecordableInput.GetAxis("Horizontal");
            if (hAxis < 0)
            {
                _text.text = "A";
                return;
            }
            else if (hAxis > 0)
            {
                _text.text = "D";
                return;
            }

            var vAxis = RecordableInput.GetAxis("Vertical");
            if (vAxis < 0)
            {
                _text.text = "S";
                return;
            }
            else if (vAxis > 0)
            {
                _text.text = "W";
                return;
            }

            foreach (var key in _triggers)
            {
                if (RecordableInput.GetButton(key))
                {
                    _text.text = key;
                    return;
                }
            }

            _text.text = "";
        }
    }
}
