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
    float N = 10; //상호작용 한 번당 증가할 값
    float M = 0.5f; //몇 초당 1씩 줄어드는지
    float i = 1f; //창문 여닫는데 걸리는 시간
    float k = 2f; //부엌, 화장실 상호작용 걸리는 시간



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
            windowGaugeText.text = "문 여는중";
        }
        else
        {
            windowGaugeText.text = "문 닫는중";
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
            hungrinessGaugeText.text = "청소하는중";
        }
        else
        {
           hungrinessGaugeText.text = "밥먹는중";
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

    IEnumerator IncreaseHungriness() //시간 k동안 값이 N만큼 증가
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
            toiletGaugeText.text = "청소하는중";
        }
        else
        {
            toiletGaugeText.text = "똥닦는중";
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

    IEnumerator IncreaseToileting() //시간 k동안 값이 N만큼 증가
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