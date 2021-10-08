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
        for(int i = 0; i < appearedBugList.Count; i++)
        {
            StartCoroutine(MoveCoroutine(appearedBugList[i]));
        }
    }

    public void MoveBug(BugIngameClass bug)
    {
        StartCoroutine(MoveCoroutine(bug));
    }

    IEnumerator MoveCoroutine(BugIngameClass bug)
    {

        Vector3 randomDeltaPos;
        Vector3 originPos = bug.bugObject.transform.position;
        if ((int)bug.name < 2)
        {
            RectTransform rect = bug.bugObject.GetComponent<RectTransform>();
            
            originPos =rect.anchoredPosition;
            randomDeltaPos =originPos+ new Vector3(Random.Range(-100f, 100f), Random.Range(-100f, 100f), 0);
            if(randomDeltaPos.x > 1280)
            {
                randomDeltaPos.x = 1280;
            }
            if(randomDeltaPos.x < -1280)
            {
                randomDeltaPos.x = -1280;
            }
            if (randomDeltaPos.y > 720)
            {
                randomDeltaPos.y = 720;
            }
            if (randomDeltaPos.y < -720)
            {
                randomDeltaPos.y = -720;
            }
            float timer = 0;
            while (bug.bugObject.activeSelf == true)
            {
                timer += Time.deltaTime;
                rect.anchoredPosition = Vector3.Lerp(originPos, randomDeltaPos, timer);
                yield return null;
                if (timer >= 1)
                {
                    originPos = rect.anchoredPosition;
                    randomDeltaPos = originPos + new Vector3(Random.Range(-100f, 100f), Random.Range(-100f, 100f), 0);
                    timer = 0;
                    if(bug.name == BugName.Mosquito)
                    {
                        bug.sitted = true;
                        yield return new WaitForSeconds(0.3f);
                        bug.sitted = false;
                    }
                }
            }

        }
        else
        {
            
            originPos = bug.bugObject.transform.position;
            randomDeltaPos = originPos + new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);
            if (randomDeltaPos.x > 8.9f)
            {
                randomDeltaPos.x = 8.9f;
            }
            if (randomDeltaPos.x < -8.9f)
            {
                randomDeltaPos.x = -8.9f;
            }
            if (randomDeltaPos.y > 4.9f)
            {
                randomDeltaPos.y = 4.9f;
            }
            if (randomDeltaPos.y < -4.9f)
            {
                randomDeltaPos.y =-4.9f;
            }
            float timer = 0;
            while (bug.bugObject.activeSelf == true)
            {
                timer += Time.deltaTime;
                bug.bugObject.transform.position = Vector3.Lerp(originPos, randomDeltaPos, timer);
                yield return null;
                if (timer >= 1)
                {
                    randomDeltaPos = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);
                    originPos = bug.bugObject.transform.position;
                    timer = 0;
                }
            }
        }


    }

    //// Update is called once per frame
    //void Update()
    //{
    //    for(int i = 0; i < appearedBugList.Count; i++)
    //    {

    //        if((int)appearedBugList[i].name < 2)
    //        {
    //            Vector2 deltaPos = new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f));
    //            appearedBugList[i].bugObject.GetComponent<RectTransform>().anchoredPosition += deltaPos;
                
    //        }
    //        else
    //        {
    //            Vector2 deltaPos = new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));
    //            appearedBugList[i].bugObject.transform.position += (Vector3)deltaPos;
    //        }
    //    }
    //}

    
}
