using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartManager : MonoBehaviour
{
    public void RestartGameScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    public void LoadMainScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
