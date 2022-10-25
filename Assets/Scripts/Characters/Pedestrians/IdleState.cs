using PER.Common.FSM;
using UnityEngine;

namespace Characters.Pedestrians
{
    [System.Serializable]
    public class IdleState : IState
    {
        private PedestrianAnimations _animations;

        public string StateName => "Idle";

        public void Initialize(PedestrianAnimations animations)
        {
            _animations = animations;
        }

        public void Enter()
        {
            _animations.Idle();
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