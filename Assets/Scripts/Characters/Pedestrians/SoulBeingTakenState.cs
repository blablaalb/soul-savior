using Common;
using PER.Common.FSM;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System;

namespace Characters.Pedestrians
{
    [System.Serializable]
    public class SoulBeingTakenState : IPedestrianState
    {
        public string StateName => "Soul Being Taken";

        private PedestrianAnimations _animations;
        private Transform _transform;
        private SoulAndBody _soulAndBody;


        public void Initialize(PedestrianAnimations animations, SoulAndBody soulAndBody, Transform transform)
        {
            _animations = animations;
            _soulAndBody = soulAndBody;
            _transform = transform;
        }

        public void Enter()
        {
            _animations.SoulBeingTaken();
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