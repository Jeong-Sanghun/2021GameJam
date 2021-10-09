using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;
    SaveDataClass saveData;

    [SerializeField]
    Text timeText;
    // Start is called before the first frame update
    void Start()
    {
        saveData = gameManager.saveData;
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        StringBuilder builder;
        while (true)
        {
            if (saveData.leftTime / 60 < 10)
            {
                builder = new StringBuilder("0");
                builder.Append((saveData.leftTime / 60).ToString());
            }
            else
            {
                builder = new StringBuilder((saveData.leftTime / 60).ToString());
            }
            builder.Append(" : ");
            if (saveData.leftTime % 60 < 10)
            {
                builder.Append("0");
                builder.Append((saveData.leftTime % 60).ToString());
            }
            else
            {
                builder.Append((saveData.leftTime % 60).ToString());
            }
            timeText.text = builder.ToString();
            yield return new WaitForSeconds(1f);
            saveData.leftTime--;

        }

    }
}
