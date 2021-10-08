using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodingProgress : MonoBehaviour
{
    public float codeFillAmount = 0.0f;
    public bool testbool = false;
    public float codingTime = 0.0f;
    public Image codeprogress;
    void Update()
    {

        if(testbool == true)
        {
            codingTime += Time.deltaTime;
        }

        if(codingTime >= 2.0f)
        {
            codeFillAmount += 5f;
            codingTime = 0.0f;
        }

        codeprogress.fillAmount = codeFillAmount / 100.0f;
    }
}
