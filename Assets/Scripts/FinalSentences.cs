using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalSentences : MonoBehaviour
{
    public Text text1;
    public Text text2;

    private int correct = 0;
    private int wrong = 0;

    private GameData data;

    private void Awake()
    {
        data = GameDataTransferer.gameData;
    }


    public void BackMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void Start()
    {

        Sentences();


        text1.text = "Correct Sentences: " +  correct;
        text2.text = "Wrong Sentences: " +  wrong;

    }

    private void Sentences()
    {
        var i = 0;

        foreach (var criminal in data.criminals)
        {
            if (data.sentences[i] >= criminal.criminalTemplate.crimeTemplate.minSentence && data.sentences[i] <= criminal.criminalTemplate.crimeTemplate.maxSentence)
            {
                correct++;
            }
            else
            {
                wrong++;
            }
        }
    }
}
