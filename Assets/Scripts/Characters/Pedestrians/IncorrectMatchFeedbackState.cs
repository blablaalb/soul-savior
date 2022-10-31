using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

namespace Characters.Pedestrians
{
    [Serializable]
    public class IncorrectMatchFeedbackState : IPedestrianState
    {
        [SerializeField]
        private float _duration;
        private PedestrianAnimations _animations;
        private Pedestrian _pedestrian;
        public string StateName => "Incorrect match";

        public void Initialize(PedestrianAnimations animations, Pedestrian pedestrian)
        {
            _pedestrian = pedestrian;
            _animations = animations;
        }

        public async void Enter()
        {
            _animations.IncorrectMatchFeedback();
            await UniTask.Delay(TimeSpan.FromSeconds(_duration));
            _pedestrian.MoveAtPlayer();
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