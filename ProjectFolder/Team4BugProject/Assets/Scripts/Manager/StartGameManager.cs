using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameManager : MonoBehaviour
{
    public void OnClickNewStartBtn()
    {
        //���� �����ϱ�
        JsonManager jsonManager = new JsonManager();
        jsonManager.SaveJson(new SaveDataClass());
        SceneManager.LoadScene("SiwonScene");
    }

    public void OnClickContinueBtn()
    {
        //�߰��� �ε���?
        SceneManager.LoadScene("SiwonScene");
    }
}
