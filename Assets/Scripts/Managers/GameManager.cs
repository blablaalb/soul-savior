using System;
using System.Collections;
using System.Collections.Generic;
using PER.Common;
using UnityEngine;

namespace Managers
{
    public class GameManager : PersistentSingleton<GameManager>
    {
        public Action Lost;
        public Action Won;

        override protected void Awake()
        {
#if UNITY_EDITOR
            Application.runInBackground = true;
#endif
            base.Awake();
        }

        public void OnLost()
        {
            Lost?.Invoke();
        }

        public void OnWon()
        {
            var level = SaveManager.Instance.GetWonLevels();
            SaveManager.Instance.UpdateWonLevels(++level);
            Won?.Invoke();
        }

    }
}
