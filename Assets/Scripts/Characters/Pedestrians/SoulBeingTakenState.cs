using Common;
using PER.Common.FSM;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System;

namespace Characters.Pedestrians
{
    [System.Serializable]
    public class SoulBeingTakenState : IState
    {
        public string StateName => "Soul Being Taken";

        private PedestrianAnimations _animations;
        private Transform _transform;
        private SoulAndBody _soulAndBody;
        [SerializeField]
        private float _soulUpPosition;


        public void Initialize(PedestrianAnimations animations, SoulAndBody soulAndBody, Transform transform)
        {
            _animations = animations;
            _soulAndBody = soulAndBody;
            _transform = transform;
        }

        public async void Enter()
        {
            _animations.SoulBeingTaken();
            await UniTask.Delay(TimeSpan.FromSeconds(2f));
            var soul = _soulAndBody.Soul;
            soul.SetActive(true);
            soul.Move(_transform.position + _transform.up * _soulUpPosition);
        }

        public void Exit()
        {
        }

        public void OnFixedUpdate()
        {
        }

        public void OnUpdate()
        {

        }
    }
}