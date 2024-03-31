using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float _horizontalInput;
    private const float XRange = 10.0f;
    
    public float speed = 10.0f;
    public GameObject projectilePrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        var playerPosition = transform.position;

        // Handle current position of player
        switch (playerPosition.x)
        {
            // Player attempting to move past bound on LHS of screen
            case < -XRange:
                transform.position = new Vector3(-XRange, playerPosition.y, playerPosition.z);
                break;
            // Player attempting to move past bound on LHS of screen
            case > XRange:
                transform.position = new Vector3(XRange, playerPosition.y, playerPosition.z);
                break;
            // Player attempting to move within acceptable bounds of screen
            default:
                _horizontalInput = Input.GetAxis("Horizontal");
                transform.Translate(Vector3.right * (Time.deltaTime * speed * _horizontalInput));
                break;
        }

        // Handle player firing projectile
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Launch a projectile from the player
            Instantiate(projectilePrefab, playerPosition, projectilePrefab.transform.rotation);
        }
    }
}
