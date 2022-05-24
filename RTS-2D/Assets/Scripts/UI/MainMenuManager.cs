using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject mainMenuGO;
    [SerializeField] GameObject startMenuGO;
    [SerializeField] GameObject optionsMenuGO;

    GameObject activeCanvasGO;
    FullScreenMode screenMode;

    

    public AudioMixer mixer;

    private void Awake() {
        if (!mainMenuGO) Debug.LogError("No Main Menu Game Object set in MainMenuManager!");
        if (!startMenuGO) Debug.LogError("No Start Menu Game Object set in MainMenuManager!");
        if (!optionsMenuGO) Debug.LogError("No Options Menu Game Object set in MainMenuManager!");

        activeCanvasGO = mainMenuGO;
    }
    public void OnStartClicked() {
        mainMenuGO.SetActive(false);
        startMenuGO.SetActive(true);

        activeCanvasGO = startMenuGO;
    }
    
    public void OnOptionsClicked() {
        mainMenuGO.SetActive(false);
        optionsMenuGO.SetActive(true);

        activeCanvasGO = optionsMenuGO;
    }

    public void OnInputBack(InputAction.CallbackContext context) {
        if (!context.started) return;

        if (activeCanvasGO != mainMenuGO) GoBack();
    }

    public void GoBack() {
        activeCanvasGO.SetActive(false);
        mainMenuGO.SetActive(true);

        activeCanvasGO = mainMenuGO;
    }

    public void AcceptSettings() {
        //
        Debug.Log("Zaakceptowano zmiany!!!");
    }

    public void OnToggleFullscreen() {
        Screen.fullScreen = !Screen.fullScreen;
    }
    public void OnVolumeSliderChange(float sliderValue){
        mixer.SetFloat("MusicVol",Mathf.Log10(sliderValue));
    }

    public void StartGame() {
        SceneManager.LoadScene("FinalMap1", LoadSceneMode.Single);
    }
    public static void ExitGame() {
        Application.Quit();
    }

}
