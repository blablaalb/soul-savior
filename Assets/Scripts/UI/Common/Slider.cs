using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using USlider = UnityEngine.UI.Slider;
using System.Linq;
using NaughtyAttributes;

namespace UI.Common
{
    public class Slider : MonoBehaviour
    {
        [Range(0, 1f)]
        [SerializeField]
        private float _value;
        private USlider _foreground;
        private USlider _background;
        [SerializeField]
        private float _fillBackgroundSpeed;
        [SerializeField]
        private float _fillForegroundSpeed;
        private float _backgroundLerpT;
        private float _foregroundLerpT;

        public float Value { get { return _value; } set { _value = value; } }

        internal void Awake()
        {
            _foreground = GetComponent<USlider>();
            _background = GetComponentsInChildren<USlider>().Where(x => x != _foreground).First();
        }

        internal void Update()
        {
            FillForeground();
            FillBackground();
        }

        private void FillBackground()
        {
            _background.value = Mathf.Lerp(_background.value, _value, _backgroundLerpT);
            _backgroundLerpT += _fillBackgroundSpeed * Time.deltaTime;
        }

        private void FillForeground()
        {
            _foreground.value = Mathf.Lerp(_foreground.value, _value, _foregroundLerpT);
            _foregroundLerpT += _fillForegroundSpeed * Time.deltaTime;
        }

    }

}
