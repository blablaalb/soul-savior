using System.Linq;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections.Generic;
using Excessives.LinqE;
using Managers;

namespace Characters.Pedestrians
{
    [System.Serializable]
    public class SoulReturnedState : IPedestrianState
    {
        private PedestrianAnimations _animations;
        private Transform _transform;
        private Material[] _materials;
        private Collider[] _colliders;
        [SerializeField]
        private float _delayBeforeDisappearance;

        public string StateName => "Soul Returned";

        public void Initialize(PedestrianAnimations animations, Transform transform)
        {
            _animations = animations;
            _transform = transform;
            _materials = _transform.GetComponentsInChildren<SkinnedMeshRenderer>().SelectMany(x => x.sharedMaterials).ToArray();
            _colliders = _transform.GetComponentsInChildren<Collider>();
        }

        public async void Enter()
        {
            DeactivateColliders();
            _animations.Dance1();
            await UniTask.Delay(TimeSpan.FromSeconds(_delayBeforeDisappearance));
            await Fadeout();
            _transform.gameObject.SetActive(false);
            RestoreFade();
            LevelManager.Instance.OnSoulReturned();
        }

        private void DeactivateColliders()
        {
            _colliders.ForEach(c => c.isTrigger = true);
        }

        private async UniTask Fadeout()
        {
            var tasks = new List<UniTask>();
            foreach (var m in _materials)
            {
                var task = m.DOFade(0f, "_Color", 2f).ToUniTask();
                tasks.Add(task);
            }
            await UniTask.WhenAll(tasks);
        }

        private void RestoreFade()
        {
            foreach (var m in _materials)
            {
                m.SetColor("_Color", Color.white);
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