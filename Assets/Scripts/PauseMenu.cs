using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool isPaused = false;
    public GameObject pauseMenuUi;
    public GameObject creditsUi;

    // Start is called before the first frame update
    void Start() {
        Pause();
    }


    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                Resume();
            } else {
                Pause();
            }
        }
    }

    public void Resume() {
        pauseMenuUi.SetActive(false);
        creditsUi.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause() {
        pauseMenuUi.SetActive(true);
        creditsUi.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ShowCredits() {
        Debug.Log("Showing credits...");
        creditsUi.SetActive(!creditsUi.activeSelf);
    }

    public void QuitGame() {
        Debug.Log("Exiting.");
        Application.Quit();
    }
}
