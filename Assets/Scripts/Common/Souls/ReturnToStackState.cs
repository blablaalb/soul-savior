using System.Collections.Generic;
using UnityEngine;

namespace Ccmmon.Souls
{
    [System.Serializable]
    public class ReturnToStackState : ISoulState
    {
        private StackMember _stackMember;
        private Soul _soul;

        public string StateName => "Return to Stack";

        public void Initialize(StackMember stackMember)
        {
            _stackMember = stackMember;
            _soul = _stackMember.GetComponent<Soul>();
        }

        public async void Enter()
        {
            await _stackMember.MoveToPlace();
            _soul.InsideStack();
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