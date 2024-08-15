using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private PlayerController _playerControllerScript;
    private Vector3 _startPosition;
    private float _repeatWidth; // 19.2

    void Start()
    {
        _startPosition = transform.position;
        _playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        _repeatWidth = GetComponent<BoxCollider>().size.x * 1/4;
    }

    void LateUpdate()
    {
        Vector3 playerPos = _playerControllerScript.gameObject.transform.position;
        Vector3 backgroundPos = gameObject.transform.position;
        const float repeatDiff = 0.1f;

        // Calculate the absolute difference between player X position, and left/right repeat points of background
        float leftPositionDiff = AbsoluteDiff(playerPos.x, (backgroundPos.x - _repeatWidth));
        float rightPositionDiff = AbsoluteDiff(playerPos.x, (backgroundPos.x + _repeatWidth));
        
        if (leftPositionDiff <= repeatDiff || rightPositionDiff <= repeatDiff)
        {
            gameObject.transform.position = new Vector3(playerPos.x, _startPosition.y, _startPosition.z);
        }

    }
    
    private float AbsoluteDiff(float p1, float p2)
    {
        float diff = Mathf.Abs(p1) - Mathf.Abs(p2);
        return Mathf.Abs(diff);
    }

}
