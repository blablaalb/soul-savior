using UnityEngine;
using System.Threading.Tasks;
using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;
using Common;

namespace Characters.Wizards
{
    [System.Serializable]
    public class GoToPortalState : MoveState
    {
        public override string StateName => "Go to Portal";

        public override void Enter()
        {
            Position = GameObject.FindObjectOfType<Portal>().transform.position;
            base.Enter();
        }
    }
}