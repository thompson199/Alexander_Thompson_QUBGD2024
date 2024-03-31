using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private const float TopBound = 30;
    private const float BottomBound = -10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        // Handle object moving out of bounds - if object goes out of top/bottom boundary, destroy object
        var objectPosition = transform.position.z;

        if (objectPosition > TopBound)
        {
            Destroy(gameObject);
            
        } else if (objectPosition < BottomBound)
        {
            Debug.Log("Game Over!!!!");
            Destroy(gameObject);
        }
    }
}
