using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queue : MonoBehaviour
{
    public List<Adventurer> queueOrder;
    public GameObject queueslot1;
    public GameObject queueslot2;
    public GameObject queueslot3;
    public GameObject counterslot;
    public GameObject exitslot;

    public GameObject gameManager;

    public GameObject dungeonslot;
    public GameObject hiddenQueueslot;

    public bool counterIsFree;

    public List<GameObject> characters;

    public Adventurer knight,monk,wizard,archer;

    // Start is called before the first frame update
    void Start()
    {
        counterIsFree = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void advanceQueue() {
        if(counterIsFree) {
            characters[0].GetComponent<CharacterDisplay>().queueSlot = counterslot;
            characters[1].GetComponent<CharacterDisplay>().queueSlot = queueslot1;
            characters[2].GetComponent<CharacterDisplay>().queueSlot = queueslot2;
            characters[3].GetComponent<CharacterDisplay>().queueSlot = queueslot3;
            characters[4].GetComponent<CharacterDisplay>().queueSlot = hiddenQueueslot;
            characters[5].GetComponent<CharacterDisplay>().queueSlot = dungeonslot;

            characters[3].transform.GetChild(0).GetComponent<script>().SetFowardLooking(true);

            int i = Random.Range(0,4);
            switch(i) {
                case 0:
                    characters[4].GetComponent<CharacterDisplay>().adventurer = knight;
                    break;
                case 1:
                    characters[4].GetComponent<CharacterDisplay>().adventurer = monk;
                    break;
                case 2:
                    characters[4].GetComponent<CharacterDisplay>().adventurer = wizard;
                    break;
                case 3:
                    characters[4].GetComponent<CharacterDisplay>().adventurer = archer;
                    break;
            }

            gameManager.GetComponent<DungeonManager>().counterCharacterType = characters[0].transform.GetChild(0).GetComponent<script>()._currentType.ToString();

            foreach (var character in characters)
            {
                character.GetComponent<CharacterDisplay>().arrivedAtSlot = false;
                character.transform.GetChild(0).GetComponent<script>().SetWalking(true);
            }
        }
        counterIsFree = false;
    }

    public void sendOnAdventure() {
        characters[0].GetComponent<CharacterDisplay>().queueSlot = exitslot;
        characters[0].GetComponent<CharacterDisplay>().arrivedAtSlot = false;
        characters[0].transform.GetChild(0).GetComponent<script>().SetFowardLooking(false);
        characters[0].transform.GetChild(0).GetComponent<script>().SetWalking(true);
        characters.Add(characters[0]);
        characters.RemoveAt(0);
        counterIsFree = true;
    }
}
