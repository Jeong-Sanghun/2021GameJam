using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField]
    GameObject pausePopup;
    [SerializeField]
    GameManager gameManager;

    public void OnPauseButton()
    {
        Time.timeScale = 0;
        pausePopup.SetActive(true);
    }

    public void OnContinueButton()
    {
        Time.timeScale = 1;
        pausePopup.SetActive(false);
    }

    public void OnHomeButton()
    {
        gameManager.SaveJson();
        SceneManager.LoadScene(0);
    }
}
