using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using PER.Common;
using UnityEngine;
using Characters.Wizards;

namespace Managers
{
    public class LevelManager : Singleton<LevelManager>
    {
        [SerializeField]
        private Island[] _islands;
        private int _returnedSouls;
        private Wizard _wizard;
        private Player _player;

        override protected void Awake()
        {
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
        private void NextIsland()
        {
            var nextIslandIndx = Array.IndexOf(_islands, Island.Active) + 1;
            if (nextIslandIndx < _islands.Length)
            {
                var nextIsland = _islands[nextIslandIndx];
                nextIsland.Activate();
                _wizard.GoToIsland(nextIsland);
                _player.GoToIsland(nextIsland);
            }
        }

        private void OnIslandPassed()
        {
            _returnedSouls = 0;
            NextIsland();
        }

        public void OnSoulReturned()
        {
            _returnedSouls++;
            if (Island.Active.PedestrianCount <= _returnedSouls)
            {
                OnIslandPassed();
            }
        }

    }
}