using UnityEngine;

public class TrapSpawn : MonoBehaviour
{
    // Booleans
    private bool isWaiting;

    // GameObjects
    public GameObject spikeShell;
    private float spawnTimer = 2.0f; 
    private float cooldownTimer = 3.0f;
    
    PlayerController _player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isWaiting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWaiting != true)
        {
            spawnTimer -= Time.deltaTime; // Spawn timer starts when player collides with 

            if (spawnTimer < 0.01f)
            {
                isWaiting = true;
                spawnTrap();
            }
            else
            {
                return;
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
        isWaiting = false;


        spawnTimer -= Time.deltaTime;

        
        }
    }

    public void spawnTrap()
    {
        Instantiate(spikeShell, transform.position, transform.rotation);

        spawnTimer = 2.0f;
    }
}
