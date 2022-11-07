using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using PER.Common;
using UnityEngine;
using Characters.Wizards;
using Characters.Players;
using Common.Souls;
using Characters.Pedestrians;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

namespace Managers
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField]
        private Island[] _islands;
        private int _returnedSouls;
        private Wizard _wizard;
        private Player _player;
        private int _totalSouls;

        public float Progress => (float)_returnedSouls / (float)_totalSouls;
        public int Level {get; private set;}

        override protected void Awake()
        {
            Level = SaveManager.Instance.GetWonLevels();
            _totalSouls = FindObjectsOfType<Pedestrian>(true).Length;
            _wizard = FindObjectOfType<Wizard>();
            _player = FindObjectOfType<Player>();
            base.Awake();
        }

        internal void Start()
        {
            FirstIsland();
        }

        private void FirstIsland()
        {
            var island = _islands[0];
            island.Activate();
        }

        [NaughtyAttributes.Button]
        private bool NextIsland()
        {
            var nextIslandIndx = Array.IndexOf(_islands, Island.Active) + 1;
            if (nextIslandIndx < _islands.Length)
            {
                var nextIsland = _islands[nextIslandIndx];
                nextIsland.Activate();
                _wizard.GoToIsland(nextIsland);
                _player.GoToIsland(nextIsland);
                return true;
            }
            _wizard.GoToPortal();
            return false;
        }

        private void OnIslandPassed()
        {
            if (!NextIsland()) GameManager.Instance.OnWon();
        }

        public void OnSoulReturned()
        {
            _returnedSouls++;
            Island.Active.PedestrianSoulsMatched++;
            if (Island.Active.PedestrianCount <= Island.Active.PedestrianSoulsMatched)
            {
                OnIslandPassed();
            }
        }

        public async void Restart()
        {
            var scene = SceneManager.GetActiveScene();
            await SceneManager.LoadSceneAsync(scene.buildIndex);
        }

    }
}