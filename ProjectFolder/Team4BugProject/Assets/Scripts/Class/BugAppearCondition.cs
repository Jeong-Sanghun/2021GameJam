using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� �ϳ��� �� ��ҿ� ��Ÿ���µ� �ʿ��� �����
public class BugAppearCondition
{
    public BugName appearingBug;            //��Ÿ���� ���� �̸�
    public PlaceName appearingPlace;        //��Ÿ���� ��� �̸�
    public float cleannessBorder;           //��谪.
    //����� ���.

    public BugAppearCondition()
    {
        appearingBug = BugName.Fly;
        appearingPlace = PlaceName.RecycleBin;
        cleannessBorder = 0;
    }

}
