using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public Card card;

    public TMP_Text nameText;
    public TMP_Text effectText;
    public TMP_Text flavourText;
    public TMP_Text armorText;
    public TMP_Text attackText;
    public TMP_Text valueText;

    public Image artworkImage;

    public Image baseCardImage;

    // Start is called before the first frame update
    void Start()
    {
        nameText.text = card.name;
        effectText.text = card.effect;
        flavourText.text = card.flavourText;
        if(card.armor != 0) armorText.text = card.armor.ToString();
        if(card.attackDamage != 0) attackText.text = card.attackDamage.ToString();
        valueText.text = card.value.ToString();

        artworkImage.sprite = card.artwork;
        if(card.baseColor != null)baseCardImage.sprite = card.baseColor;
    }

    public void updateCard(Card card) {

        this.card = card;

        nameText.text = card.name;
        effectText.text = card.effect;
        flavourText.text = card.flavourText;
        armorText.text = card.armor.ToString();
        if (card.armor == 0) armorText.text = "";
        attackText.text = card.attackDamage.ToString();
        if (card.attackDamage == 0) attackText.text = "";
        valueText.text = card.value.ToString();

        artworkImage.sprite = card.artwork;
        if(card.baseColor != null)baseCardImage.sprite = card.baseColor;
    }



}
