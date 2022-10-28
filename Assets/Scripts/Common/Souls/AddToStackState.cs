using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;

namespace Ccmmon.Souls
{
    [System.Serializable]
    public class AddToStackState : ISoulState
    {
        private StackMember _stackMember;
        private Transform _transform;
        [SerializeField]
        private float _elevetationHeight;
        [SerializeField]
        private float _elevationTime;
        private Transform _initialParent;
        private CancellationTokenSource _cts;
        private Soul _soul;

        public string StateName => "Soul Being Added to Stack";

        public void Initialize(Transform transform)
        {
            _transform = transform;
            _stackMember = _transform.GetComponent<StackMember>();
            _soul = _stackMember.GetComponent<Soul>();
        }

        public async void Enter()
        {
            _initialParent = _transform.parent;
            _transform.gameObject.SetActive(true);
            _cts = new CancellationTokenSource();
            _transform.parent = null;
            await _transform.DOMoveY(_elevetationHeight, _elevationTime).WithCancellation(_cts.Token);
            await _stackMember.AddSelf(true);
            _soul.InsideStack();
        }

        public void Exit()
        {
            _cts?.Cancel();
        }

        public void OnFixedUpdate()
        {
        }

        public void OnUpdate()
        {
        }
    }
}