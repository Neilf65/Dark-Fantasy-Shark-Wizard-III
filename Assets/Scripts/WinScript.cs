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
        // Wait two frames to ensure UI is active
        yield return null;
        yield return null;

        if (EventSystem.current != null && firstButton != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstButton.gameObject);
        }
    }
    public void ReplayWin()
    {
        AttemptManager.Instance.ResetAttempts();
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Current Scene Index: " + currentIndex);

        SceneManager.LoadScene(currentIndex);
    }

}

