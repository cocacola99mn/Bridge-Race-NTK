using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoystickInput : Singleton<JoystickInput>, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Image JoystickBackground;

    public Vector2 posInput;

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            JoystickBackground.rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out posInput)) ;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        posInput = Vector2.zero;
    }

    public float inputHorizontal()
    {
        if (posInput.x != 0)
            return posInput.x;
        else
            return Input.GetAxis(GameConstant.HORIZONTAL_AXIS);
    }

    public float inputVertical()
    {
        if (posInput.y != 0)
            return posInput.y;
        else
            return Input.GetAxis(GameConstant.VERTICAL_AXIS);
    }
}
