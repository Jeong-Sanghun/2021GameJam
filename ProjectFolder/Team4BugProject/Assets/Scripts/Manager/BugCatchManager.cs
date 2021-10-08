using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BugCatchManager : MonoBehaviour
{
    GameManager gameManager;
    SaveDataClass saveData;
    BugWrapper bugWrapper;
    List<BugIngameClass> appearedBugList;
    [SerializeField]
    Transform handTransform;

    private void Start()
    {
        gameManager = GameManager.inst;
        saveData = gameManager.saveData;
        appearedBugList = saveData.appearedBugList;


        bugWrapper = gameManager.bugWrapper;
    }
    public void SetEventTrigger(BugIngameClass bug)
    {
        if ((int)bug.name < 2)
        {
            EventTrigger eventTrigger = bug.bugObject.GetComponent<EventTrigger>();

            //��ư �̺�Ʈ
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerDown;
            entry.callback.AddListener((data) => { OnCatchEvent((PointerEventData)data, bug); });
            eventTrigger.triggers.Add(entry);
        }
    }

    public void OnCatchButton()
    {
        if (appearedBugList.Count == 0)
        {
            return;
        }
        int leastIndex = -1;
        float leastMagnitude = 50000;
        for (int i = 0; i < appearedBugList.Count; i++)
        {
            if ((int)appearedBugList[i].name < 2)
            {
                continue;
            }
            float mag = Vector3.SqrMagnitude(handTransform.position - appearedBugList[i].bugObject.transform.position);
            if (leastMagnitude > mag)
            {
                leastIndex = i;
                leastMagnitude = mag;
            }
        }
        if (leastIndex == -1)
        {
            return;
        }
        HitBug(appearedBugList[leastIndex]);
    }

    void OnCatchEvent(PointerEventData data, BugIngameClass bug)
    {
        HitBug(bug);
    }

    void HitBug(BugIngameClass bug)
    {
        BugClass bugClass = null;
        for (int i = 0; i < bugWrapper.bugList.Count; i++)
        {
            if (bugWrapper.bugList[i].name == bug.name)
            {
                bugClass = bugWrapper.bugList[i];
                break;
            }
        }
        if (bugClass == null)
        {
            return;
        }

        bug.nowHp--;

        if (bug.nowHp <= 0)
        {
            bug.bugObject.SetActive(false);
            appearedBugList.Remove(bug);
        }
        else
        {
            float col = (float)bug.nowHp / bugClass.hp;
            Color red = new Color(1, col, col);
            if ((int)bug.name < 2)
            {
                bug.bugObject.GetComponent<Image>().color = red;
            }
            else
            {
                bug.bugObject.GetComponent<SpriteRenderer>().color = red;
            }
        }
    }
}