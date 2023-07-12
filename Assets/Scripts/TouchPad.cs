using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchPad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Vector2 origin;
    private Vector2 direction;
    private Vector2 smoothDirection;
    private float smoothing = 0.05f;

    private int pointerID;
    private bool touched;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        direction = Vector2.zero;
        touched = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!touched)
        {
            touched = true;
            origin = eventData.position;
            pointerID = eventData.pointerId;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.pointerId == pointerID)
        {
            Vector2 currentPosition = eventData.position;
            Vector2 directionRaw = currentPosition - origin;
            direction = directionRaw.normalized;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId == pointerID)
        {
            direction = Vector2.zero;
            touched = false;
        }
    }

    public Vector2 GetDirection()
    {
        smoothDirection = Vector2.MoveTowards(smoothDirection, direction, smoothing);
        return smoothDirection;
    }
}
