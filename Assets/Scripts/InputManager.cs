using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    private void Update() {
        if(Input.GetKeyDown("h")) {
            if(canvas.enabled) {
                canvas.enabled = false; 
            } else {
                canvas.enabled = true;
            }
        }
    }
}
