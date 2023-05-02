using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float timeLevel;
    public Text timeLevel_txt;
    public static bool stopTimer;

    void Start()
    {
        stopTimer = false;
    }

    void Update()
    {
        if (stopTimer == false)
        {
            this.timeLevel += Time.deltaTime;
            this.timeLevel_txt.text = this.timeLevel.ToString("0");
        }
    }
}
