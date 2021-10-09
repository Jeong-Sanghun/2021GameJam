using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    SoundManager soundManager;
    [SerializeField]
    GameObject joyStick;
    [SerializeField]
    GameObject catchButton;
    
    [SerializeField]
    GameObject buttonsParent;
    [SerializeField]
    GameObject interactionButtonParent;

    [SerializeField]
    GameObject interactionKitchen;
    [SerializeField]
    GameObject interactionToilet;
    [SerializeField]
    GameObject interactionRecycleBin;
    [SerializeField]
    GameObject interactionWindowOpen;
    [SerializeField]
    GameObject interactionWindowClose;

    [SerializeField]
    GameObject windowOpenButton;
    [SerializeField]
    GameObject windowCloseButton;

    List<GameObject> interactionList;

    bool nowComputer;
    public bool nowInteraction;

    // Start is called before the first frame update
    void Start()
    {
        buttonsParent.SetActive(true);
        interactionButtonParent.SetActive(false);
        interactionList = new List<GameObject>();
        interactionList.Add(interactionKitchen);
        interactionList.Add(interactionToilet);
        interactionList.Add(interactionRecycleBin);
        interactionList.Add(interactionWindowOpen);
        interactionList.Add(interactionWindowClose);
        nowComputer = false;
        nowInteraction = false;


    }

    void EssentialSetup()
    {
        buttonsParent.SetActive(false);
        interactionButtonParent.SetActive(true);
        for (int i = 0; i < interactionList.Count; i++)
        {
            interactionList[i].SetActive(false);
        }
        soundManager.ClickTruePlay();
    }

    public void OnKitchenButton()
    {
        EssentialSetup();
        interactionKitchen.SetActive(true);
    }

    public void OnToiletButton()
    {
        EssentialSetup();
        interactionToilet.SetActive(true);
    }

    public void OnRecycleBinButton()
    {
        EssentialSetup();
        interactionRecycleBin.SetActive(true);
    }

    public void OnWindowButton()
    {
        EssentialSetup();
        //���� â�� �����ִ��� �ƴ����� ���� �ٸ��� �ڵ��ؾ���. if�� �ϳ� ���� ��
        bool isOpen = gameManager.saveData.isWindowOpen;
        windowCloseButton.SetActive(isOpen);
        windowOpenButton.SetActive(!isOpen);
    }

    public void OnComputerButton(bool active)
    {
        nowComputer = !active;
        buttonsParent.SetActive(active);
        soundManager.ClickTruePlay();
    }

    


    public void OnInteractionButton()
    {
        soundManager.ClickTruePlay();
        interactionButtonParent.SetActive(false);
        for (int i = 0; i < interactionList.Count; i++)
        {
            interactionList[i].SetActive(false);
        }
        nowComputer = true;
        joyStick.SetActive(false);
        catchButton.SetActive(false);
        nowInteraction = true;
    }

    public void OnInteractionReturn()
    {
        nowInteraction = false;
        buttonsParent.SetActive(true);
        nowComputer = false;
        joyStick.SetActive(true);
        catchButton.SetActive(true);
    }


    public void OnTransparentBG()
    {
       if(buttonsParent.activeSelf == true)
        {
            return;
        }
       if(nowComputer == true)
        {
            return;
        }

        buttonsParent.SetActive(true);

        interactionButtonParent.SetActive(false);
        for(int i = 0; i < interactionList.Count; i++)
        {
            if(interactionList[i].activeSelf == true)
            {
                interactionList[i].SetActive(false);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
