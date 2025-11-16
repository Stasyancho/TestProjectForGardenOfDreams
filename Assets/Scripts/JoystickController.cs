using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Joystick Settings")]
    public RectTransform background; // Большой круг (фон)
    public RectTransform handle;     // Маленький круг (ручка)
    public float handleRange = 1f;   // Радиус движения ручки
    
    [Header("Output")]
    public Vector2 direction = Vector2.zero;
    
    private Vector2 startPos;
    private float maxDistance;
    
    void Start()
    {
        startPos = background.position;
        maxDistance = background.sizeDelta.x * 0.5f * handleRange;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position = RectTransformUtility.WorldToScreenPoint(null, background.position);
        Vector2 radius = background.sizeDelta * 0.5f;
        
        // Вычисляем направление
        direction = (eventData.position - position) / (radius * handleRange);
        
        // Ограничиваем величину вектора до 1
        if (direction.magnitude > 1)
            direction = direction.normalized;
        
        // Перемещаем ручку
        handle.anchoredPosition = direction * radius * handleRange;
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        // Сбрасываем джойстик
        direction = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }
}