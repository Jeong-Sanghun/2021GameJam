using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    public BugAppearingClassWrapper bugAppearingWrapper;
    public BugWrapper bugWrapper;
    public SaveDataClass saveData;
    JsonManager jsonManager;



    // Start is called before the first frame update
    void Awake()
    {
        bugWrapper = new BugWrapper();
        bugAppearingWrapper = new BugAppearingClassWrapper();
        jsonManager = new JsonManager();
        saveData = jsonManager.LoadSaveData();
    }

    public void SaveJson()
    {
        jsonManager.SaveJson(saveData);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
