using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public struct GameData
{
    public List<CriminalBehaviour> criminals;
    public List<int> sentences;
}

public class GameManager : Singleton<GameManager>
{
    //Private vars
    

    private CriminalTemplate[] criminalTemplates;
    private CrimeTemplate[] crimeTemplates;

    private float verdictTimer = 0f;
    private float gameCurrentTimer = 0f;

    private float score = 0f;



    //REFERENCES

    [HideInInspector] public CriminalBehaviour currentCriminal = null;
    public GameRulesTemplate rulesTemplate;
    public AudioManager audioManager;
    public GameObject criminalPrefab;
    public GameObject criminalAnchor;
    public GameObject ui;
    public TMP_Text uiTimer;
    public Image uiCriminalTimer;
    public Animation gavelAnim;
    public GameObject slam;
    public GameObject guiltiness;
    public Sprite[] guilts;

    [HideInInspector] public GuiltOMeter guiltMeter;
    public Button notGuilty;


    [HideInInspector] public GameData gameData;
    //Main Methods

    protected override void Awake()
    {
        base.Awake();

        UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);

        InitVars();

        gameData = new GameData();
        gameData.criminals = new List<CriminalBehaviour>();
        gameData.sentences = new List<int>();

        SpawnCriminal();
    }

    private void Update()
    {
        if (gameCurrentTimer > 0f)
        {
            gameCurrentTimer -= Time.deltaTime;

            uiTimer.text = Mathf.FloorToInt(gameCurrentTimer).ToString();

            if (Mathf.FloorToInt(gameCurrentTimer) < 5 && !oneTimer)
            {
                oneTimer = true;
                audioManager.PlaySound(6);
            }
        }
        else
        {
            gameData.criminals.Remove(currentCriminal);
            GameDataTransferer.gameData = gameData;
            audioManager.PlaySound(7);

            StartCoroutine(DelayCO(1f));
        }
    }
    bool oneTimer = false;
    
    IEnumerator DelayCO(float delay)
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(4);
    }

    //Custom Methods

    public void SpawnCriminal()
    {
        //currentCriminal = Instantiate(criminalPrefab, criminalAnchor.transform).GetComponent<CriminalBehaviour>();



        int n = Random.Range(0, rulesTemplate.criminalPrefabs.Length);
        int m = Random.Range(0, crimeTemplates.Length);

        currentCriminal = Instantiate(rulesTemplate.criminalPrefabs[n], criminalAnchor.transform).GetComponent<CriminalBehaviour>();

        currentCriminal.criminalTemplate = criminalTemplates[n];

        currentCriminal.criminalTemplate.crimeTemplate = crimeTemplates[m];

        gameData.criminals.Add(currentCriminal);
    }



    public void UpdatePoints(float points)
    {
        score += points;
    }


    public void UpdateCriminalTimer(float timer)
    {
        var rend = uiCriminalTimer;

        rend.material.SetFloat("_Threshold", timer);
    }

    public void GetVerdict(float points, bool guilty)
    {
        if (guilty)
        {
           gameData.sentences.Add(Mathf.FloorToInt(points));

            if (points >= currentCriminal.criminalTemplate.crimeTemplate.minSentence && points <= currentCriminal.criminalTemplate.crimeTemplate.maxSentence)
            {
                UpdatePoints(rulesTemplate.guiltyPointsCorrect);
            }
            else
            {
                UpdatePoints(rulesTemplate.guiltyPointsWrong);
            }
        }
        else
        {
            gameData.sentences.Add(-1);

            UpdatePoints(rulesTemplate.notGuiltyPoints);
        }
        
    }
    
    private void InitVars()
    {
        gameCurrentTimer = rulesTemplate.gameTimer;
        verdictTimer = rulesTemplate.verdictTimer;
        criminalTemplates = rulesTemplate.criminalTemplates;
        crimeTemplates = rulesTemplate.crimeTemplates;
        guiltMeter = ui.GetComponentInChildren<GuiltOMeter>();
    }


    public void UnClick()
    {
        guiltMeter.UnClick();
    }
}
