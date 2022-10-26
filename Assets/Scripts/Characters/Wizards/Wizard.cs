using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Characters.Pedestrians;
using Cysharp.Threading.Tasks;
using Cysharp.Threading;
using System;
using PER.Common.FSM;

namespace Characters.Wizards
{
    public class Wizard : Context<IWizardState>
    {
        [SerializeField]
        private float _startTakingSoulsDelay;
        private List<Pedestrian> _pedestrians;
        private Player _player;
        private WizardAnimations _animations;
        [SerializeField]
        private TakeSoulState _takeSoulState;
        [SerializeField]
        private IdleState _idleState;

        public int TotalSould { get; private set; }
        public int TakenSouls { get; private set; }


        internal void Awake()
        {
            _player = FindObjectOfType<Player>();
            _animations = GetComponentInChildren<WizardAnimations>();
            _pedestrians = FindObjectsOfType<Pedestrian>().ToList();
            TotalSould = _pedestrians.Count;
            _takeSoulState.Initialize(this, _animations, this.transform);
            _idleState.Initialize(_animations, this, _player);
        }

        internal void Start()
        {
            Idle();
        }

        public void TakeNextSoul()
        {
            if (_pedestrians.Count > 0)
            {
                var index = _pedestrians.Count - 1;
                var pedestrian = _pedestrians[index];
                _pedestrians.RemoveAt(index);
                TakeSoul(pedestrian);
            }
        }

        public void TakeSoul(Pedestrian pedestrian)
        {
            _takeSoulState.Pedestrian = pedestrian;
            if (_currentState != _takeSoulState) EnterState(_takeSoulState);
        }

        public void Idle()
        {
            if (_currentState != _idleState) EnterState(_idleState);
        }

        public void OnSoulTaken()
        {
            TakenSouls++;
        }

    }

}
