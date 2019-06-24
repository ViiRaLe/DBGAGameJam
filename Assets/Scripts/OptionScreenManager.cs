using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionScreenManager : MonoBehaviour
{

    [HideInInspector] public bool volume = true;

    public void OnVolume()
    {
        volume = !volume;
        // change volume
    }

    public void OnMenu()
    {
        SceneManager.LoadScene(1);
    }
}
