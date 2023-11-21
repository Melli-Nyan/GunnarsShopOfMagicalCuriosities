using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Abenteuerlust/Card")]
public class Card : ScriptableObject
{
    public new string name;
    public string effect;
    public string flavourText;

    public Sprite artwork;
    public Sprite baseColor;

    public int attackDamage;
    public int armor;
    public int value;

    public int duration;

    public int extraLoot;
    public int extraChoose;

    public int numGoodCards;
    public int numBestCards;

    public bool knightProf;
    public bool archerProf;
    public bool wizardProf;
    public bool monkProf;
}
