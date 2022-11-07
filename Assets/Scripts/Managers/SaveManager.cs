using PER.Common;
using UnityEngine;

namespace Managers
{
    public class SaveManager : PersistentSingleton<SaveManager>
    {
        private string _wonLevelsKey = "won_levels";

        public void UpdateWonLevels(int level)
        {
            PlayerPrefs.SetInt(_wonLevelsKey, level);
        }

        public int GetWonLevels()
        {
            return PlayerPrefs.GetInt(_wonLevelsKey, 1);
        }

    }
}