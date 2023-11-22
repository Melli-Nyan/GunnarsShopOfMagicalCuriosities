using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterDisplay : MonoBehaviour
{
    public Adventurer adventurer;

    public Image characterSprite;

    public TMP_Text attackNeed;
    public TMP_Text defenceNeed;
    public TMP_Text availableFunds;

    public GameObject queueSlot;

    public GameObject counterSlot;

    public GameObject adventurerUI;
    public GameObject bagUI;

    public float walkSpeed;

    public bool arrivedAtSlot;
    public bool footstepsStarted;

    // Start is called before the first frame update
    void Start()
    {
        walkSpeed /= 5;
    }

    // Update is called once per frame
    void Update()
    {   
        if(!arrivedAtSlot){
            if(!footstepsStarted) {
                SoundManager.PlayFootsteps();
                footstepsStarted = true;
            }
            if(Vector3.Distance(GetComponent<RectTransform>().anchoredPosition, queueSlot.GetComponent<RectTransform>().anchoredPosition) > 0.1f) {
                GetComponent<RectTransform>().anchoredPosition = Vector3.MoveTowards(GetComponent<RectTransform>().anchoredPosition, queueSlot.GetComponent<RectTransform>().anchoredPosition, 400 * walkSpeed * Time.deltaTime);
            } else {
                arrivedAtSlot = true;
                SoundManager.StopFootsteps();
                footstepsStarted = false;
                this.transform.GetChild(0).GetComponent<script>().SetWalking(false);
                if(queueSlot == counterSlot) {
                    attackNeed.text = adventurer.attackNeed.ToString() + " Attack";
                    defenceNeed.text = adventurer.defenceNeed.ToString() + " Defence";
                    availableFunds.text = adventurer.availableFunds.ToString() + " Gold";

                    adventurerUI.SetActive(true);
                    bagUI.SetActive(true);
                }
            }
        }     
        
    }
}
