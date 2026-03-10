using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseManager : MonoBehaviour
{
    public static LoseManager manager;
    public static bool isGameOver;


    [SerializeField] private GameObject loseScreen;
    [SerializeField] private Button replayButton;
    [SerializeField] private Button returnButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private AudioSource music;

    private void Awake()
    {
        manager = this;
        isGameOver = false;
        loseScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Lose()
    {
        if (isGameOver) return;

        isGameOver = true;
        loseScreen.SetActive(true);
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        StartCoroutine(SelectDefaultButtonCoroutine());
    }

    private IEnumerator SelectDefaultButtonCoroutine()
    {
        yield return null;
        if (EventSystem.current != null)
            EventSystem.current.SetSelectedGameObject(null);

       
        if (replayButton != null)
            replayButton.Select();
    }

    public void ReplayGame()
    {
        Time.timeScale = 1f;
        isGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToTitle()
    {
        Time.timeScale = 1f;
        isGameOver = false;
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitLose()
    {
        Application.Quit();
    }
}
