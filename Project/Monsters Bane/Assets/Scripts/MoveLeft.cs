using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private PlayerController _playerControllerScript;
    private Rigidbody _playerControllerRb;
    
    private const float _speed = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        _playerControllerRb = _playerControllerScript.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerControllerScript.GetIsPlayerTouchingObstacle() && _playerControllerRb.velocity.magnitude > 0) return;
        
        // If player is moving left, then move object right - and vice versa. Else don't move object
        float movementDirection = Input.GetAxisRaw("Horizontal");
        Vector3 movementVector = (movementDirection < 0) ? Vector3.right : (movementDirection > 0) ? Vector3.left : Vector3.zero;
        
        transform.Translate(movementVector * (Time.deltaTime * _speed));
    }
}
