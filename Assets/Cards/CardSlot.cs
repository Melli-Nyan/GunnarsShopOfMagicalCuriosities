using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardSlot : MonoBehaviour, IDropHandler
{
    public bool slotOccupied;
    public GameObject gameManager;
    public GameObject slottedCard;
    public void OnDrop(PointerEventData eventData) {
        if (eventData.pointerDrag != null && !slotOccupied) {

            SoundManager.PlayCardPlace();

            eventData.pointerDrag.GetComponent<Drag>().slot.GetComponent<CardSlot>().slotOccupied = false;
            eventData.pointerDrag.GetComponent<Drag>().slot = this.gameObject;
            slotOccupied = true;
            slottedCard = eventData.pointerDrag;
        }
    }

    public void freeSlot() {
        slotOccupied = false;
    }



}
