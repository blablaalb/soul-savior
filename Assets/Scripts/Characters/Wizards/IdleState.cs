using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;

namespace Characters.Wizards
{
    [System.Serializable]
    public class IdleState : IWizardState
    {
        private WizardAnimations _animations;
        private Wizard _wizard;
        [SerializeField]
        private float _idleTime;
        [SerializeField]
        private float _rotateTime;
        private Player _player;
        private Transform _transform;

        public string StateName => "Idle";

        public void Initialize(WizardAnimations animations, Wizard wizard, Player player)
        {
            _transform = wizard.transform;
            _animations = animations;
            _wizard = wizard;
            _player = player;
        }

        public async void Enter()
        {
            _animations.Idle();
            var direction = new Vector3(_player.transform.position.x, _transform.position.y, _player.transform.position.z) - _transform.position;
            await _transform.DORotate(Quaternion.LookRotation(direction).eulerAngles, _rotateTime);
            await UniTask.Delay(TimeSpan.FromSeconds(_idleTime));
            if (_wizard.TakenSouls < _wizard.TotalSould)
            {
                _wizard.TakeNextSoul();
            }
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