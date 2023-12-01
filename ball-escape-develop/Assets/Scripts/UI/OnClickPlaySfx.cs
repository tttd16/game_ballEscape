using UnityEngine;
using UnityEngine.EventSystems;

public class OnClickPlaySfx : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        SoundManager.instance?.PlayClick();
    }
}
