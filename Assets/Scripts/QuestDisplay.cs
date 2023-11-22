using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestDisplay : MonoBehaviour
{
    public TMP_Text counterText;
    public Image headImage;

    public Card card1, card2, card3;

    public int slot, duration, lootCount, lootChooseCount;

    public string characterType;

    public void SetDuration(int initialDuration) {
        duration = initialDuration;
        counterText.text = duration.ToString();
    }

    public void SetLoot(Card card1, Card card2, Card card3) {
        this.card1 = card1;
        this.card2 = card2;
        this.card3 = card3;
    }

    public void SetAvatar(Sprite headSprite, string charType) {
        headImage.sprite = headSprite;

        characterType = charType;
    }

    
    
}
