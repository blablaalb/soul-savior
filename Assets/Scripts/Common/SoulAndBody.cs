using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class SoulAndBody : MonoBehaviour
    {
        private Soul _soul;
        [SerializeField]
        private GameObject _body;

        public Soul Soul => _soul ??= _soul = GetComponentInChildren<Soul>(true);
        public GameObject Body => _body;
    }
}