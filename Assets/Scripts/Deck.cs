using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<Card> drawOrder, essentials;

    public GameObject gameManager;
    
    public GameObject canvas;

    public GameObject cardRender;
    public GameObject[] slots = new GameObject[7];

    public void drawCard() {        
        int i = nextFreeHandSlot();
        if(i <= 6)  {
            GameObject clone = Instantiate(cardRender, canvas.transform);
            clone.GetComponent<CardDisplay>().card = drawOrder[0];
            drawOrder.RemoveAt(0);
            clone.GetComponent<Drag>().slot = slots[i];
            slots[i].GetComponent<CardSlot>().slotOccupied = true;
            SoundManager.PlayCardSwoosh();
        }
    }

    public void drawCardEssential() {        
        int i = nextFreeHandSlot();
        if(i <= 6)  {
            GameObject clone = Instantiate(cardRender, canvas.transform);
            clone.GetComponent<CardDisplay>().card = drawOrder[0];
            drawOrder.RemoveAt(0);
            clone.GetComponent<Drag>().slot = slots[i];
            slots[i].GetComponent<CardSlot>().slotOccupied = true;
            SoundManager.PlayCardSwoosh();
        }
    }

    private void Start() {
        shuffleDeck();
        StartCoroutine(WaitForSoundSetup());
        
    }

    IEnumerator WaitForSoundSetup() {
        yield return new WaitForSeconds(1);

        drawCard();
        drawCard();
        drawCard();
        drawCard();
        drawCard();
        drawCard();
        drawCard();
    }

    public void drawUntilFull() {

        for(int i = 0; i < 7; i++) {

            if(drawOrder.Count == 0) {
                drawOrder.AddRange(gameManager.GetComponent<DungeonManager>().normalLoot);
                drawCard();
            } else {
                drawCard();
            }           
        }
        //recalcProficiency();
    }

    public int nextFreeHandSlot() {
        int i = 0;
        foreach (GameObject slot in slots)
        {
            if (!slot.GetComponent<CardSlot>().slotOccupied) break;
            i++;
        }
        return i;
    }

    public void shuffleDeck() {
        SoundManager.PlayShuffle();
        for (int i = 0; i < drawOrder.Count; i++) {

            // Pick random Element
            int j = Random.Range(i, drawOrder.Count);

            // Swap Elements
            Card tmp = drawOrder[i];
            drawOrder[i] = drawOrder[j];
            drawOrder[j] = tmp;
        }        
    }

    public void recalcProficiency() {
        switch(gameManager.GetComponent<DungeonManager>().counterCharacterType) {
                case "Knight":
                    foreach(Card card in drawOrder) {
                       if(card.knightProf == false) card.attackDamage /= 2;
                    }
                    break;
                case "Thief":
                    break;
                case "Wizard":
                    break;
                case "Monk":
                    break;
            }
        
    }

}
