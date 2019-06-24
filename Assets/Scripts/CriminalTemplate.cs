using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;

[CreateAssetMenu(fileName = "CriminalTemplate", menuName = "HammerOfJustice/Criminal", order = 2)]
public class CriminalTemplate : ScriptableObject
{
    public Sprite sprite;
    

    [HideInInspector] public CrimeTemplate crimeTemplate;
}
