using System.Collections;
using System.Collections.Generic;
using PER.Common;
using UnityEngine;

namespace Managers
{

    public class AudioManager : Singleton<AudioManager>
    {
        private AudioSource _audSource;
        [SerializeField]
        private AudioClip[] _successAudClips;

        override protected void Awake()
        {
            _audSource = gameObject.AddComponent<AudioSource>();
            _audSource.playOnAwake = false;
            base.Awake();
        }

        public void Success()
        {
            var indx = Random.Range(0, _successAudClips.Length);
            var audClip = _successAudClips[indx];
            _audSource.PlayOneShot(audClip);
        }

    }
}
