using UnityEngine;
using System.Threading.Tasks;
using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;

namespace Characters.Wizards
{
    [System.Serializable]
    public class MoveState : IWizardState
    {
        protected Transform _transform;
        [SerializeField]
        private float _rotateTime;
        [SerializeField]
        private float _moveTime;
        protected CancellationTokenSource _cts;

        public Vector3 Position { get; set; }
        public virtual string StateName => "Move";

        public void Initialize(Transform transform)
        {
            _transform = transform;
        }

        public virtual async void Enter()
        {
            _cts = new CancellationTokenSource();
            await Rotate();
            await Move();
        }

        /// <summary>
        /// Rotates ignoring the Y axis
        /// </summary>
        /// <returns></returns>
        protected async UniTask Rotate()
        {
            var from = _transform.position;
            var to = Position;
            to.y = from.y;
            var direction = to - from;
            await _transform.DORotateQuaternion(Quaternion.LookRotation(direction), _rotateTime).WithCancellation(_cts.Token);
        }

        protected async UniTask Move()
        {
            await _transform.DOMove(Position, _moveTime).WithCancellation(_cts.Token);
        }

        public virtual void Exit()
        {
        }

        public virtual void OnFixedUpdate()
        {
        }

        public virtual void OnUpdate()
        {
        }
    }

}