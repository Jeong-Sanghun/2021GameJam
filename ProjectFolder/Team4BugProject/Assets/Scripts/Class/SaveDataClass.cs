using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DesireName
{
    Air, Hungriness, Toileting
}

public enum PlaceName
{
    Kitchen, RestRoom, RecycleBin, Window
}


public class SaveDataClass
{
    public float[] desireNumber;
    public List<BugIngameClass> appearedBugList;
    public float[] placeCleannessNumber;
    public bool isWindowOpen;
    public float codingProgress;


    public SaveDataClass()
    {
        desireNumber = new float[] {100, 100, 100};
        placeCleannessNumber = new float[4];

        
        appearedBugList = new List<BugIngameClass>();
    }
}
