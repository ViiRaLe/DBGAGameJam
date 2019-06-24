using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameRulesTemplate", menuName = "HammerOfJustice/GameRules", order = 1)]
public class GameRulesTemplate : ScriptableObject
{

    [Header("Game Rules")]

    public float gameTimer = 60f;
    public float verdictTimer = 5f;
    public float respawnDelay = 0.5f;
    public float vibrationDelay = 0.2f;

    [Header("Point Assignement")]

    public float notGuiltyPoints = -0.5f;
    public float guiltyPointsCorrect = +1f;
    public float guiltyPointsWrong = -1f;


    [Header("Criminals & Charges")]

    public GameObject[] criminalPrefabs;
    public CriminalTemplate[] criminalTemplates;
    public CrimeTemplate[] crimeTemplates;


    [Header("Guilt-O-Meter vars")]

    public float guiltoMeterPrecharge = 0.5f;
    
    public float guiltMeterPerTick = 5f;
    public float guiltoMeterMaxSentence = 100f;
    
    public float[] guiltMeterMult;
}
