using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPanelManager : MonoBehaviour
{
    [HideInInspector] public int sceneTo = -1;

    public void OnYes()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneTo);
    }

    public void OnBack()
    {
        gameObject.SetActive(false);
        sceneTo = -1;
    }
}
