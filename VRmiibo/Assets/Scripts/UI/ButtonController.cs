using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool pointerDown;
    public UnityEvent onPointerDown;

    public void OnPointerUp(PointerEventData eventData)
    {
        pointerDown = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
    }

    private void Update()
    {
        if (pointerDown)
        {
            if (onPointerDown != null)
            {
                onPointerDown.Invoke();
            }
        }
    }
}
