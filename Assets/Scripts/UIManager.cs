using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private CriminalBehaviour currentCriminal = null;


    private void Update()
    {
        if (currentCriminal == null)
        {
            currentCriminal = GetComponentInChildren<CriminalBehaviour>();
        }
        else if (currentCriminal.judged)
        {
            currentCriminal = null;
        }        
    }


    public void Guilty()
    {

        currentCriminal.judged = true;
        currentCriminal = null;
    }

    public void NotGuilty()
    {
        GameManager.Instance.currentCriminal.guilty = false;
        GameManager.Instance.currentCriminal.currentPoints = 0f;

        GameManager.Instance.audioManager.PlaySound(0);

        currentCriminal.judged = true;
        currentCriminal = null;
    }
}
