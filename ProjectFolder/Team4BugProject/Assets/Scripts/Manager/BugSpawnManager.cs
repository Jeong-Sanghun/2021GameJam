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
                appearedBugList[i].SetBugPrefab(bugPrefab[(int)appearedBugList[i].name], bugCanvasParent);
            }
            else
            {
                appearedBugList[i].SetBugPrefab(bugPrefab[(int)appearedBugList[i].name], bugWorldParent);
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
        for (int i = 0; i < bugAppearingWrapper.conditionList.Count; i++)
        {
            if (bugAppearingWrapper.conditionList[i].appearingBug == name)
            {
                condition = bugAppearingWrapper.conditionList[i];
                break;
            }
        }
        while (true)
        {
            yield return new WaitForSeconds(1f);
            //if (condition.cleannessBorder < saveData.placeCleannessNumber[(int)condition.appearingPlace])
            //{
            //    continue;
            //}
            if (Random.Range(0f, 1f) > 0.5f)
            {
                continue;
            }
            Vector3 randomPos = Vector3.zero;
            GameObject bugParent;
            if((int)name < 2)
            {
                bugParent = bugCanvasParent;
            }
            else
            {
                bugParent = bugWorldParent;
            }
            BugIngameClass bug = new BugIngameClass(name, bugPrefab[(int)name], bugParent, randomPos);
            appearedBugList.Add(bug);
        }


    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
