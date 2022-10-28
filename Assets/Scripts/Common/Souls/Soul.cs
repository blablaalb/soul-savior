using System;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading;
using Common;
using PER.Common.FSM;

namespace Common.Souls
{
    public class Soul : Context<ISoulState>
    {
        private Transform _body;
        private StackMember _stackMember;
        [SerializeField]
        private AddToStackState _addToStackState;
        [SerializeField]
        private FollowTouchState _follorTouchState;
        [SerializeField]
        private ReturnToStackState _returnToStackState;
        [SerializeField]
        private InsideStackState _insideStackState;
        private MovingInsideStackState _movingInsideStackState;


        internal void Awake()
        {
            _body = GetComponentInParent<SoulAndBody>().Body.transform;
            _stackMember = GetComponent<StackMember>();
            _addToStackState.Initialize(this.transform);
            _follorTouchState.Initialize(this, _body);
            _returnToStackState.Initialize(_stackMember);
            _movingInsideStackState = GetComponent<MovingInsideStackState>();
        }

        public void MoveToPLaceInStack()
        {
            if (_currentState != _movingInsideStackState)
                EnterState(_movingInsideStackState);
        }

        public void AddToStack()
        {
            if (_currentState != _addToStackState) EnterState(_addToStackState);
        }

        public void FollowTouch(Vector2 direction)
        {
            _follorTouchState.DirectionNormalized = direction;
            if (_currentState != _follorTouchState)
                if (_currentState != _addToStackState)
                    EnterState(_follorTouchState);
        }

        public void ReturnToStack()
        {
            if (_currentState != _returnToStackState) EnterState(_returnToStackState);
        }

        public void InsideStack()
        {
            if (_currentState != _insideStackState) EnterState(_insideStackState);
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

#if UNITY_EDITOR
        [NaughtyAttributes.Button]
        void PrintCurrentState()
        {
            Debug.Log(CurrentState.StateName, this);
        }
#endif

    }
}