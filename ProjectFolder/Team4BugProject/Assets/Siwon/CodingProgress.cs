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

    float N = 10; //��ȣ�ۿ� �� ���� ������ ��
    float M = 0.5f; //�� �ʴ� 1�� �پ�����
    float k = 2f; //�ξ�, ȭ��� ��ȣ�ۿ� �ɸ��� �ð�
    // float i = 1f; //â�� ���ݴµ� �ɸ��� �ð�

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
