using UnityEngine;
using DG.Tweening;
using Characters.Pedestrians;
using Cysharp.Threading.Tasks;
using System;

namespace Characters.Wizards
{
    [System.Serializable]
    public class TakeSoulState : IWizardState
    {
        [SerializeField]
        private float _rotateTime;
        [SerializeField]
        private float _soulElevationDelay;
        [SerializeField]
        private float _soulMovingToStackDelay;
        private Wizard _wizard;
        private Transform _transform;
        private WizardAnimations _animations;

        public string StateName => "Take Soul";
        public Pedestrian Pedestrian { get; set; }

        public void Initialize(Wizard wizard, WizardAnimations animation, Transform transform)
        {
            _wizard = wizard;
            _animations = animation;
            _transform = transform;
        }


        public async void Enter()
        {
            _animations.TakingSoulBegan += OnTakingSoulBegan;
            var rotationDirection = _transform.position - new Vector3(Pedestrian.transform.position.x, _transform.position.y, Pedestrian.transform.position.z);
            await _transform.DORotate(Quaternion.LookRotation(rotationDirection).eulerAngles, _rotateTime);
            _animations.TakeSoul();
        }

        private async void OnTakingSoulBegan()
        {
            Pedestrian.SoulBeingTaken();
            await UniTask.Delay(TimeSpan.FromSeconds(_soulElevationDelay));
            var soul = Pedestrian.Soul;
            _wizard.OnSoulTaken();
            soul.AddToStack();
            _wizard.Idle();
        }

        public void Exit()
        {
            _animations.TakingSoulBegan -= OnTakingSoulBegan;
        }

        public void OnFixedUpdate()
        {
        }

        public void OnUpdate()
        {
        }

    }
}