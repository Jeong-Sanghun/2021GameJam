using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugCatchManager : MonoBehaviour
{
    GameManager gameManager;
    SaveDataClass saveData;
    List<BugIngameClass> appearedBugList;
    [SerializeField]
    Transform handTransform;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.inst;
        saveData = gameManager.saveData;
        appearedBugList = saveData.appearedBugList;
        
    }

    //public void On

    // Update is called once per frame
    void Update()
    {
        
    }
}
