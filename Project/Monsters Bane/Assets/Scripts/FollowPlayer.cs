using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject _mainCameraObj;
    
    private float _xOffset = 7f;
    private float _yOffset = 1.7f;
    private float _zOffset = -16f;

    private Vector3 _cameraOffset;
    
    public GameObject player;
    
    // Start is called before the first frame update
    private void Start()
    {
        _mainCameraObj = gameObject;
        _cameraOffset = new Vector3(_xOffset, _yOffset, _zOffset);
    }

    // Update is called once per frame
    private void Update()
    {
        _mainCameraObj.transform.position = player.transform.position + _cameraOffset;
    }
}
