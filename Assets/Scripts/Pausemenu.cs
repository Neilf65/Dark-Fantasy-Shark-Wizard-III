using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pausemenu : MonoBehaviour
{
    
    [SerializeField] private GameObject pauseMenu;
    private bool isPaused;
    public GameObject resumeButton;
    public GameObject restartButton;

    void Start()
    {
        // HARD RESET on play
        Time.timeScale = 1f;
        isPaused = false;
        pauseMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

        
 

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void ReturnToTitle()
     {
        Time.timeScale = 0f;
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitPauseMenu()
    {
        Application.Quit();
    }

}
