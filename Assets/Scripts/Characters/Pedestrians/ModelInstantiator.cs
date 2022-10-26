using System;
using System.Collections.Generic;
using Common;
using UnityEngine;
using System.Linq;


namespace Characters.Pedestrians
{
    public class ModelInstantiator : MonoBehaviour
    {
        [SerializeField]
        private Transform _modelsRoot;

        public SoulAndBody Random()
        {
            SoulAndBody[] models = _modelsRoot.GetComponentsInChildren<SoulAndBody>(true).ToArray();
            if (models == null || models.Length == 0) throw new IndexOutOfRangeException("No pedestrian models left");
            var indx = UnityEngine.Random.Range(0, models.Length);
            var model = models[indx];
            model.transform.SetParent(this.transform);
            model.transform.localPosition = Vector3.zero;
            model.transform.rotation = Quaternion.identity;
            model.gameObject.SetActive(true);
            return model;
        }

    }

}