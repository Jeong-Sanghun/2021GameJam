using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BugName
{
    Mosquito, Roach, Fly, MossFly
}
[System.Serializable]
public class BugClass
{
    public BugName name; //0,1,2,3 or str??
    public int hp ; //or float
    public bool isOnScreen;
    //화면위에 생기면 true


    public BugClass()
    {
        name = BugName.Fly;
        hp = 10;
        isOnScreen = false;
    }

    

}
