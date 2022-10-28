using Common.Souls;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Common.Souls
{
    public class MovingInsideStackState : StackMember, ISoulState
    {
        private Soul _soul;

        public string StateName => "Moving Inside Stack";

        internal void Awake()
        {
            _soul = GetComponent<Soul>();
        }

        public override async UniTask MoveToPlace(Ease ease = Ease.OutQuart)
        {
            if (_soul.CurrentState.StateName == "Inside Stack")
                _soul.MoveToPLaceInStack();
            else if (_soul.CurrentState.StateName == "Soul Being Added to Stack" || _soul.CurrentState.StateName == "Return to Stack")
                await base.MoveToPlace();
        }

        public async void Enter()
        {
            await base.MoveToPlace();
            _soul.InsideStack();
        }

        public void OnUpdate()
        {
        }

        public void OnFixedUpdate()
        {
        }

        public void Exit()
        {
            base.StopMoving();
        }

    }
}