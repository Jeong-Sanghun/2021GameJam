using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugMoveManager : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;
    SaveDataClass saveData;
    List<BugIngameClass> appearedBugList;
    const float mosquitoRoachMoveRange = 200f;
    const float flyMossFlyMoveRange = 1f;

    // Start is called before the first frame update
    void Start()
    {
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
        yield return new WaitForSeconds(Random.Range(0f, 1f));
        Vector3 randomDeltaPos;
        Vector3 originPos = bug.bugObject.transform.position;
        if ((int)bug.name < 2)
        {
            RectTransform rect = bug.bugObject.GetComponent<RectTransform>();
            
            originPos =rect.anchoredPosition;
            if(bug.name == BugName.Mosquito)
            {
                randomDeltaPos = originPos + new Vector3(Random.Range(-mosquitoRoachMoveRange,
    mosquitoRoachMoveRange), Random.Range(-mosquitoRoachMoveRange, mosquitoRoachMoveRange), 0);
            }
            else
            {
                randomDeltaPos = originPos + new Vector3(Random.Range(-mosquitoRoachMoveRange/2,
    mosquitoRoachMoveRange/2), Random.Range(-mosquitoRoachMoveRange/2, mosquitoRoachMoveRange/2), 0);
            }

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
                    if (bug.name == BugName.Mosquito)
                    {
                        randomDeltaPos = originPos + new Vector3(Random.Range(-mosquitoRoachMoveRange,
            mosquitoRoachMoveRange), Random.Range(-mosquitoRoachMoveRange, mosquitoRoachMoveRange), 0);
                    }
                    else
                    {
                        randomDeltaPos = originPos + new Vector3(Random.Range(-mosquitoRoachMoveRange / 2,
            mosquitoRoachMoveRange / 2), Random.Range(-mosquitoRoachMoveRange / 2, mosquitoRoachMoveRange / 2), 0);
                    }
                    timer = 0;
                    bug.position = originPos;
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
            randomDeltaPos = originPos + new Vector3(Random.Range(-flyMossFlyMoveRange
                , flyMossFlyMoveRange), Random.Range(-flyMossFlyMoveRange, flyMossFlyMoveRange), 0);
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
                    randomDeltaPos = originPos + new Vector3(Random.Range(-flyMossFlyMoveRange
                , flyMossFlyMoveRange), Random.Range(-flyMossFlyMoveRange, flyMossFlyMoveRange), 0);
                    originPos = bug.bugObject.transform.position;
                    bug.position = originPos;
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
