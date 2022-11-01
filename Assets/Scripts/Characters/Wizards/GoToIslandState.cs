using UnityEngine;
using System.Threading.Tasks;
using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;

namespace Characters.Wizards
{
    [System.Serializable]
    public class GoToIslandState : MoveState
    {
        private WizardAnimations _animations;
        private Wizard _wizard;
        [SerializeField]
        private float _jumpTime;

        public Vector3 JumpFromPosition { get; set; }
        public Vector3 JumpToPosition { get; set; }
        public Vector3 WizardPosition { get; set; }

        public override string StateName => "Go to Island";

        public void Initialize(Transform transform, Wizard wizard, WizardAnimations animations)
        {
            base.Initialize(transform);
            _wizard = wizard;
            _animations = animations;
        }

        public override async void Enter()
        {
            _cts = new CancellationTokenSource();
            Position = JumpFromPosition;
            await Rotate();
            _animations.Move();
            await Move();
            Position = JumpToPosition;
            await Jump();
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            _wizard.TakeAvailableSouls();
        }

        private async UniTask Jump()
        {
            _animations.Jump();
            await _transform.DOJump(Position, jumpPower: 5f,numJumps: 1, _jumpTime).WithCancellation(_cts.Token);
            _animations.Land();
        }

        public override void Exit()
        {
        }

        public override void OnFixedUpdate()
        {
        }

        public override void OnUpdate()
        {
        }

    }
}