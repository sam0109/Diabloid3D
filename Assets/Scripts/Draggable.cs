using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IPointerDownHandler, IEndDragHandler
{
    public bool isItem;
    public Transform toMove;
    private Vector2 pointerOffset;
    private RectTransform rectTransform;
    private RectTransform parentRectTransform;
    private CanvasGroup canvasGroup;
    private RectTransform uiRoot;

    void Start()
    {
        if (toMove)
        {
            rectTransform = toMove.GetComponent<RectTransform>();
            parentRectTransform = toMove.parent.GetComponent<RectTransform>();
        }
        else
        {
            rectTransform = transform.GetComponent<RectTransform>();
            parentRectTransform = transform.parent.GetComponent<RectTransform>();
        }
        canvasGroup = GetComponent<CanvasGroup>();
        uiRoot = GameObject.FindGameObjectWithTag("UI").GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData data)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, data.position, data.pressEventCamera, out pointerOffset);
        if (canvasGroup)
        {
            canvasGroup.blocksRaycasts = false;
        }
        SendMessageUpwards("SendToFront", SendMessageOptions.DontRequireReceiver);
    }

    public void OnDrag(PointerEventData data)
    {
        rectTransform.transform.SetParent(uiRoot);
        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(uiRoot, Input.mousePosition, data.pressEventCamera, out localPointerPosition))
        {
            rectTransform.localPosition = localPointerPosition - pointerOffset;
        }
    }

    public void OnEndDrag(PointerEventData data)
    {
        if (canvasGroup)
        {
            canvasGroup.blocksRaycasts = true;
        }
        rectTransform.transform.SetParent(parentRectTransform.transform);
        if (isItem)
        {
            rectTransform.anchoredPosition = Vector3.zero;

            Slot currentSlot = GetComponentInParent<Slot>();
            ItemHolder currentItemHolder = GetComponentInParent<ItemHolderUI>().myItemHolder;

            Slot newSlot = data.pointerEnter.GetComponentInParent<Slot>();
            ItemHolderUI entered = data.pointerEnter.GetComponentInParent<ItemHolderUI>();
            if (entered != null)
            {
                ItemHolder newItemHolder = entered.myItemHolder;
                ItemManager.manager.SwapItems(currentItemHolder, currentSlot.slotIndex, newItemHolder, newSlot.slotIndex);
            }
            else
            {
                Debug.Log("No target item holder.");
            }
        }
    }
}