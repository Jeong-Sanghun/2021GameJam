using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform handle;
    public RectTransform outLine;

    private float deadZone = 0;
    private float handleRange = 1;
    private Vector2 input = Vector2.zero;
    private Canvas canvas;

    public float Horizontal { get { return input.x; } }
    public float Vertical { get { return input.y; } }

    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        outLine = gameObject.GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 radius = outLine.sizeDelta / 2;
        //Debug.Log(eventData.position.x);
        Debug.Log((eventData.position - outLine.anchoredPosition) / (radius * canvas.scaleFactor));
        input = new Vector2((eventData.position - outLine.anchoredPosition).x / (radius * canvas.scaleFactor).x - 6.3f, (eventData.position - outLine.anchoredPosition).y / (radius * canvas.scaleFactor).y - 3.6f);
        HandleInput(input.magnitude, input.normalized);
        handle.anchoredPosition = input * radius * handleRange;
    }

    private void HandleInput(float magnitude, Vector2 normalised)
    {
        if (magnitude > deadZone)
        {
            if (magnitude > 1)
            {
                input = normalised;
            }
        }
        else
        {
            input = Vector2.zero;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        input = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;

    }
}
