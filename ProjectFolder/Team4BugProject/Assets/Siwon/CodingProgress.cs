using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class CodingProgress : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;
    SaveDataClass saveData;
    [SerializeField]
    SoundManager soundManager;
    [SerializeField]
    SpriteRenderer recycleBinRenderer;
    [SerializeField]
    Sprite recycleBinDirtySprite;
    public float codeFillAmount = 0.0f;
    public bool iscoding = false;
    public float codingTime = 0.0f;
    public Image codeprogress;
    public GameObject progressing;
    public GameObject joystick;
    public GameObject killbutton;
    public GameObject hand;
    public GameObject GameWinUI;
    public Text leftTimeText;

    float N = 10; //��ȣ�ۿ� �� ���� ������ ��
    float M = 0.5f; //�� �ʴ� 1�� �پ�����
    float k = 2f; //�ξ�, ȭ��� ��ȣ�ۿ� �ɸ��� �ð�
    // float i = 1f; //â�� ���ݴµ� �ɸ��� �ð�
    private void Start()
    {
        saveData = gameManager.saveData;
        codeFillAmount = saveData.codingProgress;
        codeprogress.fillAmount = codeFillAmount / 100.0f;
    }

    public void StartCoding()
    {
        iscoding = true;
        progressing.SetActive(true);
        joystick.SetActive(false);
        killbutton.SetActive(false);
        hand.SetActive(false);
        soundManager.CodingPlay();

    }

    public void StopCoding()
    {
        iscoding = false;
        progressing.SetActive(false);
        joystick.SetActive(true);
        killbutton.SetActive(true);
        hand.SetActive(true);
        soundManager.StopEffectSound();
        gameManager.SaveJson();
    }
    void Update()
    {
        if(iscoding == true)
        {
            codingTime += Time.deltaTime;
            gameManager.saveData.placeCleannessNumber[(int)PlaceName.RecycleBin] += Time.deltaTime / k * (N + (int)k);
            if(gameManager.saveData.placeCleannessNumber[(int)PlaceName.RecycleBin] >= 20) ////////// ��������Ʈ �̹�����ü
            {
                //RecycleBin_dirty�� sprite ��ü
                recycleBinRenderer.sprite = recycleBinDirtySprite;
            }
        }

        if(codingTime >= 2.0f)
        {
            codeFillAmount += 5f;
            saveData.codingProgress = codeFillAmount;
            codingTime = 0.0f;
        }

        codeprogress.fillAmount = codeFillAmount / 100.0f;

        if(codeFillAmount >=100.0f)
        {
            GameWin();
        }
    }
    bool gameWinned = false;
    public void GameWin()
    {
        if(gameWinned == true)
        {
            return;
        }gameWinned = true;

        StringBuilder builder = new StringBuilder("�����ð� ");
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

        GameWinUI.SetActive(true);
        soundManager.StopWholeSound();
        Time.timeScale = 0;
        gameManager.saveData = new SaveDataClass();
        gameManager.SaveJson();
    }
}
