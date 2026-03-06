using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Pausemenu : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject firstButton;

    private PlayerControls controls;
    private bool isPaused = false;
    private bool pauseRequested = false;

    void Awake()
    {
        controls = new PlayerControls();
    }

    void OnEnable()
    {
        controls.Land.Enable();
        controls.Land.Pause.performed += OnPausePerformed;
    }

    void OnDisable()
    {
     
        controls.Land.Disable();
        controls.Land.Pause.performed -= OnPausePerformed;
    }

    private void OnPausePerformed(InputAction.CallbackContext ctx)
    {
        // Safe: set flag, do NOT modify UI here
        pauseRequested = true;
    }

    void Start()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (pauseRequested)
        {
            pauseRequested = false;

            if (!LoseManager.isGameOver)
            {
                TogglePause();
            }
        }
    }

    private void TogglePause()
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Select first button for controller
        StartCoroutine(SelectFirstButtonCoroutine());
    }

    private IEnumerator SelectFirstButtonCoroutine()
    {
        yield return null; // wait one frame for UI to activate
        EventSystem.current.SetSelectedGameObject(null);
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
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToTitle()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitPauseMenu()
    {
        Application.Quit();
    }
}
