using System.Collections;
using System.Collections.Generic;
using PER.Common.FSM;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Common;

namespace Characters.Pedestrians
{
    public class Pedestrian : Context<IState>
    {
        [SerializeField]
        private MoveAtPlayerState _moveAtPlayerState;
        [SerializeField]
        private IdleState _idleState;
        [SerializeField]
        private SoulBeingTakenState _soulBeingTakenState;
        private Player _player;
        private SoulAndBody _soulAndBody;

        public Soul Soul => _soulAndBody.Soul;

        internal void Awake()
        {
            var _modelInstantiator = GetComponentInChildren<ModelInstantiator>();
            var model = _modelInstantiator.Random();
            model.Soul.SetActive(false);
            _soulAndBody = GetComponentInChildren<SoulAndBody>();
            _player = FindObjectOfType<Player>();
            var animations = GetComponentInChildren<PedestrianAnimations>();
            _moveAtPlayerState.Initialize(this.transform, _player, animations);
            _idleState.Initialize(animations);
            _soulBeingTakenState.Initialize(animations, _soulAndBody, this.transform);
        }

        internal void Start()
        {
            Idle();
        }

        public void Idle()
        {
            if (_currentState != _idleState) EnterState(_idleState);
        }

        public void MoveAtPlayer()
        {
            if (_currentState != _moveAtPlayerState) EnterState(_moveAtPlayerState);
        }

        public void SoulBeingTaken()
        {
            if (_currentState != _soulBeingTakenState) EnterState(_soulBeingTakenState);
        }

    }
}
