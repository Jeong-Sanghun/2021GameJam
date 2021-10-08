using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugMoveManager : MonoBehaviour
{
    GameManager gameManager;
    SaveDataClass saveData;
    List<BugIngameClass> appearedBugList;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.inst;
        saveData = gameManager.saveData;

        appearedBugList = saveData.appearedBugList;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < appearedBugList.Count; i++)
        {

            if((int)appearedBugList[i].name < 2)
            {
                Vector2 deltaPos = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
                appearedBugList[i].bugObject.GetComponent<RectTransform>().anchoredPosition += deltaPos;
                
            }
            else
            {
                Vector2 deltaPos = new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));
                appearedBugList[i].bugObject.transform.position += (Vector3)deltaPos;
            }
        }
    }

    
}
