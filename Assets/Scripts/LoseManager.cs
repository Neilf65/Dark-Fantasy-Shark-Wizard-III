using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;



public class LoseManager : MonoBehaviour
{
    public static LoseManager manager;

    public static bool isGameOver;

    [SerializeField] private GameObject LoseScreen;
    [SerializeField] private Button firstButton;

    private void Awake()
    {
        manager = this;
        isGameOver = false;
        Time.timeScale = 1f;
    }

    private void Start()
    {
        LoseScreen.SetActive(false);
    }

    public void Lose()
    {
        if (isGameOver) return;

        isGameOver = true;
        LoseScreen.SetActive(true);
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
    public void ReplayGame()
    {
        Debug.Log("Replay pressed");

        Time.timeScale = 1f;
        isGameOver = false;


        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Current Scene Index: " + currentIndex);

        SceneManager.LoadScene(currentIndex);

    }
}
