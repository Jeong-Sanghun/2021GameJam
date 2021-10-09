using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float x, y;
    private Joystick joystick;

    void Awake()
    {
        joystick = GameObject.FindObjectOfType<Joystick>();
    }

    void FixedUpdate()
    {
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            MoveControl();
        }
    }

    private void MoveControl()
    {
        Vector3 upMovement = Vector3.up * speed * Time.deltaTime * joystick.Vertical;
        Vector3 rightMovement = Vector3.right * speed * Time.deltaTime * joystick.Horizontal;
        if (transform.position.y >= 6f && joystick.Vertical > 0f)
        {
            upMovement = Vector3.zero;
        }
        if (transform.position.y <= -6f && joystick.Vertical <0f)
        {
            upMovement = Vector3.zero;
        }
        if (transform.position.x >= 12f && joystick.Horizontal > 0f)
        {
            rightMovement = Vector3.zero;
        }
        if (transform.position.x <= -12f && joystick.Horizontal < 0f)
        {
            rightMovement = Vector3.zero;
        }
        transform.position += upMovement;
        transform.position += rightMovement;
    }
}
