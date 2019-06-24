using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreenManager : MonoBehaviour
{
    public GameObject checkPanel;

    public void OnStart()
    {
        SceneManager.LoadScene(3);
    }

    public void OnOptions()
    {
        SceneManager.LoadScene(2);
    }
    
    

    //PANEL Methods

    public void OnYes()
    {
        Application.Quit();
    }

    public void OnBack()
    {
        CheckPanel(false);
    }



    public void CheckPanel(bool activate)
    {
        checkPanel.SetActive(activate);
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !checkPanel.activeSelf) CheckPanel(true);
        else if (Input.GetKeyDown(KeyCode.Escape) && checkPanel.activeSelf) CheckPanel(false);
    }
}
