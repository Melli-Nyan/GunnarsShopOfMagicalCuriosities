using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Adventurer", menuName = "Abenteuerlust/Adventurer")]
public class Adventurer : ScriptableObject {
    public string title;
    public int attackNeed;
    public int defenceNeed;
    public int availableFunds;

    public Sprite sprite;
}
