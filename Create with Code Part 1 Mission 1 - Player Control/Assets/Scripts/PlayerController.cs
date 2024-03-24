using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _speed = 20.0f;
    private float _turnSpeed = 45.0f;
    
    public float horizontalInput;
    public float forwardInput;
    
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        // Get movement input
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
        
        // Move the vehicle forwards/backwards (Based on vertical input)
        transform.Translate(Vector3.forward * (Time.deltaTime * _speed * forwardInput));
        
        // Turn the vehicle left/right (Based on horizontal input)
        transform.Rotate(Vector3.up, Time.deltaTime * _turnSpeed * horizontalInput);
    }
}
