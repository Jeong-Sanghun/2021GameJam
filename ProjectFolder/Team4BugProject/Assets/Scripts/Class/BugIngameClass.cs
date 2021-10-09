
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BugIngameClass
{
    public BugName name;
    [System.NonSerialized]
    public GameObject bugObject;
    public Vector3 position;
    public int nowHp;
    [System.NonSerialized]
    public bool sitted;

    public void SetWorldBugPrefab(GameObject prefab,GameObject parent)
    {
        bugObject = GameObject.Instantiate(prefab, parent.transform);
        bugObject.transform.localPosition = position;
    }

    public void SetCanvasBugPrefab(GameObject prefab, GameObject parent)
    {
        bugObject = GameObject.Instantiate(prefab, parent.transform);
        bugObject.GetComponent<RectTransform>().anchoredPosition = position;
    }

    public BugIngameClass()
    {
        name = BugName.Fly;
        bugObject = null;
        position = Vector3.zero;
        sitted = false;
    }

    public BugIngameClass(BugClass bug, GameObject prefab, GameObject parent, Vector3 pos)
    {
        name = bug.name;
        nowHp = bug.hp;
        position = pos;
        if ((int)bug.name < 2)
        {
            SetCanvasBugPrefab(prefab, parent);
        }
        else
        {
            SetWorldBugPrefab(prefab, parent);
        }
        

        sitted = false;

    }
}
