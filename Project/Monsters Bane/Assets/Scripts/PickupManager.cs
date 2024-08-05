using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    private GameObject _pickupObj;
    private PlayerUIHandler _playerUI;
    
    private Vector3 _rotationVector;
    
    private const int _healthPickupIncrease = 10;
    
    // Start is called before the first frame update
    private void Start()
    {
        _pickupObj = gameObject;
        _playerUI = GameObject.Find("Player UI").GetComponent<PlayerUIHandler>();

        _rotationVector = new Vector3(0, 0.15f, 0);
    }

    // Update is called once per frame
    private void Update()
    {
        _pickupObj.transform.Rotate(_rotationVector);
    }

    private void OnCollisionEnter(Collision other)
    {
        GameObject collidedObject = other.gameObject;
        bool collidedWithPlayer = collidedObject.CompareTag("Player");

        if (_pickupObj.name.Equals("Health Pickup") && collidedWithPlayer)
        {
            PlayerController playerCont = collidedObject.GetComponent<PlayerController>();
            
            playerCont.SetPlayerHealth(playerCont.GetPlayerHealth() + _healthPickupIncrease);
            _playerUI.UpdatePlayerHealth(_healthPickupIncrease, true);
            
            Destroy(_pickupObj);
        }
    }
}
