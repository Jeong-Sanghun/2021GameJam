using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesireChangeManager : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    SoundManager soundManager;
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


    ///// �屸 ��������
    [SerializeField] Image AirGaugeBar;
    [SerializeField] Image HungrinessGaugeBar;
    [SerializeField] Image ToiletingGaugeBar;


    /////
    [SerializeField] GameObject GameOverPopUp;

    /////
    [SerializeField] List<Image> PlaceImgList = new List<Image>();

    /////
    IEnumerator cor;
    float N = 10; //��ȣ�ۿ� �� ���� ������ ��
    float M = 4f; //�� �ʴ� 1�� �پ�����
    float k = 7f; //�ξ�, ȭ��� ��ȣ�ۿ� �ɸ��� �ð�
    // float i = 1f; //â�� ���ݴµ� �ɸ��� �ð�



    void Start()
    {
        saveData = gameManager.saveData;

        if (saveData.isWindowOpen)
        {
            cor = IncreaseAir();
        }
        else
        {
            cor = DecreaseAir();
        }
        
        StartCoroutine(cor);
        StartCoroutine(DecreaseHungriness());
        StartCoroutine(DecreaseToileting());
        
    }

    //////////////////////////

    public void AirInteraction()
    {
        if(saveData.isWindowOpen) {
            saveData.isWindowOpen = false;
            soundManager.CloseWindowPlay();
        }
        else {
            saveData.isWindowOpen = true;
            soundManager.OpenWindowPlay();
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
        while (timer < 1.5f)
        {
            timer += Time.deltaTime;
            windowGaugeImage.fillAmount = timer/1.5f;
            yield return null;
        }
        windowGaugeObject.SetActive(false);
        buttonManager.OnInteractionReturn();
        WindowStateChanged();
        gameManager.SaveJson();
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
            yield return new WaitForSeconds(1f);
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
            soundManager.DishesPlay();
        }
        else
        {
           hungrinessGaugeText.text = "��Դ���";
            soundManager.CookPlay();
        }
        float timer = 0;
        while (timer < 7f)
        {
            timer += Time.deltaTime;
            hungrinessGaugeImage.fillAmount = timer/7f;
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
        gameManager.SaveJson();
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
            soundManager.ToiletWashPlay();
            toiletGaugeText.text = "û���ϴ���";
        }
        else
        {
            soundManager.ToiletPlay();
            toiletGaugeText.text = "�˴۴���";
        }
        float timer = 0;
        while (timer < 7f)
        {
            timer += Time.deltaTime;
            toiletGaugeImage.fillAmount = timer/7f;
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
        gameManager.SaveJson();
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
        soundManager.RecycleBinPlay();
        StartCoroutine(RecycleBinCleanCoroutine());
    }

    IEnumerator RecycleBinCleanCoroutine()
    {
        recycleBinGaugeObject.SetActive(true);
        float timer = 0;
        while (timer < 7f)
        {
            timer += Time.deltaTime;
            recycleBinGaugeImage.fillAmount = timer/7f;
            yield return null;
        }
        recycleBinGaugeObject.SetActive(false);
        buttonManager.OnInteractionReturn();
        saveData.placeCleannessNumber[(int)PlaceName.RecycleBin] -= 10;
        if (saveData.placeCleannessNumber[(int)PlaceName.RecycleBin] < 0)
        {
            saveData.placeCleannessNumber[(int)PlaceName.RecycleBin] = 0;
        }
        gameManager.SaveJson();
    }

    ////////////////////////////////////

    void GameOver()
    {
        Time.timeScale = 0;
        GameOverPopUp.SetActive(true);
        gameManager.saveData = new SaveDataClass();
        gameManager.SaveJson();
    }

    void FixedUpdate()
    {
        AirGaugeBar.fillAmount = saveData.desireNumber[(int)DesireName.Air] / 100;
        HungrinessGaugeBar.fillAmount = saveData.desireNumber[(int)DesireName.Hungriness] / 100;
        ToiletingGaugeBar.fillAmount = saveData.desireNumber[(int)DesireName.Toileting] / 100;

        if ((saveData.desireNumber[(int)DesireName.Air] <= 30) || (saveData.desireNumber[(int)DesireName.Hungriness] <= 30)
            || (saveData.desireNumber[(int)DesireName.Toileting] <= 30) || saveData.appearedBugList.Count > 20 || 
            saveData.leftTime < 120)
        {
            soundManager.BGMChanger(true);
        }
        else
        {
            soundManager.BGMChanger(false);
        }
        if ((saveData.desireNumber[(int)DesireName.Air]<=0) || (saveData.desireNumber[(int)DesireName.Hungriness]<=0) || (saveData.desireNumber[(int)DesireName.Toileting]<=0) || 
            saveData.appearedBugList.Count>40 || saveData.leftTime<0) {
            GameOver();
        }
    }
}