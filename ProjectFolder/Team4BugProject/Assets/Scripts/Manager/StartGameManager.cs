using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameManager : MonoBehaviour
{
    public void OnClickNewStartBtn()
    {
        //새로 시작하기
        SceneManager.LoadScene("SiwonScene");
    }

    public void OnClickContinueBtn()
    {
        //중간에 로딩씬?
        SceneManager.LoadScene("SiwonScene");
    }
}
