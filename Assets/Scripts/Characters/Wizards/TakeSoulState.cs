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
        public string StateName => "Take Soul";
        public Pedestrian Pedestrian { get; set; }


        public void Enter()
        {
            _animations.TakingSoulBegan += OnTakingSoulBegan;
            var direction = _transform.position - new Vector3(Pedestrian.transform.position.x, _transform.position.y, Pedestrian.transform.position.z);
            _transform.DORotate(Quaternion.LookRotation(direction).eulerAngles, _rotateTime).OnComplete(() =>
            {
                _animations.TakeSoul();
            });
        }

        private async void OnTakingSoulBegan()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_soulElevationDelay));
            Pedestrian.SoulBeingTaken();
            var soul = Pedestrian.Soul;
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

        public void Initialize(Wizard wizard, WizardAnimations animation, Transform transform)
        {
            _wizard = wizard;
            _animations = animation;
            _transform = transform;
        }



        [SerializeField]
        private float _rotateTime;
        [SerializeField]
        private float _soulElevationDelay;
        private Wizard _wizard;
        private Transform _transform;
        private WizardAnimations _animations;

    }
}