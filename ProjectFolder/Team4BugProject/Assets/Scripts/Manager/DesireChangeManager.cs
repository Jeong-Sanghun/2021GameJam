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
    //[SerializeField]
    //Text windowGaugeText;
    [SerializeField]
    Image windowGaugeImage;

    [SerializeField]
    GameObject hungrinessGaugeObject;
    //[SerializeField]
    //Text hungrinessGaugeText;
    [SerializeField]
    Image hungrinessGaugeImage;

    [SerializeField]
    GameObject toiletGaugeObject;
    //[SerializeField]
    //Text toiletGaugeText;
    [SerializeField]
    Image toiletGaugeImage;

    [SerializeField]
    GameObject recycleBinGaugeObject;
    [SerializeField]
    Image recycleBinGaugeImage;


    ///// 욕구 게이지바
    [SerializeField] Image AirGaugeBar;
    [SerializeField] Image HungrinessGaugeBar;
    [SerializeField] Image ToiletingGaugeBar;


    /////
    [SerializeField] GameObject GameOverPopUp;

    /////
    [SerializeField] List<GameObject> PlaceSprList = new List<GameObject>();
    //kitchen restroom recyclebin window 순서
    [SerializeField] List<Sprite> PlaceImgList = new List<Sprite>();

    /////
    IEnumerator cor;
    float N = 30; //상호작용 한 번당 증가할 값
    float M = 2f; //몇 초당 1씩 줄어드는지
    float k = 7f; //부엌, 화장실 상호작용 걸리는 시간

    int cleanAmount = 200; //청소 시 감소하는 값

    // float i = 1f; //창문 여닫는데 걸리는 시간



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
        if (saveData.isWindowOpen)
        {
            saveData.isWindowOpen = false;
            //스프라이트 이미지교체
            //window_closed으로 스프라이트 교체
            PlaceSprList[(int)PlaceName.Window].GetComponent<SpriteRenderer>().sprite = PlaceImgList[6];
            soundManager.CloseWindowPlay();
        }
        else
        {
            saveData.isWindowOpen = true;
            //스프라이트 이미지교체
            //window_open으로 스프라이트 교체
            PlaceSprList[(int)PlaceName.Window].GetComponent<SpriteRenderer>().sprite = PlaceImgList[7];
            soundManager.OpenWindowPlay();
        }

        StartCoroutine(WindowStateChangeCoroutine(saveData.isWindowOpen));
    }

    IEnumerator WindowStateChangeCoroutine(bool isOpening)
    {
        windowGaugeObject.SetActive(true);
        float timer = 0;
        while (timer < 1.5f)
        {
            timer += Time.deltaTime;
            windowGaugeImage.fillAmount = timer / 1.5f;
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

        if (saveData.isWindowOpen)
        {
            cor = IncreaseAir();
        }
        else
        { //!isWindowOpen
            cor = DecreaseAir();
        }
        StartCoroutine(cor);
    }

    IEnumerator IncreaseAir()
    {
        while (saveData.desireNumber[(int)DesireName.Air] < 100)
        {
            saveData.desireNumber[(int)DesireName.Air]++;
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator DecreaseAir()
    {
        while (saveData.desireNumber[(int)DesireName.Air] > 0)
        {
            saveData.desireNumber[(int)DesireName.Air]--;
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

            soundManager.DishesPlay();
        }
        else
        {

            soundManager.CookPlay();
        }
        float timer = 0;
        while (timer < 7f)
        {
            timer += Time.deltaTime;
            hungrinessGaugeImage.fillAmount = timer / 7f;
            yield return null;
        }
        hungrinessGaugeObject.SetActive(false);
        buttonManager.OnInteractionReturn();
        if (isCleaning) //청소 상호작용 시
        {
            saveData.placeCleannessNumber[(int)PlaceName.Kitchen] -= cleanAmount; ////////
            if (saveData.placeCleannessNumber[(int)PlaceName.Kitchen] <= 20) //스프라이트 이미지교체
            {
                //kitchen_clean으로 스프라이트 교체
                PlaceSprList[(int)PlaceName.Kitchen].GetComponent<SpriteRenderer>().sprite = PlaceImgList[0];
            }
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

    IEnumerator IncreaseHungriness() //시간 k동안 값이 N만큼 증가
    {
        float time = 0f;
        while (time < k)
        {
            saveData.desireNumber[(int)DesireName.Hungriness] += Time.deltaTime / k * (N + (int)k);
            saveData.placeCleannessNumber[(int)PlaceName.Kitchen] += Time.deltaTime / k * (N + (int)k);
            if (saveData.placeCleannessNumber[(int)PlaceName.Kitchen] >= 20) //스프라이트 이미지교체
            {
                //kitchen_dirty로 스프라이트 교체
                PlaceSprList[(int)PlaceName.Kitchen].GetComponent<SpriteRenderer>().sprite = PlaceImgList[1];

            }
            yield return null;
            time += Time.deltaTime;
        }
    }

    IEnumerator DecreaseHungriness()
    {
        while (saveData.desireNumber[(int)DesireName.Hungriness] > 0)
        {
            saveData.desireNumber[(int)DesireName.Hungriness]--;
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

        }
        else
        {
            soundManager.ToiletPlay();

        }
        float timer = 0;
        while (timer < 7f)
        {
            timer += Time.deltaTime;
            toiletGaugeImage.fillAmount = timer / 7f;
            yield return null;
        }
        toiletGaugeObject.SetActive(false);
        buttonManager.OnInteractionReturn();
        if (isCleaning) //청소 상호작용 시
        {
            saveData.placeCleannessNumber[(int)PlaceName.RestRoom] -= cleanAmount;
            if (saveData.placeCleannessNumber[(int)PlaceName.RestRoom] <= 20) //스프라이트 이미지교체
            {
                //restroom_clean으로 스프라이트 교체
                PlaceSprList[(int)PlaceName.RestRoom].GetComponent<SpriteRenderer>().sprite = PlaceImgList[2];
            }
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

    IEnumerator IncreaseToileting() //시간 k동안 값이 N만큼 증가
    {
        float time = 0f;
        while (time < k)
        {
            saveData.desireNumber[(int)DesireName.Toileting] += Time.deltaTime / k * (N + (int)k);
            saveData.placeCleannessNumber[(int)PlaceName.RestRoom] += Time.deltaTime / k * (N + (int)k); ///
            if (saveData.placeCleannessNumber[(int)PlaceName.RestRoom] >= 20) //스프라이트 이미지교체
            {
                //restroom_dirty로 스프라이트 교체
                PlaceSprList[(int)PlaceName.RestRoom].GetComponent<SpriteRenderer>().sprite = PlaceImgList[3];

            }
            yield return null;
            time += Time.deltaTime;
        }
    }


    IEnumerator DecreaseToileting()
    {
        while (saveData.desireNumber[(int)DesireName.Toileting] > 0)
        {
            saveData.desireNumber[(int)DesireName.Toileting]--;
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
            recycleBinGaugeImage.fillAmount = timer / 7f;
            yield return null;
        }
        recycleBinGaugeObject.SetActive(false);
        buttonManager.OnInteractionReturn();
        saveData.placeCleannessNumber[(int)PlaceName.RecycleBin] -= cleanAmount; /////
        if (saveData.placeCleannessNumber[(int)PlaceName.RecycleBin] >= 20) //스프라이트 이미지교체
        {
            //recyclebin_dirty으로 스프라이트 교체
            PlaceSprList[(int)PlaceName.RecycleBin].GetComponent<SpriteRenderer>().sprite = PlaceImgList[5];
        }
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
        //욕구게이지바
        AirGaugeBar.fillAmount = saveData.desireNumber[(int)DesireName.Air] / 100;
        HungrinessGaugeBar.fillAmount = saveData.desireNumber[(int)DesireName.Hungriness] / 100;
        ToiletingGaugeBar.fillAmount = saveData.desireNumber[(int)DesireName.Toileting] / 100;


        //사운드
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


        //게임오버
        if ((saveData.desireNumber[(int)DesireName.Air] <= 0) || (saveData.desireNumber[(int)DesireName.Hungriness] <= 0) || (saveData.desireNumber[(int)DesireName.Toileting] <= 0) ||
            saveData.appearedBugList.Count > 40 || saveData.leftTime < 0)
        {
            GameOver();
        }
    }
}