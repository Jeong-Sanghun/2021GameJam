using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesireChangeManager : MonoBehaviour
{
    GameManager gameManager;
    SaveDataClass saveData;
    [SerializeField]
    ButtonManager buttonManager;

    [SerializeField]
    GameObject windowGaugeObject;
    [SerializeField]
    Text windowGaugeText;
    [SerializeField]
    Image windowGaugeImage;

    [SerializeField]
    GameObject hungrinessGaugeObject;
    [SerializeField]
    Text hungrinessGaugeText;
    [SerializeField]
    Image hungrinessGaugeImage;

    [SerializeField]
    GameObject toiletGaugeObject;
    [SerializeField]
    Text toiletGaugeText;
    [SerializeField]
    Image toiletGaugeImage;

    [SerializeField]
    GameObject recycleBinGaugeObject;
    [SerializeField]
    Image recycleBinGaugeImage;


    /////
    [SerializeField] Image AirGaugeBar;
    [SerializeField] Image HungrinessGaugeBar;
    [SerializeField] Image ToiletingGaugeBar;


    /////
    IEnumerator cor;
    float N = 10; //��ȣ�ۿ� �� ���� ������ ��
    float M = 0.5f; //�� �ʴ� 1�� �پ�����
    float i = 1f; //â�� ���ݴµ� �ɸ��� �ð�
    float k = 2f; //�ξ�, ȭ��� ��ȣ�ۿ� �ɸ��� �ð�



    void Start()
    {
        gameManager = GameManager.inst;
        saveData = gameManager.saveData;

        cor = DecreaseAir();
        StartCoroutine(cor);
        StartCoroutine(DecreaseHungriness());
        StartCoroutine(DecreaseToileting());
        
    }

    //////////////////////////

    public void AirInteraction()
    {
        if(saveData.isWindowOpen) {
            saveData.isWindowOpen = false;
        }
        else {
            saveData.isWindowOpen = true;
        }
        StartCoroutine(WindowStateChangeCoroutine(saveData.isWindowOpen));
    }

    IEnumerator WindowStateChangeCoroutine(bool isOpening)
    {
        windowGaugeObject.SetActive(true);
        if (isOpening)
        {
            windowGaugeText.text = "�� ������";
        }
        else
        {
            windowGaugeText.text = "�� �ݴ���";
        }
        float timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime;
            windowGaugeImage.fillAmount = timer;
            yield return null;
        }
        windowGaugeObject.SetActive(false);
        buttonManager.OnInteractionReturn();
        WindowStateChanged();
    }

    void WindowStateChanged()
    {
        StopCoroutine(cor);

        if(saveData.isWindowOpen) {
            cor = IncreaseAir();
        }
        else { //!isWindowOpen
            cor = DecreaseAir();
        }
        StartCoroutine(cor);
    }

    IEnumerator IncreaseAir()
    {
        while(saveData.desireNumber[(int)DesireName.Air] < 100) {
            saveData.desireNumber[(int)DesireName.Air]++;
            // Debug.Log(saveData.desireNumber[(int)DesireName.Air]); /////
            yield return new WaitForSeconds(M);
        }
    }

    IEnumerator DecreaseAir()
    {
        while(saveData.desireNumber[(int)DesireName.Air] > 0) {
            saveData.desireNumber[(int)DesireName.Air]--;
            // Debug.Log(saveData.desireNumber[(int)DesireName.Air]); /////
            yield return new WaitForSeconds(M);
        }
    }

    ////////////////////////////////

    public void HungrinessInteraction(bool isCleaning)
    {
        StartCoroutine(HungrinessGaugeCoroutine(isCleaning));
    }

    IEnumerator HungrinessGaugeCoroutine(bool isCleaning)
    {
        hungrinessGaugeObject.SetActive(true);
        if (isCleaning)
        {
            hungrinessGaugeText.text = "û���ϴ���";
        }
        else
        {
           hungrinessGaugeText.text = "��Դ���";
        }
        float timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime;
            hungrinessGaugeImage.fillAmount = timer;
            yield return null;
        }
       hungrinessGaugeObject.SetActive(false);
        buttonManager.OnInteractionReturn();
        if (isCleaning)
        {
            saveData.placeCleannessNumber[(int)PlaceName.Kitchen] -= 10;
            if (saveData.placeCleannessNumber[(int)PlaceName.Kitchen] < 0)
            {
                saveData.placeCleannessNumber[(int)PlaceName.Kitchen] = 0;
            }
        }
        else
        {
            StartCoroutine(IncreaseHungriness());
        }
    }

    IEnumerator IncreaseHungriness() //�ð� k���� ���� N��ŭ ����
    {
        float time = 0f;
        while(time < k) {
            saveData.desireNumber[(int)DesireName.Hungriness] += Time.deltaTime / k * (N+(int)k);
            saveData.placeCleannessNumber[(int)PlaceName.Kitchen] += Time.deltaTime / k * (N + (int)k);
            //Debug.Log("hungriness : " + saveData.desireNumber[(int)DesireName.Hungriness]); /////
            yield return null;
            time += Time.deltaTime;
        }
    }

    IEnumerator DecreaseHungriness()
    {
        while(saveData.desireNumber[(int)DesireName.Hungriness] > 0) {
            saveData.desireNumber[(int)DesireName.Hungriness]--;
            //Debug.Log("hungriness : " + saveData.desireNumber[(int)DesireName.Hungriness]); /////
            yield return new WaitForSeconds(M);
        }
    }

    ///////////////////////////////////

    public void ToiletingInteraction(bool isCleaning)
    {
        StartCoroutine(ToiletingGaugeCoroutine(isCleaning));
    }

    IEnumerator ToiletingGaugeCoroutine(bool isCleaning)
    {
        toiletGaugeObject.SetActive(true);
        if (isCleaning)
        {
            toiletGaugeText.text = "û���ϴ���";
        }
        else
        {
            toiletGaugeText.text = "�˴۴���";
        }
        float timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime;
            toiletGaugeImage.fillAmount = timer;
            yield return null;
        }
        toiletGaugeObject.SetActive(false);
        buttonManager.OnInteractionReturn();
        if (isCleaning)
        {
            saveData.placeCleannessNumber[(int)PlaceName.RestRoom] -= 10;
            if (saveData.placeCleannessNumber[(int)PlaceName.RestRoom] < 0)
            {
                saveData.placeCleannessNumber[(int)PlaceName.RestRoom] = 0;
            }
        }
        else
        {
            StartCoroutine(IncreaseToileting());
        }
    }

    IEnumerator IncreaseToileting() //�ð� k���� ���� N��ŭ ����
    {
        float time = 0f;
        while(time < k) {
            saveData.desireNumber[(int)DesireName.Toileting] += Time.deltaTime / k * (N+(int)k);
            saveData.placeCleannessNumber[(int)PlaceName.RestRoom] += Time.deltaTime / k * (N + (int)k);
            //Debug.Log(saveData.desireNumber[(int)DesireName.Toileting]); /////
            yield return null;
            time += Time.deltaTime;
        }
    }


    IEnumerator DecreaseToileting()
    {
        while(saveData.desireNumber[(int)DesireName.Toileting] > 0) {
            saveData.desireNumber[(int)DesireName.Toileting]--;
            //Debug.Log(saveData.desireNumber[(int)DesireName.Toileting]); /////
            yield return new WaitForSeconds(M);
        }
    }

    /////////////////////////////////////////
    
    public void CleanRecycleBin()
    {
        StartCoroutine(RecycleBinCleanCoroutine());
    }

    IEnumerator RecycleBinCleanCoroutine()
    {
        recycleBinGaugeObject.SetActive(true);
        float timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime;
            recycleBinGaugeImage.fillAmount = timer;
            yield return null;
        }
        recycleBinGaugeObject.SetActive(false);
        buttonManager.OnInteractionReturn();
        saveData.placeCleannessNumber[(int)PlaceName.RecycleBin] -= 10;
        if (saveData.placeCleannessNumber[(int)PlaceName.RecycleBin] < 0)
        {
            saveData.placeCleannessNumber[(int)PlaceName.RecycleBin] = 0;
        }
    }

}