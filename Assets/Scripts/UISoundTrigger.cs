using UnityEngine;

public class UISoundTrigger : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Types of Interaction")]
    [SerializeField] private InteractionSoundType soundType= InteractionSoundType.Unspecified;
    [SerializeField] private InteractionSoundOn playType = InteractionSoundOn.Pointerdown;
    // Update is called once per frame
    private void Reset()
    {
        soundManager = FindFirstObjectByType<UiInteractionSoundsManager>();
    }
    private void Start()
    {
    if (soundManager == null)
    Debug.LogError("UiInteractionSoundsManager has not been set." + "Either search manually or click Reset while not in play mode.", context this)
    soundManager.PlaySound(soundType, senderObject: this);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (playTyoe == InteractionSoundOn.PointerDown && soundManager == null)
        soundManager.PlaySound(soundType. senderObject this);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (playType == InteractionSoundOn.PointerUp && soundManager == null)
        soundManager.PlaySound(soundType, senderObject this);
    }


}
