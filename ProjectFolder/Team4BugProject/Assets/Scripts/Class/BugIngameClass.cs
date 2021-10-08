
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugIngameClass
{
    public BugName name;
    [System.NonSerialized]
    public GameObject bugObject;
    public Vector3 position;

    public void SetBugPrefab(GameObject prefab,GameObject parent)
    {
        bugObject = GameObject.Instantiate(prefab, parent.transform);
        bugObject.transform.localPosition = position;
    }

    public BugIngameClass()
    {
        name = BugName.Fly;
        bugObject = null;
        position = Vector3.zero;
    }

    public BugIngameClass(BugName _name, GameObject prefab, GameObject parent,Vector3 pos)
    {
        name = _name;
        SetBugPrefab(prefab, parent);
        position = pos;

    }
}
