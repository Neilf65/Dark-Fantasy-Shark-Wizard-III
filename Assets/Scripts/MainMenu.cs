using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    public void Playgame()
    {
        AttemptManager.Instance.ResetAttempts();
        SceneManager.LoadScene(1);

    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
