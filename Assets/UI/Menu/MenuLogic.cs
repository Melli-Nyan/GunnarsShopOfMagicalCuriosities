using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MenuLogic : MonoBehaviour
{

    //private VisualElement tutorialScreen;
    private VisualElement menuScreen;
    private VisualElement overlayScreen;
    private VisualElement tutorialScreen;

    private bool menuIsOpen;

    void Start(){
        SoundManager.RestartMusic();
    }

    void OnEnable(){

        var root = GetComponent<UIDocument>().rootVisualElement;

        // --- get the elements from the menu ---
        this.menuScreen = root.Q<VisualElement>("menu") as VisualElement;
        this.overlayScreen = root.Q<VisualElement>("overlay") as VisualElement;
        this.tutorialScreen = root.Q<VisualElement>("tutorial") as VisualElement;

        Button menuButton = root.Q<Button>("menuButton") as Button;
        
        Button playButton = root.Q<Button>("playButton") as Button;
        Button restartButton = root.Q<Button>("restartButton") as Button;
        Button tutorialButton = root.Q<Button>("tutorialButton") as Button;
        Button exitButton = root.Q<Button>("exitButton") as Button;
        
        Button returnButton = root.Q<Button>("returnButton") as Button;

        // --- implement buttonClick events ---
        menuButton.clicked += () => openMenu();

        playButton.clicked += () => resumeGame();
        restartButton.clicked += () => restartGame();
        tutorialButton.clicked += () => openTutorial();
        exitButton.clicked += () => exitGame();

        returnButton.clicked += () => openMenu();



        menuScreen.style.display = DisplayStyle.None;
        overlayScreen.style.display = DisplayStyle.Flex; //
        tutorialScreen.style.display = DisplayStyle.None;
        

        menuIsOpen = false;
    }

    void Update()
    {
        
            if (Input.GetKeyDown(KeyCode.Escape) && !menuIsOpen){
                openMenu();
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && menuIsOpen){
                resumeGame();
            } 

    }
  
    void openMenu(){
        menuScreen.style.display = DisplayStyle.Flex;
        overlayScreen.style.display = DisplayStyle.None; //
        tutorialScreen.style.display = DisplayStyle.None;

        menuIsOpen = true;
    }
    void resumeGame(){
        menuScreen.style.display = DisplayStyle.None;
        overlayScreen.style.display = DisplayStyle.Flex; //

        menuIsOpen = false;
    }
    void restartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void openTutorial(){
        menuScreen.style.display = DisplayStyle.None;
        tutorialScreen.style.display = DisplayStyle.Flex;
    }

    void exitGame(){
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
