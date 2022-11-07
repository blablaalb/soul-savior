using Managers;
using UnityEngine;
using UI.Common;

namespace UI.Levels
{
    public class ProgressBar : MonoBehaviour
    {
        private Slider _slider;

        internal void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        internal void LateUpdate()
        {
            _slider.Value = LevelManager.Instance.Progress;
        }


    }
}