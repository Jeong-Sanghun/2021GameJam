using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BugWrapper
{
    public List<BugClass> bugList;

    public BugWrapper()
    {
        bugList = new List<BugClass>();

        BugClass bug = new BugClass();
        bug.name = BugName.Fly;
        bug.hp = 2;
        bug.isOnScreen = false;
        bugList.Add(bug);

        bug = new BugClass();
        bug.name = BugName.Roach;
        bug.hp = 5;
        bug.isOnScreen = false;
        bugList.Add(bug);

        bug = new BugClass();
        bug.name = BugName.Mosquito;
        bug.hp = 2;
        bug.isOnScreen = true;
        bugList.Add(bug);

        bug = new BugClass();
        bug.name = BugName.MossFly;
        bug.hp = 1;
        bug.isOnScreen = true;
        bugList.Add(bug);
    }
}
