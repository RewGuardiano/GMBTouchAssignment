using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoystickController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public float joystickRadius = 50f; // Radius within which the joystick can move

    private Vector2 joystickInput;
    private bool isJoystickActive = false;
    private Vector2 joystickCenter;
    private RectTransform joystickRect;

    public Vector2 JoystickInput => joystickInput; // Public getter for joystick input
    public bool IsJoystickActive => isJoystickActive;

    void Start()
    {
        joystickRect = GetComponent<RectTransform>();
        joystickCenter = joystickRect.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        joystickCenter = joystickRect.anchoredPosition;
        isJoystickActive = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 direction = eventData.position - (Vector2)(joystickRect.parent.GetComponent<RectTransform>().position);
        joystickInput = Vector2.ClampMagnitude(direction, joystickRadius) / joystickRadius;

        // Move the joystick image
        joystickRect.anchoredPosition = joystickCenter + joystickInput * joystickRadius;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isJoystickActive = false;
        joystickInput = Vector2.zero;
        joystickRect.anchoredPosition = joystickCenter; // Return to center
    }
}