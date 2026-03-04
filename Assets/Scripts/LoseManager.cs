using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;


public class LoseManager : MonoBehaviour
{
    public static LoseManager manager;

    public static bool isGameOver;  

    [SerializeField] private GameObject LoseScreen;

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
        isGameOver = true;
        LoseScreen.SetActive(true);   
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
