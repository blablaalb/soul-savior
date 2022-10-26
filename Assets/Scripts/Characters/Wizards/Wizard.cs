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

        internal void Start()
        {
            // Idle();
            var pedestrian = _pedestrians.First();
            Debug.Log(pedestrian.gameObject.name, pedestrian);
            TakeSoul(pedestrian);
        }

        public void TakeSoul(Pedestrian pedestrian)
        {
            _takeSoulState.Pedestrian = pedestrian;
            if (_currentState != _takeSoulState) EnterState(_takeSoulState);
        }

        public void TakeAllSouls()
        {

        }

        public void Idle()
        {
            if (_currentState != _idleState) EnterState(_idleState);
        }

        internal void Awake()
        {
            _animations = GetComponentInChildren<WizardAnimations>();
            _pedestrians = FindObjectsOfType<Pedestrian>();
            _takeSoulState.Initialize(this, _animations, this.transform);
        }



        private Pedestrian[] _pedestrians;
        private WizardAnimations _animations;
        [SerializeField]
        private TakeSoulState _takeSoulState;
        [SerializeField]
        private IdleState _idleState;

    }

}
