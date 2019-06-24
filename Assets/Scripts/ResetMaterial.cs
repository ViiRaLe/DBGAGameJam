using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetMaterial : MonoBehaviour
{
    private void OnDisable()
    {
        var img = GetComponent<Image>();

        img.material.SetFloat("_Threshold", 0f);
    }
}
