using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugSpawnManager : MonoBehaviour
{
    GameManager gameManager;
    BugWrapper bugWrapper;
    BugAppearingClassWrapper bugAppearingWrapper;
    SaveDataClass saveData;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.inst;
        bugWrapper = gameManager.bugWrapper;
        bugAppearingWrapper = gameManager.bugAppearingWrapper;
        saveData = gameManager.saveData;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
