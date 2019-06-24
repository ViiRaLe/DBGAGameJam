using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject checkPanel;


    public void OnVolume()
    {
        //volume things
    }

    public void OnPause()
    {
        PausePanel(true);
    }

    public void OnResume()
    {
        PausePanel(false);        
    }

    public void OnRestart()  //check
    {
        CheckPanel(true, 3);
    }

    public void OnMenu()  //check
    {
        CheckPanel(true, 0);
    }

    


    private void PausePanel(bool active)
    {
        pausePanel.SetActive(active);
        Time.timeScale = pausePanel.activeSelf ? 0f : 1.0f;
    }

  
    private void CheckPanel(bool active, int coming)  //  3 restart, 0 title
    {
        checkPanel.SetActive(active);
        checkPanel.GetComponent<CheckPanelManager>().sceneTo = coming;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !pausePanel.activeSelf) PausePanel(true);
        else if (Input.GetKeyDown(KeyCode.Escape) && pausePanel.activeSelf && checkPanel.activeSelf) CheckPanel(false, 0);
        else if (Input.GetKeyDown(KeyCode.Escape) && pausePanel.activeSelf && !checkPanel.activeSelf) PausePanel(false);
    }
}
