using System.Collections;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pausemenu : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject firstButton;
    [SerializeField] private AudioSource music;

    private PlayerControls controls;
    private bool isPaused = false;
    private bool pauseRequested = false;
    public AudioClip clickSound;
    public AudioClip pauseSound;

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
        music.Pause();
        if (UIAudioManager.instance != null)
            UIAudioManager.instance.PlayClick(pauseSound);
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

        music.UnPause();
    }

    // Called by Resume BUTTON (plays sound)
    public void ResumeButton()
    {
        if (UIAudioManager.instance != null)
            UIAudioManager.instance.PlayClick(clickSound);

        ResumeGame();
    }

    private IEnumerator ResumeRoutine()
    {
        if (UIAudioManager.instance != null)
            UIAudioManager.instance.PlayClick(clickSound);

        yield return null; 

        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        music.UnPause();
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (UIAudioManager.instance != null)
        {
            UIAudioManager.instance.PlayClick(clickSound);
        }
    }

    public void ReturnToTitle()
    {
        if (UIAudioManager.instance != null)
            UIAudioManager.instance.PlayClick(clickSound);

        Time.timeScale = 1f; // unpause
        isPaused = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitPauseMenu()
    {
        Application.Quit();
        if (UIAudioManager.instance != null)
        {
            UIAudioManager.instance.PlayClick(clickSound);
        }
    }
}
