using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;


public enum GameOvered
{
    Air,Hungry,Toilet,Time,Bug,
}
public class DesireChangeManager : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    SoundManager soundManager;
    [SerializeField]
    AdManager adManager;
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
    [SerializeField] Text gameOverText;
    [SerializeField] Text leftTimeText;


    /////
    [SerializeField] List<GameObject> PlaceSprList = new List<GameObject>();
    //kitchen restroom recyclebin window 순서
    [SerializeField] List<Sprite> PlaceImgList = new List<Sprite>();

    /////
    IEnumerator cor;
    float N = 60; //상호작용 한 번당 증가할 값
    float M = 1.1f; //몇 초당 1씩 줄어드는지
    float k = 7f; //부엌, 화장실 상호작용 걸리는 시간

    int cleanAmount = 60; //청소 시 감소하는 값

    // float i = 1f; //창문 여닫는데 걸리는 시간



    void Start()
    {
        saveData = gameManager.saveData;

        if (saveData.isWindowOpen)
        {
            PlaceSprList[(int)PlaceName.Window].GetComponent<SpriteRenderer>().sprite = PlaceImgList[7];
            cor = IncreaseAir();
        }
        else
        {
            PlaceSprList[(int)PlaceName.Window].GetComponent<SpriteRenderer>().sprite = PlaceImgList[6];
            cor = DecreaseAir();
        }
        if (saveData.placeCleannessNumber[(int)PlaceName.Kitchen] >= 30) //스프라이트 이미지교체
        {
            //kitchen_dirty로 스프라이트 교체
            PlaceSprList[(int)PlaceName.Kitchen].GetComponent<SpriteRenderer>().sprite = PlaceImgList[1];

        }
        if (saveData.placeCleannessNumber[(int)PlaceName.Kitchen] >= 30) //스프라이트 이미지교체
        {
            //kitchen_dirty로 스프라이트 교체
            PlaceSprList[(int)PlaceName.Kitchen].GetComponent<SpriteRenderer>().sprite = PlaceImgList[1];

        }
        if (saveData.placeCleannessNumber[(int)PlaceName.RestRoom] >= 30) //스프라이트 이미지교체
        {
            //restroom_dirty로 스프라이트 교체
            PlaceSprList[(int)PlaceName.RestRoom].GetComponent<SpriteRenderer>().sprite = PlaceImgList[3];

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
        if(!isCleaning)
        {
            StartCoroutine(IncreaseHungriness());
        }
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
            if (saveData.placeCleannessNumber[(int)PlaceName.Kitchen] <= 30) //스프라이트 이미지교체
            {
                //kitchen_clean으로 스프라이트 교체
                PlaceSprList[(int)PlaceName.Kitchen].GetComponent<SpriteRenderer>().sprite = PlaceImgList[0];
            }
            if (saveData.placeCleannessNumber[(int)PlaceName.Kitchen] < 0)
            {
                saveData.placeCleannessNumber[(int)PlaceName.Kitchen] = 0;
            }
        }
        /*
        else
        {
            StartCoroutine(IncreaseHungriness());
        }*/
        gameManager.SaveJson();
    }

    IEnumerator IncreaseHungriness() //시간 k동안 값이 N만큼 증가
    {
        float time = 0f;
        while (time < k)
        {
            saveData.desireNumber[(int)DesireName.Hungriness] += Time.deltaTime / k * (N + (int)k);
            saveData.placeCleannessNumber[(int)PlaceName.Kitchen] += Time.deltaTime / k * (N + (int)k);
            if (saveData.placeCleannessNumber[(int)PlaceName.Kitchen] >= 30) //스프라이트 이미지교체
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
        if(!isCleaning)
        {
            StartCoroutine(IncreaseToileting());
        }
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
            if (saveData.placeCleannessNumber[(int)PlaceName.RestRoom] <= 30) //스프라이트 이미지교체
            {
                //restroom_clean으로 스프라이트 교체
                PlaceSprList[(int)PlaceName.RestRoom].GetComponent<SpriteRenderer>().sprite = PlaceImgList[2];
            }
            if (saveData.placeCleannessNumber[(int)PlaceName.RestRoom] < 0)
            {
                saveData.placeCleannessNumber[(int)PlaceName.RestRoom] = 0;
            }
        }
        /*
        else
        {
            StartCoroutine(IncreaseToileting());
        }*/
        gameManager.SaveJson();
    }

    IEnumerator IncreaseToileting() //시간 k동안 값이 N만큼 증가
    {
        float time = 0f;
        while (time < k)
        {
            saveData.desireNumber[(int)DesireName.Toileting] += Time.deltaTime / k * (N + (int)k);
            saveData.placeCleannessNumber[(int)PlaceName.RestRoom] += Time.deltaTime / k * (N + (int)k); ///
            if (saveData.placeCleannessNumber[(int)PlaceName.RestRoom] >= 30) //스프라이트 이미지교체
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
        if (saveData.placeCleannessNumber[(int)PlaceName.RecycleBin] <= 30) //스프라이트 이미지교체
        {
            //recyclebin_clean으로 스프라이트 교체
            PlaceSprList[(int)PlaceName.RecycleBin].GetComponent<SpriteRenderer>().sprite = PlaceImgList[4];
        }
        if (saveData.placeCleannessNumber[(int)PlaceName.RecycleBin] < 0)
        {
            saveData.placeCleannessNumber[(int)PlaceName.RecycleBin] = 0;
        }
        gameManager.SaveJson();
    }

    ////////////////////////////////////
    bool isGameOvered = false;
    void GameOver(GameOvered reason)
    {
        if(isGameOvered == true)
        {
            return;
        }
        switch (reason)
        {
            case GameOvered.Air:
                gameOverText.text = "맑은 공기를 마시지 못해 쓰러졌습니다...";
                break;
            case GameOvered.Hungry:
                gameOverText.text = "배고픔을 견디다 기절했습니다...";
                break;
            case GameOvered.Toilet:
                gameOverText.text = "화장실 가는 것을 참다가 참사가 일어났습니다..." ;
                break;
            case GameOvered.Bug:
                gameOverText.text = "벌레들에 둘러싸여 쓰러졌습니다...";
                break;
            case GameOvered.Time:
                gameOverText.text = "밀려오는 졸음을 버티지 못하고 쓰러졌습니다...";
                leftTimeText.gameObject.SetActive(false);
                break;
            default:
                break;
        }
        StringBuilder builder =new StringBuilder( "남은시간 ");
        if (saveData.leftTime / 60 < 10)
        {
            builder.Append("0");
            builder.Append((saveData.leftTime / 60).ToString());
        }
        else
        {
            builder.Append((saveData.leftTime / 60).ToString());
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
        leftTimeText.text = builder.ToString();

        isGameOvered = true;
        Time.timeScale = 0;
        GameOverPopUp.SetActive(true);
        soundManager.StopWholeSound();
        gameManager.saveData = new SaveDataClass();
        gameManager.SaveJson();
        adManager.ShowAd();
    }

    void FixedUpdate()
    {
        //욕구게이지바
        AirGaugeBar.fillAmount = saveData.desireNumber[(int)DesireName.Air] / 100;
        HungrinessGaugeBar.fillAmount = saveData.desireNumber[(int)DesireName.Hungriness] / 100;
        ToiletingGaugeBar.fillAmount = saveData.desireNumber[(int)DesireName.Toileting] / 100;


        //사운드
        if ((saveData.desireNumber[(int)DesireName.Air] <= 30) || (saveData.desireNumber[(int)DesireName.Hungriness] <= 30)
            || (saveData.desireNumber[(int)DesireName.Toileting] <= 30) || saveData.appearedBugList.Count > 40 ||
            saveData.leftTime < 120)
        {
            soundManager.BGMChanger(true);
        }
        else
        {
            soundManager.BGMChanger(false);
        }


        //게임오버
        if (saveData.desireNumber[(int)DesireName.Air] <= 0)
        {
            GameOver(GameOvered.Air);

        }
        if(saveData.desireNumber[(int)DesireName.Hungriness] <= 0)
        {
            GameOver(GameOvered.Hungry);
        }
            
         if(saveData.desireNumber[(int)DesireName.Toileting] <= 0)
        {
            GameOver(GameOvered.Toilet);
        }
          if(saveData.appearedBugList.Count > 40)
        {
            GameOver(GameOvered.Bug);
        }
            if(saveData.leftTime < 0)
        {
            GameOver(GameOvered.Time);
        }

    }
}