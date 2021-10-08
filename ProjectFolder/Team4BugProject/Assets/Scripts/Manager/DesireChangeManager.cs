using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesireChangeManager : MonoBehaviour
{
    GameManager gameManager;
    SaveDataClass saveData;

    /////
    IEnumerator cor;


    void Start()
    {
        gameManager = GameManager.inst;
        saveData = gameManager.saveData;

        cor = DecreaseAir();
        StartCoroutine(cor);
        StartCoroutine(DecreaseHungriness());
        StartCoroutine(DecreaseToileting());
    }

    public void WindowStateChanged()
    {
        StopCoroutine(cor);

        if(saveData.isWindowOpen) {
            cor = DecreaseAir();
        }
        else {
            cor = IncreaseAir();
        }
        StartCoroutine(cor);
    }

    IEnumerator IncreaseAir()
    {
        while(saveData.desireNumber[(int)DesireName.Air] < 100) {
            saveData.desireNumber[(int)DesireName.Air]++;
            //Debug.Log(saveData.desireNumber[(int)DesireName.Air]); //
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator DecreaseAir()
    {
        while(saveData.desireNumber[(int)DesireName.Air] > 0) {
            saveData.desireNumber[(int)DesireName.Air]--;
            //Debug.Log(saveData.desireNumber[(int)DesireName.Air]); //
            yield return new WaitForSeconds(1f);
        }
    }

    public void IncreaseHungriness() //상호작용 발생시 호출
    {
        saveData.desireNumber[(int)DesireName.Hungriness] += 10; //n값 정해지면 수정
        //Debug.Log(saveData.desireNumber[(int)DesireName.Hungriness]); //
    }

    IEnumerator DecreaseHungriness()
    {
        while(saveData.desireNumber[(int)DesireName.Hungriness] > 0) {
            saveData.desireNumber[(int)DesireName.Hungriness]--;
            //Debug.Log(saveData.desireNumber[(int)DesireName.Hungriness]); //
            yield return new WaitForSeconds(1f);
        }
    }

    public void IncreaseToileting() //상호작용 발생시 호출
    {
        saveData.desireNumber[(int)DesireName.Toileting] += 10; //n값 정해지면 수정
        //Debug.Log(saveData.desireNumber[(int)DesireName.Toileting]); //
    }

    IEnumerator DecreaseToileting()
    {
        while(saveData.desireNumber[(int)DesireName.Toileting] > 0) {
            saveData.desireNumber[(int)DesireName.Toileting]--;
            //Debug.Log(saveData.desireNumber[(int)DesireName.Toileting]); //
            yield return new WaitForSeconds(1f);
        }
    }
}
