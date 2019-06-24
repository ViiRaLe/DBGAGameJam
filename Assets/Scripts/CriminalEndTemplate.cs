using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CriminalEndTemplate : MonoBehaviour
{
    public Image p;
    public Image t1;
    public Image t2;
    public Image t3;
    public Text c;
    public Text s;



    public Sprite photo;
    public Sprite tip1;
    public Sprite tip2;
    public Sprite tip3;
    public string crime;
    public int sentence;


    private void Start()
    {
        p.sprite = photo;
        t1.sprite = tip1;
        t2.sprite = tip2;
        t3.sprite = tip3;
        c.text = crime;
        s.text = sentence.ToString();
    }
}
