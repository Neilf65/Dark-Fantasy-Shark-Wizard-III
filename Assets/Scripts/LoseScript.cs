using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoseScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TMP_Text pointsText;
    public void Setup(int AttemptManager)
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
        pointsText.text = AttemptManager.ToString() + "Attempts";
    }
}
