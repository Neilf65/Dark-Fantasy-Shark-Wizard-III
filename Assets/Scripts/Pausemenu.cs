using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
        controls.Land.Pause.performed -= OnPausePerformed;
        controls.Land.Disable();
    }

    private void OnPausePerformed(InputAction.CallbackContext ctx)
    {
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

        music.Pause();

        if (UIAudioManager.instance != null)
            UIAudioManager.instance.PlayClick(pauseSound);

        StartCoroutine(SelectFirstButtonCoroutine());
    }

    private IEnumerator SelectFirstButtonCoroutine()
    {
        yield return null;

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

    public void ResumeButton()
    {
        if (UIAudioManager.instance != null)
            UIAudioManager.instance.PlayClick(clickSound);

        ResumeGame();
    }

    public void RestartGame()
    {
        if (UIAudioManager.instance != null)
            UIAudioManager.instance.PlayClick(clickSound);

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToTitle()
    {
        if (UIAudioManager.instance != null)
            UIAudioManager.instance.PlayClick(clickSound);

        Time.timeScale = 1f;
        isPaused = false;

        SceneManager.LoadScene("MainMenu");
    }

    public void QuitPauseMenu()
    {
        if (UIAudioManager.instance != null)
            UIAudioManager.instance.PlayClick(clickSound);

        Application.Quit();
    }
}
