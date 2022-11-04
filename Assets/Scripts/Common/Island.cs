using System.Collections;
using System.Collections.Generic;
using Characters.Pedestrians;
using UnityEngine;

public class Island : MonoBehaviour
{
    [SerializeField]
    public Transform _pedestriansRoot;

    public Transform IslandJumpPosition;
    public Transform IslandLandPosition;
    public Transform WizardJumpPosition;
    public Transform WizardLandPosition;

    public int PedestrianCount { get; private set; }
    public int PedestrianSoulsMatched { get; set; }
    public static Island Active { get; private set; }

    internal void Awake()
    {
        PedestrianCount = _pedestriansRoot.GetComponentsInChildren<Pedestrian>().Length;
    }

    public void Activate()
    {
        Active?.Deactivate();
        _pedestriansRoot.gameObject.SetActive(true);
        Island.Active = this;
    }

    public void Deactivate()
    {
        _pedestriansRoot.gameObject.SetActive(false);
    }
}
