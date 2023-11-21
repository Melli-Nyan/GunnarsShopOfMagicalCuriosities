using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DungeonManager : MonoBehaviour
{
    public GameObject gear1;
    public GameObject gear2;
    public GameObject gear3;

    public GameObject deck;
    public GameObject questTemplate;

    public GameObject questSlot1, questSlot2, questSlot3, questSlot4;

    public Sprite knightHead, monkHead, archerHead, wizardHead;

    private Card card1, card2, card3;

    public GameObject[] activeQuests = new GameObject[4];

    public List<Card> selectedCards, normalLoot, goodLoot, bestLoot;

    public List<GameObject> lootCards;

    public Card card;

    public GameObject window;

    public GameObject playerCanvas, lootCanvas, inputBlockCanvas;

    public string counterCharacterType;

    public int chooseAmount;

    public bool attackReached;

    public TMP_Text counter;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void useGear() {
        if(gear1.GetComponent<CardSlot>().slotOccupied) {
            card1 = gear1.GetComponent<CardSlot>().slottedCard.GetComponent<CardDisplay>().card;
            Destroy(gear1.GetComponent<CardSlot>().slottedCard);
        }
        if(gear2.GetComponent<CardSlot>().slotOccupied) {
            card2 = gear2.GetComponent<CardSlot>().slottedCard.GetComponent<CardDisplay>().card;
            Destroy(gear2.GetComponent<CardSlot>().slottedCard);
        }
        if(gear3.GetComponent<CardSlot>().slotOccupied) {
            card3 = gear3.GetComponent<CardSlot>().slottedCard.GetComponent<CardDisplay>().card;
            Destroy(gear3.GetComponent<CardSlot>().slottedCard);
        }
    }

    public void beginDungeoneering() {
        card1 = card2 = card3 = null;
        useGear();
        GameObject quest = null;
        if(!questSlot1.GetComponent<QuestSlot>().occupied) {
            quest = Instantiate(questTemplate, questSlot1.transform, true);
            quest.GetComponent<QuestDisplay>().SetLoot(card1, card2, card3);
            questSlot1.GetComponent<QuestSlot>().occupied = true;
            activeQuests[0] = quest;

        } else if(!questSlot2.GetComponent<QuestSlot>().occupied) {
            quest = Instantiate(questTemplate, questSlot2.transform, true);
            quest.GetComponent<QuestDisplay>().SetLoot(card1, card2, card3);
            questSlot2.GetComponent<QuestSlot>().occupied = true;
            activeQuests[1] = quest;

        } else if(!questSlot3.GetComponent<QuestSlot>().occupied) {
            quest = Instantiate(questTemplate, questSlot3.transform, true);
            quest.GetComponent<QuestDisplay>().SetLoot(card1, card2, card3);
            questSlot3.GetComponent<QuestSlot>().occupied = true;
            activeQuests[2] = quest;

        } else if(!questSlot4.GetComponent<QuestSlot>().occupied) {
            quest = Instantiate(questTemplate, questSlot4.transform, true);
            quest.GetComponent<QuestDisplay>().SetLoot(card1, card2, card3);
            questSlot4.GetComponent<QuestSlot>().occupied = true;
            activeQuests[3] = quest;
        }

        quest.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,0,0);
        quest.GetComponent<QuestDisplay>().SetDuration(calcDuration(card1, card2, card3));



        switch (counterCharacterType) {
            case "Archer":
                quest.GetComponent<QuestDisplay>().SetAvatar(archerHead);
                break;
            case "Knight":
                quest.GetComponent<QuestDisplay>().SetAvatar(knightHead);
                break;
            case "Wizard":
                quest.GetComponent<QuestDisplay>().SetAvatar(wizardHead);
                break;
            case "Monk":
                quest.GetComponent<QuestDisplay>().SetAvatar(monkHead);
                break;
            default:
                break;
        }  
    }

    public void decreaseQuestTimers() {
        for(int i = 0; i < 4; i++)
        {
            if(activeQuests[i] != null) DecreaseDuration(activeQuests[i], i);
        }
    }

    public void DecreaseDuration(GameObject quest, int i) {
        if(quest.GetComponent<QuestDisplay>().duration > 1) {
            quest.GetComponent<QuestDisplay>().duration--;
            quest.GetComponent<QuestDisplay>().counterText.text = quest.GetComponent<QuestDisplay>().duration.ToString();
        } else {
            
            switch(i) {

                case 0:
                    questSlot1.GetComponent<QuestSlot>().occupied = false;
                    break;
                case 1:
                    questSlot2.GetComponent<QuestSlot>().occupied = false;
                    break;
                case 2:
                    questSlot3.GetComponent<QuestSlot>().occupied = false;
                    break;
                case 3:
                    questSlot4.GetComponent<QuestSlot>().occupied = false;
                    break;
            }
            activeQuests[i] = null;
            payoutQuest(quest);
            Destroy(quest);
        }
    }

    public void payoutQuest(GameObject quest) {
        window.GetComponent<Animator>().SetTrigger("Open");
        window.GetComponent<Animator>().SetTrigger("Arrive");

        

        int attackDamage = calcAttack(quest);
        int defence = calcDefence(quest);
        int cardsRewarded = calcLootAmount(quest);
        chooseAmount = calcChosableLoot(quest);
        int goodCards =  calcGoodLoot(quest);
        int bestCards = calcBestLoot(quest);

        counter.text = chooseAmount.ToString();


        if(!attackReached) {

            cardsRewarded /= 2;
        }

        for(int i = 0; i <= 8; i++) {

            if(i < cardsRewarded) {
                lootCards[i].SetActive(true);
                if(bestCards > 0) {

                    int joe = Random.Range(0, bestLoot.Count);

                    
                    
                    lootCards[i].GetComponent<CardDisplay>().updateCard(bestLoot[joe]);
                    bestCards--;

                } else if (goodCards > 0) {

                    int joe = Random.Range(0, goodLoot.Count);

                    lootCards[i].GetComponent<CardDisplay>().updateCard(goodLoot[joe]);
                    goodCards--;
                } else {

                    int r = Random.Range(0, 10);
                    int joe;

                    if(r > 8) {

                        joe = Random.Range(0, bestLoot.Count);
                        lootCards[i].GetComponent<CardDisplay>().updateCard(bestLoot[i]);

                    } else if(r > 4) {

                        joe = Random.Range(0, goodLoot.Count);
                        lootCards[i].GetComponent<CardDisplay>().updateCard(goodLoot[i]);

                    } else {

                        joe = Random.Range(0, normalLoot.Count);
                        lootCards[i].GetComponent<CardDisplay>().updateCard(normalLoot[i]);
                    }
                }

            } else {
                lootCards[i].SetActive(false);
            }
        }
        
        inputBlockCanvas.SetActive(true);
        StartCoroutine(WaitForBirdArrive());
    }

    IEnumerator WaitForBirdArrive() {

        SoundManager.PlayOwl();
        yield return new WaitForSeconds(1);

        inputBlockCanvas.SetActive(false);

        lootCanvas.SetActive(true);
        playerCanvas.SetActive(false);
    }

    public void chooseLoot() {
        Debug.Log(chooseAmount);
        if(selectedCards.Count < chooseAmount) {
            deck.GetComponent<Deck>().drawOrder.AddRange(selectedCards);
            deck.GetComponent<Deck>().shuffleDeck();

            lootCanvas.SetActive(false);
            playerCanvas.SetActive(true);


            selectedCards.Clear();

            window.GetComponent<Animator>().SetTrigger("Leave");
            //window.GetComponent<Animator>().SetTrigger("Close");
            StartCoroutine(WaitForBirdLeave());
        }
    }

    IEnumerator WaitForBirdLeave() {
        yield return new WaitForSeconds(1);

        window.GetComponent<Animator>().SetTrigger("Close");
    }

    public int calcDuration(Card card1, Card card2, Card card3) {
        int duration = 3;
        if(card1 != null) duration += card1.duration;
        if(card2 != null) duration += card2.duration;
        if(card3 != null) duration += card3.duration;
        return duration;
    }

    public int calcAttack(GameObject quest) {
        int attackDamage = 0;
        
        if(quest.GetComponent<QuestDisplay>().card1 != null) {
            int tempAttack = quest.GetComponent<QuestDisplay>().card1.attackDamage;
            if(!checkTypematch(card1, counterCharacterType)) {
                tempAttack /= 2;
            }
            attackDamage += tempAttack;
        }
        if(quest.GetComponent<QuestDisplay>().card2 != null) {
            int tempAttack = quest.GetComponent<QuestDisplay>().card2.attackDamage;
            if(!checkTypematch(card2, counterCharacterType)) {
                tempAttack /= 2;
            }
            attackDamage += tempAttack;
        }
        if(quest.GetComponent<QuestDisplay>().card3 != null) {
            int tempAttack = quest.GetComponent<QuestDisplay>().card3.attackDamage;
            if(!checkTypematch(card3, counterCharacterType)) {
                tempAttack /= 2;
            }
            attackDamage += tempAttack;
        }

        return attackDamage;
    }

    public bool checkTypematch(Card card, string currentChar) {



        switch(currentChar) {
                case "Knight":
                    if(card.knightProf) {
                        return true;
                    } else return false;
                    break;
                case "Archer":
                    if(card.archerProf) {
                        return true;
                    } else return false;
                    break;
                case "Wizard":
                    if(card.wizardProf) {
                        return true;
                    } else return false;
                    break;
                case "Monk":
                    if(card.monkProf) {
                        return true;
                    } else return false;
                    break;
                default:
                    return true;
                    break;
        }

    }

    public int calcDefence(GameObject quest) {
        int defence = 0;
        if(quest.GetComponent<QuestDisplay>().card1 != null) defence += quest.GetComponent<QuestDisplay>().card1.armor;
        if(quest.GetComponent<QuestDisplay>().card2 != null) defence += quest.GetComponent<QuestDisplay>().card2.armor;
        if(quest.GetComponent<QuestDisplay>().card3 != null) defence += quest.GetComponent<QuestDisplay>().card3.armor;

        return defence;
    }

    public int calcLootAmount(GameObject quest) {
        int loot = 3;
        if(quest.GetComponent<QuestDisplay>().card1 != null) loot += quest.GetComponent<QuestDisplay>().card1.extraLoot;
        if(quest.GetComponent<QuestDisplay>().card2 != null) loot += quest.GetComponent<QuestDisplay>().card2.extraLoot;
        if(quest.GetComponent<QuestDisplay>().card3 != null) loot += quest.GetComponent<QuestDisplay>().card3.extraLoot;

        return loot;
    }

    public int calcChosableLoot(GameObject quest) {
        int choose = 2;
        if(quest.GetComponent<QuestDisplay>().card1 != null) choose += quest.GetComponent<QuestDisplay>().card1.extraChoose;
        if(quest.GetComponent<QuestDisplay>().card2 != null) choose += quest.GetComponent<QuestDisplay>().card2.extraChoose;
        if(quest.GetComponent<QuestDisplay>().card3 != null) choose += quest.GetComponent<QuestDisplay>().card3.extraChoose;

        return choose;
    }

    public int calcGoodLoot(GameObject quest) {
        int good = 0;
        if(quest.GetComponent<QuestDisplay>().card1 != null) good += quest.GetComponent<QuestDisplay>().card1.numGoodCards;
        if(quest.GetComponent<QuestDisplay>().card2 != null) good += quest.GetComponent<QuestDisplay>().card2.numGoodCards;
        if(quest.GetComponent<QuestDisplay>().card3 != null) good += quest.GetComponent<QuestDisplay>().card3.numGoodCards;

        return good;
    }

    public int calcBestLoot(GameObject quest) {
        int best = 0;
        if(quest.GetComponent<QuestDisplay>().card1 != null) best += quest.GetComponent<QuestDisplay>().card1.numBestCards;
        if(quest.GetComponent<QuestDisplay>().card2 != null) best += quest.GetComponent<QuestDisplay>().card2.numBestCards;
        if(quest.GetComponent<QuestDisplay>().card3 != null) best += quest.GetComponent<QuestDisplay>().card3.numBestCards;

        return best;
    }
}
