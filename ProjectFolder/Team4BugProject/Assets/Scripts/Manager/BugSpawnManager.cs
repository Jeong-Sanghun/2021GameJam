using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugSpawnManager : MonoBehaviour
{
    GameManager gameManager;
    BugWrapper bugWrapper;
    BugAppearingClassWrapper bugAppearingWrapper;
    SaveDataClass saveData;
    List<BugIngameClass> appearedBugList;
    [SerializeField]
    BugMoveManager bugMoveManager;
    [SerializeField]
    BugCatchManager bugCatchManager;

    [SerializeField]
    GameObject[] bugPrefab;
    [SerializeField]
    GameObject bugWorldParent;
    [SerializeField]
    GameObject bugCanvasParent;

    

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.inst;
        bugWrapper = gameManager.bugWrapper;
        bugAppearingWrapper = gameManager.bugAppearingWrapper;
        saveData = gameManager.saveData;
        appearedBugList = saveData.appearedBugList;
        for (int i = 0; i < appearedBugList.Count; i++)
        {
            //로드했을때는 게임오브젝트가 없어서 이걸 해줘야함.
            if ((int)appearedBugList[i].name < 2)
            {
                appearedBugList[i].SetCanvasBugPrefab(bugPrefab[(int)appearedBugList[i].name], bugCanvasParent);
                bugCatchManager.SetEventTrigger(appearedBugList[i]);
            }
            else
            {
                appearedBugList[i].SetWorldBugPrefab(bugPrefab[(int)appearedBugList[i].name], bugWorldParent);
            }
            
        }

        StartCoroutine(CoroutineStarter());
    }

    IEnumerator CoroutineStarter()
    {
        for(int  i = 0; i < 4; i++)
        {
            StartCoroutine(BugSpawnCoroutine((BugName)i));
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator BugSpawnCoroutine(BugName name)
    {
        BugAppearCondition condition = null;
        BugClass bugClass = null;
        for (int i = 0; i < bugAppearingWrapper.conditionList.Count; i++)
        {
            if (bugAppearingWrapper.conditionList[i].appearingBug == name)
            {
                condition = bugAppearingWrapper.conditionList[i];
                break;
            }
        }

        for(int i = 0; i < bugWrapper.bugList.Count; i++)
        {
            if (bugWrapper.bugList[i].name == name)
            {
                bugClass = bugWrapper.bugList[i];
                break;
            }
        }
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if(name == BugName.Mosquito)
            {
                if (saveData.isWindowOpen == false)
                {
                    continue;
                }

            }
            else
            {
                if (condition.cleannessBorder > saveData.placeCleannessNumber[(int)condition.appearingPlace])
                {
                    continue;
                }
                else
                {
                    Debug.Log(condition.appearingPlace);
                }

            }


            //여기가 확률임.
            if (Random.Range(0f, 1f) > 0.5f)
            {
                continue;
            }
            Vector3 randomPos;
            GameObject bugParent;
            if((int)name < 2)
            {
                bugParent = bugCanvasParent;
                randomPos = new Vector3(Random.Range(-700f, 700f), Random.Range(-400f, 400f),0);
            }
            else
            {
                bugParent = bugWorldParent;
                randomPos = new Vector3(Random.Range(-5f, 5f), Random.Range(-3f, 3f), 0);
            }
            BugIngameClass bug = new BugIngameClass(bugClass, bugPrefab[(int)name], bugParent, randomPos);
            appearedBugList.Add(bug);
            bugCatchManager.SetEventTrigger(bug);
            bugMoveManager.MoveBug(bug);
            GameManager.inst.SaveJson();
        }


    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
