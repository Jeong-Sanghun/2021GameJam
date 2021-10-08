using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//버그 하나가 한 장소에 나타나는데 필요한 컨디션
public class BugAppearCondition
{
    public BugName appearingBug;            //나타나는 버그 이름
    public PlaceName appearingPlace;        //나타나는 장소 이름
    public float cleannessBorder;           //경계값.
    //생기는 경계.

    public BugAppearCondition()
    {
        appearingBug = BugName.Fly;
        appearingPlace = PlaceName.RecycleBin;
        cleannessBorder = 0;
    }

}
