using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class SoulAndBody : MonoBehaviour
    {
        [SerializeField]
        private GameObject _soul;
        [SerializeField]
        private GameObject _body;

        public GameObject Soul => _soul;
        public GameObject Body => _body;
    }
}