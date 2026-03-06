using UnityEngine;
using System;
using UnityEngine.UIElements;

public class KeyCollectible : MonoBehaviour




{
    [SerializeField] private float rotationSpeedX = 0.5f;
    [SerializeField] private float rotationSpeedY = 0.5f;
    [SerializeField] private float slowMultiplier = 1f;
    [SerializeField] private float length;
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
    
        // Make the item ping pong up and down in place

        float PositionPingPongY = (Mathf.PingPong(Time.time, length) - length / 2) / slowMultiplier;; 
        transform.Translate(new Vector3(0, PositionPingPongY, 0));

    }

    void OnTriggerEnter(Collider other)
    {
        //Destroy Collectible
        Debug.Log("Collided with the key collectible");
        _player.CollectKey(gameObject);

    }
}
