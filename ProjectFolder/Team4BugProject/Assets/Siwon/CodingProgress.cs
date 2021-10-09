using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CodingProgress : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;
    public float codeFillAmount = 0.0f;
    public bool iscoding = false;
    public float codingTime = 0.0f;
    public Image codeprogress;
    public GameObject progressing;
    public GameObject joystick;
    public GameObject killbutton;
    public GameObject hand;

    float N = 10; //상호작용 한 번당 증가할 값
    float M = 0.5f; //몇 초당 1씩 줄어드는지
    float k = 2f; //부엌, 화장실 상호작용 걸리는 시간
    // float i = 1f; //창문 여닫는데 걸리는 시간

    public void StartCoding()
    {
        iscoding = true;
        progressing.SetActive(true);
        joystick.SetActive(false);
        killbutton.SetActive(false);
        hand.SetActive(false);
    }

    public void StopCoding()
    {
        iscoding = false;
        progressing.SetActive(false);
        joystick.SetActive(true);
        killbutton.SetActive(true);
        hand.SetActive(true);
    }
    void Update()
    {
        if(iscoding == true)
        {
            codingTime += Time.deltaTime;
            gameManager.saveData.placeCleannessNumber[(int)PlaceName.RecycleBin] += Time.deltaTime / k * (N + (int)k);
        }

        if(codingTime >= 2.0f)
        {
            codeFillAmount += 5f;
            codingTime = 0.0f;
        }

        codeprogress.fillAmount = codeFillAmount / 100.0f;
    }
}
