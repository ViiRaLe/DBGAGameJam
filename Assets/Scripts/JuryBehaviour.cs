using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JuryBehaviour : MonoBehaviour
{
    private GameRulesTemplate rulesTemplate;
    private CriminalBehaviour currentCriminal;

    private float fractTime;

    public GameObject[] tips;

    private void Start()
    {
        rulesTemplate = GameManager.Instance.rulesTemplate;
        currentCriminal = GameManager.Instance.currentCriminal;

        TipSpriteSet();

        ShowHideTips(false);

        fractTime = rulesTemplate.verdictTimer / 3;
    }

    private void Update()
    {
        if (GameManager.Instance.currentCriminal != currentCriminal)
        {
            ShowHideTips(false);

            currentCriminal = GameManager.Instance.currentCriminal;

            TipSpriteSet();
        }

        GiveTip();
    }

    public void GiveTip()
    {
        if (currentCriminal.verdictCurrentTimer >= 0.5f && currentCriminal.verdictCurrentTimer < fractTime)
        {
            tips[0].SetActive(true);
            //GameManager.Instance.audioManager.PlaySound(4);
        }
        else if (currentCriminal.verdictCurrentTimer >= fractTime && currentCriminal.verdictCurrentTimer <= fractTime * 2)
        {
            tips[1].SetActive(true);
            //GameManager.Instance.audioManager.PlaySound(4);
        }
        else if (currentCriminal.verdictCurrentTimer >= fractTime && currentCriminal.verdictCurrentTimer <= fractTime * 3)
        {
            tips[2].SetActive(true);
            //GameManager.Instance.audioManager.PlaySound(4);
        }
    }

    public void TipSpriteSet()
    {
        var i = 0;

        foreach (GameObject tip in tips)
        {
            tip.GetComponent<Image>().sprite = currentCriminal.criminalTemplate.crimeTemplate.tips[i];

            i++;
        }
    }

    public void ShowHideTips(bool show)
    {
        foreach (GameObject tip in tips)
        {
            tip.SetActive(show);
        }
    }
}
