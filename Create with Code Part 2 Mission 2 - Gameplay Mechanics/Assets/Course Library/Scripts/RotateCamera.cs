using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float rotationSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Multiply by -1 to invert rotation - i.e. left key rotates clockwise
        // float horizontalInput = Input.GetAxis("Horizontal");
        
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}