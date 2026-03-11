using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonPressSound : MonoBehaviour, IPointerDownHandler
{
    public AudioClip clickSound;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (UIAudioManager.instance != null)
        {
            UIAudioManager.instance.PlayClick(clickSound);
        }
    }
}
