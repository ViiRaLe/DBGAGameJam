using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CrimeTemplate", menuName = "HammerOfJustice/Crime", order = 3)]
public class CrimeTemplate : ScriptableObject
{
    public Sprite[] tips;

    [Range(1,100)]public int minSentence = 1;
    [Range(1,100)]public int maxSentence = 100;
}
