using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Managers;
using TMPro;
using UnityEngine;

namespace UI.Levels.Screens
{
    public class LevelCompleted : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _levelText;


        internal void Start()
        {
            GameManager.Instance.Won += OnLevelWon;
            Close();
        }

        internal void OnDestroy()
        {
            try
            {
                GameManager.Instance.Won -= OnLevelWon;
            }
            catch { }
        }

        private async void OnLevelWon()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(3));
            Open();
        }

        public void Open()
        {
            gameObject.SetActive(true);
            _levelText.SetText($"LEVEL {LevelManager.Instance.Level}\nCOMPLETED");
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}