using UnityEngine;
using System.Linq;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using Excessives.LinqE;

namespace Common.Souls
{
    [System.Serializable]
    public class DisappearState : ISoulState
    {
        private Material[] _materials;
        private Transform _transform;

        public string StateName => "Disappear";

        public void Initialize(Transform transform)
        {
            _transform = transform;
            _materials = transform.GetComponentsInChildren<SkinnedMeshRenderer>().SelectMany(x => x.sharedMaterials).ToArray();
        }

        public async void Enter()
        {
            var tasks = new List<UniTask>();
            foreach (var m in _materials)
            {
                tasks.Add(m.DOColor(new Color(0, 0, 0, 0), "_TintColor",  2f).ToUniTask());
            }

            await UniTask.WhenAll(tasks);
            _transform.gameObject.SetActive(false);
            _materials.ForEach(m =>  m.SetColor("_TintColor", Color.white));

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