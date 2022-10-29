using System.Collections;
using System.Collections.Generic;
using PER.Common;
using UnityEngine;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        override protected void Awake()
        {
            #if UNITY_EDITOR
            Application.runInBackground = true;
            #endif
            base.Awake();
        }
    }
}
