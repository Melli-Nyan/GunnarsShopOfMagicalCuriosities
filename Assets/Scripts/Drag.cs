using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    [SerializeField] private Canvas canvas; 

    public int snapSpeed;
    public GameObject slot;
    public bool dropped = true;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update() {
        if(dropped) {
            GetComponent<RectTransform>().anchoredPosition = Vector3.MoveTowards(GetComponent<RectTransform>().anchoredPosition, slot.GetComponent<RectTransform>().anchoredPosition, 400 * snapSpeed * Time.deltaTime);
        }
        
    }

    public void OnPointerEnter(PointerEventData eventData) {
        rectTransform.SetAsLastSibling();
        rectTransform.localScale = rectTransform.localScale * 1.1f;
    }

    public void OnPointerExit(PointerEventData eventData) {
        rectTransform.localScale = rectTransform.localScale / 1.1f;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        dropped = false;
        canvasGroup.blocksRaycasts = false;

        SoundManager.PlayCardSwoosh();

        if(slot != null) {
            slot.GetComponent<Image>().raycastTarget = true;
        }
    }

    public void OnDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) {
        dropped = true;
        canvasGroup.blocksRaycasts = true;
    }
}
