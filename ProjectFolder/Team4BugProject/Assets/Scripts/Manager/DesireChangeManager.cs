using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesireChangeManager : MonoBehaviour
{
    GameManager gameManager;
    SaveDataClass saveData;

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

    IEnumerator IncreaseHungriness() //�ð� k���� ���� N��ŭ ����
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

    IEnumerator IncreaseToileting() //�ð� k���� ���� N��ŭ ����
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