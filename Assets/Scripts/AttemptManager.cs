using UnityEngine;

public class AttemptManager : MonoBehaviour
{
    public static AttemptManager Instance;

    private int attempts = 0;
    [SerializeField] private TMPro.TextMeshProUGUI attemptCounterText;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        attemptCounterText = FindObjectOfType<TMPro.TextMeshProUGUI>();
        UpdateAttemptText();
    }

    public void IncrementAttempts()
    {
        attempts++;
        UpdateAttemptText();
    }

    private void UpdateAttemptText()
    {
        attemptCounterText.text = $"Attempts: {attempts}";
    }

    // Add this method to reset attempts
        public void ResetAttempts()
        {
            attempts = 0;
            UpdateAttemptText();
        }
}
