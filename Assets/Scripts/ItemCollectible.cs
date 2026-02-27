using UnityEngine;

public class ItemCollectible : MonoBehaviour
{
    [SerializeField] private float rotationSpeedX = 0.5f;
    [SerializeField] private float rotationSpeedY = 0.5f;
    [SerializeField] private float positionY = 1f;
    PlayerController _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate collectible
        transform.Rotate(rotationSpeedX, rotationSpeedY, 0);
    

    }

    void OnTriggerEnter(Collider other)
    {
        //Collect Stun Item
        Debug.Log("Collided with the stun item!");
        _player.CollectStunItem(gameObject);
    }
}
