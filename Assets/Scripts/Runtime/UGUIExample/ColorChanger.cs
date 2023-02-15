// Copyright (c) 2021 Koji Hasegawa.
// This software is released under the MIT License.

using UnityEngine;
using UnityEngine.EventSystems;

namespace UGUIExample
{
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(Renderer))]
    public class ColorChanger : MonoBehaviour, IPointerClickHandler
    {
        private const float ColorChangePerSec = 0.2f;
        private Material _material;
        private bool _toggle = false;

        private void Awake()
        {
            _material = GetComponent<Renderer>().material;
            _material.color = Color.HSVToRGB(0f, 0.8f, 1f);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _toggle = !_toggle;
        }

        private void Update()
        {
            if (_toggle) Gaming();
        }

        private void Gaming()
        {
            Color.RGBToHSV(_material.color, out var hue, out var saturation, out var value);
            hue += ColorChangePerSec * Time.deltaTime;
            if (hue >= 1f)
            {
                hue = 0;
            }

            _material.color = Color.HSVToRGB(hue, saturation, value);
        }
    }
}
