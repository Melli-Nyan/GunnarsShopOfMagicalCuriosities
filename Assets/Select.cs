using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Select : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    [SerializeField] private Canvas canvas; 

    public int snapSpeed;
    public GameObject slot;
    public bool selected;

    public TMP_Text selectedCount;

    public GameObject gameManager;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        rectTransform.SetAsLastSibling();
        rectTransform.localScale = rectTransform.localScale * 1.1f;
    }

    public void OnPointerExit(PointerEventData eventData) {
        rectTransform.localScale = rectTransform.localScale / 1.1f;
    }

    public void OnPointerClick(PointerEventData eventData) {
        if(selected) {
            selected = false;
            selectedCount.text = (int.Parse(selectedCount.text) - 1).ToString();
            GetComponent<Outline>().enabled = false;
            gameManager.GetComponent<DungeonManager>().selectedCards.Remove(GetComponent<CardDisplay>().card);
        } else {
            selected = true;
            selectedCount.text = (int.Parse(selectedCount.text) + 1).ToString();
            GetComponent<Outline>().enabled = true;
            gameManager.GetComponent<DungeonManager>().selectedCards.Add(GetComponent<CardDisplay>().card);
        }
    }

    public void Deselect() {
        selected = false;
        GetComponent<Outline>().enabled = false;
    }

}

