using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IPointerDownHandler, IEndDragHandler
{
    private Vector2 pointerOffset;
    private RectTransform rectTransform;
    private RectTransform rectTransformSlot;
    private CanvasGroup canvasGroup;
    private RectTransform uiRoot;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransformSlot = transform.parent.GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        uiRoot = GameObject.FindGameObjectWithTag("UI").GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData data)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, data.position, data.pressEventCamera, out pointerOffset);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData data)
    {
        if(transform.parent != uiRoot)
        {
            gameObject.transform.SetParent(uiRoot);
        }

        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(uiRoot, Input.mousePosition, data.pressEventCamera, out localPointerPosition))
        {
            rectTransform.localPosition = localPointerPosition - pointerOffset;
        }
    }

    public void OnEndDrag(PointerEventData data)
    {
        gameObject.transform.SetParent(rectTransformSlot.transform);
        canvasGroup.blocksRaycasts = true;
        rectTransform.anchoredPosition = Vector3.zero;
        Inventory.inventory.SwapItems(GetComponentInParent<Slot>().slotIndex, data.pointerEnter.GetComponentInParent<Slot>().slotIndex);
    }


}