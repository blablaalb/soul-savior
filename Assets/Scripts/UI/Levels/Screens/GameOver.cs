using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using TMPro;

namespace UI.Levels.Screens
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _levelText;


        internal void Start()
        {
            GameManager.Instance.Lost += Open;
            Close();
        }

        internal void OnDestroy()
        {
            try
            {
                GameManager.Instance.Lost -= Open;
            }
            catch { }
        }

        public void Open()
        {
            gameObject.SetActive(true);
            _levelText.SetText($"LEVEL {LevelManager.Instance.Level}\nFAILED");
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
