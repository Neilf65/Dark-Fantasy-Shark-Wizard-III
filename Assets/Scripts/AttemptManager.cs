using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class AttemptManager : MonoBehaviour
{
    public static AttemptManager Instance;

    private int attempts = 0;
    [SerializeField] private TextMeshProUGUI attemptCounterText;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        if (scene.name == "MainMenu")
        {
            ResetAttempts();
        }

        
        UpdateAttemptText();
    }

    public void IncrementAttempts()
    {
        attempts++;
        UpdateAttemptText();
    }

    public void ResetAttempts()
    {
        attempts = 0;
        UpdateAttemptText();
    }

    private void UpdateAttemptText()
    {
        if (attemptCounterText != null)
        {
            attemptCounterText.text = $"Attempts: {attempts}";
        }
    }
}
