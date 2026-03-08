using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    public static bool isWin;
    [SerializeField] private TMP_Text finalTimeText;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private Timer timer;
    [SerializeField] private AttemptManager attemptManager;
    [SerializeField] private Button firstButton;


    private void Awake()
    {
        manager = this;
        isWin = false;
        Time.timeScale = 1f;
    }

    private void Start()
    {
        winScreen.SetActive(false);
    }

    public void Win()
    {
        if (isWin) return;

        isWin = true;

        timer.StopTimer();

        finalTimeText.text = "Time: " + timer.GetFormattedTime();

        winScreen.SetActive(true);

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        StartCoroutine(SelectFirstButtonCoroutine());
    }
    private IEnumerator SelectFirstButtonCoroutine()
    {
        // Wait a few frames to let UI activate
        yield return null;
        yield return null;
        yield return null;

        // Ensure EventSystem exists
        if (EventSystem.current == null)
        {
            Debug.LogError("No EventSystem found in the scene!");
            yield break;
        }

        if (firstButton == null)
        {
            Debug.LogError("First button is not assigned!");
            yield break;
        }

        
        firstButton.interactable = true;

        
        EventSystem.current.SetSelectedGameObject(null);

        
        EventSystem.current.SetSelectedGameObject(firstButton.gameObject);

        
        firstButton.OnSelect(null);
    }
    public void ReplayWin()
    {
        AttemptManager.Instance.ResetAttempts();
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Current Scene Index: " + currentIndex);

        SceneManager.LoadScene(currentIndex);
    }
    public void ReturnToTitle()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitWin()
    {
        Application.Quit();
    }
}

