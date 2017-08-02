using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MoveToFront : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData data)
    {
        SendToFront();
    }

    public void SendToFront()
    {
        transform.SetAsLastSibling();
    }
}
