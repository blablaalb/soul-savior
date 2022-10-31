using UnityEngine;

namespace Characters.Pedestrians
{
    [System.Serializable]
    public class SoulReturnedState : IPedestrianState
    {
        private PedestrianAnimations _animations;

        public string StateName => "Soul Returned";

        public void Initialize(PedestrianAnimations animations)
        {
            _animations = animations;
        }

        public void Enter()
        {
            _animations.Dance1();
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