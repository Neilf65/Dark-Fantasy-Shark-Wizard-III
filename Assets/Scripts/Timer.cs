using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;

    float elapsedTime;
    bool measureTime = true;

    void Update()
    {
        if (!measureTime) return;

        elapsedTime += Time.deltaTime;
        UpdateTimerDisplay();
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 1000f) % 1000f);

        timerText.text = $"{minutes:00}:{seconds:00}:{milliseconds:000}";
    }
    public string GetFormattedTime()
    {
    int minutes = Mathf.FloorToInt(elapsedTime / 60f);
    int seconds = Mathf.FloorToInt(elapsedTime % 60f);
    int milliseconds = Mathf.FloorToInt((elapsedTime * 1000f) % 1000f);

    return $"{minutes:00}:{seconds:00}:{milliseconds:000}";
    }

    public float GetMeasuredTime()
    {
        return elapsedTime;
    }

    public void StartTimer()
    {
        measureTime = true;
    }

    public void StopTimer()
    {
        measureTime = false;
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
        UpdateTimerDisplay();
    }
}
