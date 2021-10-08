using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesireChangeManager : MonoBehaviour
{
    GameManager gameManager;
    SaveDataClass saveData;

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
        Invoke("WindowStateChanged", i);
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

    public void HungrinessInteraction()
    {
        StartCoroutine(IncreaseHungriness());
    }

    IEnumerator IncreaseHungriness() //시간 k동안 값이 N만큼 증가
    {
        float time = 0f;
        while(time < k) {
            saveData.desireNumber[(int)DesireName.Hungriness] += Time.deltaTime / k * (N+(int)k);
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

    public void ToiletingInteraction()
    {
        StartCoroutine(IncreaseToileting());
    }

    IEnumerator IncreaseToileting() //시간 k동안 값이 N만큼 증가
    {
        float time = 0f;
        while(time < k) {
            saveData.desireNumber[(int)DesireName.Toileting] += Time.deltaTime / k * (N+(int)k);
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
}