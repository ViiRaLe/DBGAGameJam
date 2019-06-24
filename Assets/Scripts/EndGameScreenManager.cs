using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameScreenManager : MonoBehaviour
{
    public GameObject criminalTemplate;

    public GameObject group;

    public GameObject scrollContent;

    public GameData gameData;

    private void Start()
    {
        gameData = GameDataTransferer.gameData;
        Populate();
    }

    public void Populate()
    {
        var i = 0;

        var c = scrollContent.GetComponent<RectTransform>();
        
        //c.sizeDelta = new Vector2(c.sizeDelta.x, c.sizeDelta.y + cart.bounds.size.y * gameData.criminals.Count);


        foreach (var criminal in gameData.criminals)
        {
            var a = Instantiate(criminalTemplate).GetComponent<CriminalEndTemplate>();
            a.transform.SetParent(group.transform, false);
            var b = a.GetComponent<RectTransform>();


            
            b.localPosition = new Vector3(b.localPosition.x, b.localPosition.y + 130 * i, b.localPosition.z);


            //0 +b.sizeDelta.y + (c.sizeDelta.y / gameData.criminals.Count) * 5
            //   b.localPosition.y + 100 - (120 * i)

            a.photo = gameData.criminals[i].criminalTemplate.sprite;
            a.tip1 = gameData.criminals[i].criminalTemplate.crimeTemplate.tips[0];
            a.tip2 = gameData.criminals[i].criminalTemplate.crimeTemplate.tips[1];
            a.tip3 = gameData.criminals[i].criminalTemplate.crimeTemplate.tips[2];
            a.crime = gameData.criminals[i].criminalTemplate.crimeTemplate.name + " " + gameData.criminals[i].criminalTemplate.crimeTemplate.minSentence + " - " + gameData.criminals[i].criminalTemplate.crimeTemplate.maxSentence;
            a.sentence = gameData.sentences[i];

            i++;            
        }
    }
}
