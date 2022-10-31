using System.Collections;
using System.Collections.Generic;
using PER.Common.FSM;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Common;
using Common.Souls;

namespace Characters.Pedestrians
{
    public class Pedestrian : Context<IPedestrianState>
    {
        [SerializeField]
        private MoveAtPlayerState _moveAtPlayerState;
        [SerializeField]
        private IdleState _idleState;
        [SerializeField]
        private SoulBeingTakenState _soulBeingTakenState;
        [SerializeField]
        private SoulReturnedState _soulReturnedState;
        [SerializeField]
        private IncorrectMatchFeedbackState _incorrectMatchFeedbackState;
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
            _soulReturnedState.Initialize(animations);
            _soulBeingTakenState.Initialize(animations, _soulAndBody, this.transform);
            _incorrectMatchFeedbackState.Initialize(animations, this);
        }

        internal void Start()
        {
            Idle();
        }

        public void IncorrectMatchFeedbackState()
        {
            if (_currentState != _incorrectMatchFeedbackState) EnterState(_incorrectMatchFeedbackState);
        }

        public void SoulReturned()
        {
            if (_currentState != _soulReturnedState) EnterState(_soulReturnedState);
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
