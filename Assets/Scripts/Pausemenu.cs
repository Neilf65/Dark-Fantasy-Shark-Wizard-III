using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Pausemenu : MonoBehaviour
{
    [SerializeField] private GameObject firstButton;
    private PlayerControls controls;
    [SerializeField] private GameObject pauseMenu;
    private bool isPaused;

    void Awake()
    {
        controls = new PlayerControls();
    }
    void OnEnable()
    {
        controls.Land.Enable();
    }

    void OnDisable()
    {
        controls.Land.Disable();
    }
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
        if (controls.Land.Pause.triggered)
        {
            Debug.Log("Pause button pressed!");
            if (!LoseManager.isGameOver)
            {
                if (isPaused) ResumeGame();
                else PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        EventSystem.current.SetSelectedGameObject(firstButton);
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
