using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BugAppearingClassWrapper
{
    public List<BugAppearCondition> conditionList;

    public BugAppearingClassWrapper()
    {
        conditionList = new List<BugAppearCondition>();
        BugAppearCondition cond = new BugAppearCondition();
        cond.appearingBug = BugName.Fly;
        cond.appearingPlace = PlaceName.Kitchen;
        conditionList.Add(cond);

        cond = new BugAppearCondition();
        cond.appearingBug = BugName.Roach;
        cond.appearingPlace = PlaceName.RecycleBin;
        conditionList.Add(cond);


        cond = new BugAppearCondition();
        cond.appearingBug = BugName.MossFly;
        cond.appearingPlace = PlaceName.RestRoom;
        conditionList.Add(cond);

        cond = new BugAppearCondition();
        cond.appearingBug = BugName.Mosquito;
        cond.appearingPlace = PlaceName.Window;
        conditionList.Add(cond);


    }
}
