using UnityEngine;

public class AttemptManager : MonoBehaviour
{
    public static AttemptManager Instance;

    private int attempts;
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
}
