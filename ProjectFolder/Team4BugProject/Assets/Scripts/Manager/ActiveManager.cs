using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveManager : MonoBehaviour
{

    public GameObject GameObject;
    // Start is called before the first frame update
    
    public void ActiveTrue()
    {
        GameObject.SetActive(true);
    }
    public void ActiveFalse()
    {
        GameObject.SetActive(false);
    }
}
