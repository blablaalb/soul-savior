using System.Collections;
using System.Collections.Generic;
using PER.Common.FSM;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Characters.Pedestrians
{
    public class Pedestrian : Context<IState>
    {
        [SerializeField]
        private MoveAtPlayerState _moveAtPlayerState;
        [SerializeField]
        private IdleState _idleState;
        private Player _player;


        internal void Awake()
        {
            var _modelInstantiator = GetComponentInChildren<ModelInstantiator>();
            var model = _modelInstantiator.Random();
            model.Soul.SetActive(false);
            _player = FindObjectOfType<Player>();
            var animations = GetComponentInChildren<PedestrianAnimations>();
            _moveAtPlayerState.Initialize(this.transform, _player, animations);
            _idleState.Initialize(animations);
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
    }
}
