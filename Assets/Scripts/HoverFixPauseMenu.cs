using UnityEngine;
using UnityEngine.EventSystems;

public class HoverFixPauseMenu : MonoBehaviour
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
}
