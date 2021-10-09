using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager inst;

    public BugAppearingClassWrapper bugAppearingWrapper;
    public BugWrapper bugWrapper;
    public SaveDataClass saveData;
    JsonManager jsonManager;



    private void Awake()
    {
        if(inst == null)
        {
            inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
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
