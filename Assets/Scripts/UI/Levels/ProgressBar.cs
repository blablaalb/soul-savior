using Managers;
using UnityEngine;
using UI.Common;
using TMPro;

namespace UI.Levels
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _currentLvlText;
        [SerializeField]
        private TextMeshProUGUI _nextLvlText;
        private Slider _slider;

        internal void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        internal void Start()
        {
            var currentLvl = LevelManager.Instance.Level;
            var nextLvl = currentLvl + 1;
            _currentLvlText.SetText($"{currentLvl}");
            _nextLvlText.SetText($"{nextLvl}");
        }

        internal void LateUpdate()
        {
            _slider.Value = LevelManager.Instance.Progress;
        }


    }
}