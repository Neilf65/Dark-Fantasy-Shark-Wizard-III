using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    public void Playgame()
    {
        SceneManager.LoadSceneAsync("SampleScene");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
