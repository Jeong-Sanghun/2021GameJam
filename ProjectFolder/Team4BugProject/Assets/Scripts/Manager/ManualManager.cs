using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManualManager : MonoBehaviour
{
    public List<GameObject> ManualLst = new List<GameObject>();
    int currentPage = 0;

    public void OnClickBackBtn()
    {
        if(currentPage > 0) {
            ManualLst[currentPage].SetActive(false);
            ManualLst[currentPage-1].SetActive(true);
            currentPage--;
        }
    }

    public void OnClickNextBtn()
    {
        if(currentPage < 4) {
            ManualLst[currentPage].SetActive(false);
            ManualLst[currentPage+1].SetActive(true);
            currentPage++;
        }
    }
}
