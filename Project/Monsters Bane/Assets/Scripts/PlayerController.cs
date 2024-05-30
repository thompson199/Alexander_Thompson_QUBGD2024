using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject _self;
    private Rigidbody _playerRb;
    
    private float _playerGroundMoveSpeed = 7.5f;
    private float _playerAirMoveSpeed = 5f;
    private float _playerJumpForce = 22.5f;

    private bool _isOnGround = true;
    private bool _isInBattle = false;
    
    public GameObject battleScreen;
    
    public int playerHealth = 50;
    public int playerAttack = 15;
    
    public const int MaxPlayerHealth = 50;
    
    // Start is called before the first frame update
    private void Start()
    {
        _self = gameObject;
        _playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        bool movingLeft = Input.GetKey(KeyCode.LeftArrow);
        bool movingRight = Input.GetKey(KeyCode.RightArrow);
        Vector3 moveDirection = (movingLeft) ? Vector3.left : (movingRight) ? Vector3.right : Vector3.zero;
        
        if (_isInBattle) return;
        
        HandleMoveLeftRight(moveDirection); 
        HandleJump();
    }

    private void OnCollisionEnter(Collision other)
    {
        GameObject collidedObject = other.gameObject;
        Vector3 playerPosition = _self.transform.position;

        if (collidedObject.CompareTag("Ground"))
        {
            _isOnGround = true;
        }
        
        else if (collidedObject.CompareTag("Enemy"))
        {
            HandleEnemyCollision(playerPosition, collidedObject);
        }
    }

    private void HandleMoveLeftRight(Vector3 playerMoveDirection)
    {
        float moveSpeed = (_isOnGround) ? _playerGroundMoveSpeed : _playerAirMoveSpeed;
        transform.Translate(playerMoveDirection * (moveSpeed * Time.deltaTime));
    }
    
    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isOnGround)
        {
            _playerRb.AddForce(_self.transform.up * _playerJumpForce, ForceMode.Impulse);
            _isOnGround = false;
        }
    }

    private void HandleEnemyCollision(Vector3 playerPosition, GameObject collidedEnemy)
    {
        Debug.Log("Collided With Enemy");
            
        // Set battle status to true for player and enemy
        _isInBattle = true;
        collidedEnemy.GetComponent<EnemyController>().SetIsInBattle(true);
            
        // Show battle screen ui element and set enemy to fight to collided enemy
        battleScreen.SetActive(true);
        battleScreen.GetComponent<BattleScreenHandler>().SetEnemyToFight(collidedEnemy.GetComponent<EnemyController>());
            
        // move player away from enemy, relative to enemy x-position - y and z position unaffected
        _self.transform.position = new Vector3(collidedEnemy.transform.position.x - 4, playerPosition.y, playerPosition.z);
        
    }

    public void SetIsInBattle(bool inBattle)
    {
        _isInBattle = inBattle;
    }
    
}
