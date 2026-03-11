using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioClip clickSound;

    public void Playgame()
    {
        AttemptManager.Instance.ResetAttempts();

        if (UIAudioManager.instance != null)
            UIAudioManager.instance.PlayClick(clickSound);

        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        if (UIAudioManager.instance != null)
            UIAudioManager.instance.PlayClick(clickSound);

        Application.Quit();
    }
}
