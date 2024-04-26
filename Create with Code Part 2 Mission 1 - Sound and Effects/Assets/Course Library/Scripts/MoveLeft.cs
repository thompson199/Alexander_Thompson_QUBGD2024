using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private PlayerController _playerControllerScript;
    
    private float _speed = 20f;
    private float _leftBoundary = -15f;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the game is not over. If so, continue to move obstacles, background etc. to left
        if (!_playerControllerScript.gameOver)
        {
            transform.Translate(Vector3.left * (Time.deltaTime * _speed));
        }

        // Check if the current object is tagged as Obstacle and its X position is less than left boundary
        if (gameObject.CompareTag("Obstacle") && transform.position.x < _leftBoundary)
        {
            Destroy(gameObject);
        }
    }
}
