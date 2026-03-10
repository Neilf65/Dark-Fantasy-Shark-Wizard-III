using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathSpike : MonoBehaviour
{
    [SerializeField] private float despawnTimer;
    public bool isSpawned;

    void Start()
    {
        despawnTimer = 3.0f;
        isSpawned = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpawned != false)
        despawnTimer -= Time.deltaTime;

        if (despawnTimer < 0.01f)
        {
            Destroy(gameObject);
        }
    }
}
