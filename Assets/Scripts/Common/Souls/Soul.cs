using System;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading;
using Common;
using PER.Common.FSM;
using Characters.Pedestrians;
using Excessives;
using Excessives.LinqE;
using Excessives.Unity;

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
        [SerializeField]
        private DisappearState _disappearState;
        private Pedestrian _pedestrian;
        [SerializeField]
        private float _matchDistanceThreshold;
        private Collider _collider;


        internal void Awake()
        {
            _collider = GetComponent<Collider>();
            _body = GetComponentInParent<SoulAndBody>().Body.transform;
            _stackMember = GetComponent<StackMember>();
            _addToStackState.Initialize(this.transform);
            _follorTouchState.Initialize(this, _body);
            _returnToStackState.Initialize(_stackMember);
            _movingInsideStackState = GetComponent<MovingInsideStackState>();
            _pedestrian = GetComponentInParent<Pedestrian>();
            _disappearState.Initialize(this.transform);
        }


        public void MoveToPLaceInStack()
        {
#pragma warning disable CS0252
            if (_currentState != _movingInsideStackState)
                EnterState(_movingInsideStackState);
#pragma warning restore CS0252
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

        public void Disappear()
        {
            if (_currentState != _disappearState) EnterState(_disappearState);
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public void OnDragEnded()
        {
            if (SoulMatched())
            {
                _pedestrian.SoulReturned();
                Disappear();
            }
            else
            {
                ReturnToStack();
                if (OverlappingPedestrian() is Pedestrian p)
                {
                    p.IncorrectMatchFeedback();
                }
            }
        }

        private bool SoulMatched()
        {
            var distance = Vector3.Distance(_pedestrian.transform.position, transform.position);
            return distance <= _matchDistanceThreshold;
        }

        private Pedestrian OverlappingPedestrian()
        {
            var pedestrians = FindObjectsOfType<Pedestrian>().Where(p => p != _pedestrian);
            foreach (var p in pedestrians)
            {
                if (_collider.bounds.Contains(p.transform.position))
                {
                    return p;
                }
            }
            return null;
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