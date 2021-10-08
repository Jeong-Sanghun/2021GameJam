using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{

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



    }

    void EssentialSetup()
    {
        buttonsParent.SetActive(false);
        interactionButtonParent.SetActive(true);
        for (int i = 0; i < interactionList.Count; i++)
        {
            interactionList[i].SetActive(false);
        }
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
        //만약 창문 열려있는지 아닌지에 한해 다르게 코딩해야함. if문 하나 들어가면 됨
        bool isOpen = GameManager.inst.saveData.isWindowOpen;
        windowCloseButton.SetActive(isOpen);
        windowOpenButton.SetActive(!isOpen);
    }

    public void OnComputerButton(bool active)
    {
        nowComputer = !active;
        buttonsParent.SetActive(active);
    }

    


    public void OnInteractionButton()
    {
        interactionButtonParent.SetActive(false);
        for (int i = 0; i < interactionList.Count; i++)
        {
            interactionList[i].SetActive(false);
        }
    }

    public void OnInteractionReturn()
    {
        buttonsParent.SetActive(true);

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
