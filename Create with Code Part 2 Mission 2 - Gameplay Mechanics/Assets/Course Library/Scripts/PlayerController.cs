using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private Vector3 powerupPositionOffset = new Vector3(0, -0.5f, 0);
    private float outOfBoundsY = -10f;
    
    public float speed;
    public float powerupStrength = 15.0f;
    public bool hasPowerup;
    public GameObject powerupIndicator;
    
    // Start is called before the first frame update
    void Start()
    {
        focalPoint = GameObject.Find("Focal Point");
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move player relative to the focal point
        float forwardInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        playerRb.AddForce(focalPoint.transform.forward * (forwardInput * speed));
        playerRb.AddForce(focalPoint.transform.right * (horizontalInput * speed));

        // Ensure that powerup indicator remains around the player
        powerupIndicator.transform.position = transform.position + powerupPositionOffset;

        if (transform.position.y < outOfBoundsY)
        {
            StopGame();
        }
        
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    private void StopGame()
    {
        Destroy(gameObject);
        Destroy(powerupIndicator);
        Debug.Log("Game Over!!!!");
        
        // Check if we are running in the Unity Editor
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // Otherwise, quit the application
        Application.Quit();
        #endif
    }

    // This function will handle when player collides the trigger of another game object
    private void OnTriggerEnter(Collider collidedObject)
    {
        if (collidedObject.CompareTag("Powerup"))
        {
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true);
            
            Destroy(collidedObject.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    // This function will start the countdown routine for the powerup
    private IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(4);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    // This function will handle when player collides with another game object
    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedObject = collision.gameObject;
        
        if (collidedObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidBody = collidedObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collidedObject.transform.position - transform.position;
            
            enemyRigidBody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            
            Debug.Log($"Collided with: {collidedObject.name} with powerup set to {hasPowerup}");
        }
    }
}
