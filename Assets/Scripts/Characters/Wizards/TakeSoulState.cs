using UnityEngine;
using DG.Tweening;
using Characters.Pedestrians;
using Cysharp.Threading.Tasks;
using System;
using System.Linq;
using System.Collections.Generic;
using Excessives.LinqE;

namespace Characters.Wizards
{
    [System.Serializable]
    public class TakeSoulState : IWizardState
    {
        [SerializeField]
        private float _rotateTime;
        [SerializeField]
        private float _soulElevationDelay;
        private Wizard _wizard;
        private Transform _transform;
        private WizardAnimations _animations;

        public string StateName => "Take Soul";
        public ICollection<Pedestrian> Pedestrians { get; set; }

        public void Initialize(Wizard wizard, WizardAnimations animation, Transform transform)
        {
            _wizard = wizard;
            _animations = animation;
            _transform = transform;
        }


        public Vector3 CenterOfVectors(Vector3[] vectors)
        {
            Vector3 sum = Vector3.zero;
            if (vectors == null || vectors.Length == 0)
            {
                return sum;
            }

            foreach (Vector3 vec in vectors)
            {
                sum += vec;
            }
            return sum / vectors.Length;
        }

        public async void Enter()
        {
            var center = CenterOfVectors(Pedestrians.Select(x => x.transform.position).ToArray());
            var rotationDirection = _transform.position - new Vector3(center.x, _transform.position.y, center.z);
            await _transform.DORotate(Quaternion.LookRotation(rotationDirection).eulerAngles, _rotateTime);
            _animations.TakingSoulBegan += OnTakingSoulBegan;
            _animations.TakeSoul();
        }

        private async void OnTakingSoulBegan()
        {
            Pedestrians.ForEach(p => p.SoulBeingTaken());
            await UniTask.Delay(TimeSpan.FromSeconds(_soulElevationDelay));
            foreach (var p in Pedestrians)
            {
                var soul = p.Soul;
                _wizard.OnSoulTaken();
                soul.AddToStack();
                _animations.TakingSoulBegan -= OnTakingSoulBegan;
            }
            await UniTask.WaitUntil(() =>
            {
                return Pedestrians.All(p => p.CurrentState.StateName != "");
            });
            _wizard.Idle();
        }

        public void Exit()
        {
            _animations.TakingSoulBegan -= OnTakingSoulBegan;
            Pedestrians = null;
        }

        public void OnFixedUpdate()
        {
        }

        public void OnUpdate()
        {
        }

    }
}