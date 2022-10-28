using System.Collections.Generic;
using UnityEngine;

namespace Ccmmon.Souls
{
    [System.Serializable]
    public class ReturnToStackState : ISoulState
    {
        private StackMember _stackMember;

        public string StateName => "Return to Stack";

        public void Initialize(StackMember stackMember)
        {
            _stackMember = stackMember;
        }

        public async void Enter()
        {
            await _stackMember.MoveToPlace();
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