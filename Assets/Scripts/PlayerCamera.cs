using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    private GameObject player;

    void Start()
    {
        if (player == null) player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        UpdatePosition();
    }

    void UpdatePosition() {
        
        // Move the camera position to the player position
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }
}
