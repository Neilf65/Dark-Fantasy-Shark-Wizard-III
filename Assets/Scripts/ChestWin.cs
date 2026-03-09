using UnityEngine;
using UnityEngine.SceneManagement;

public class ChestWin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.manager.Win();
        }
    }
}
