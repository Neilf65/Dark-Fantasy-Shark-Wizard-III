using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager manager;
    public static bool isWin;
    [SerializeField] private TMP_Text finalTimeText;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private Timer timer;
     [SerializeField] private AttemptManager attemptManager;

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
    }
    public void ReplayWin()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Current Scene Index: " + currentIndex);

        SceneManager.LoadScene(currentIndex);
        attemptManager.ResetAttempts();
    }

}

