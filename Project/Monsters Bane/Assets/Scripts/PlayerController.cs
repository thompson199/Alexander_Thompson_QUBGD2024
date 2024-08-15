using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject _playerObj;
    private Rigidbody _playerRb;
    private Animator _playerAnim;
    
    private float _playerGroundMoveSpeed = 7.5f;
    private float _playerAirMoveSpeed = 5f;
    private float _playerJumpForce = 22.5f;

    private bool _isOnGround = true;
    private bool _isInBattle = false;
    private bool _isFacingRight = true;
    private bool _isTouchingObstacle = false;
    
    private int _playerHealth = 50;
    private int _playerAttack = 15;
    
    private const int _maxPlayerHealth = 50;
    private static readonly int SpeedF = Animator.StringToHash("Speed_f");
    
    public GameObject battleScreen;

    // Start is called before the first frame update
    private void Start()
    {
        _playerObj = gameObject;
        _playerRb = GetComponent<Rigidbody>();
        _playerAnim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (_isInBattle) return;
        
        HandleMoveLeftRight(); 
        HandleJump();
    }

    private void OnCollisionEnter(Collision other)
    {
        GameObject collidedObject = other.gameObject;
        Vector3 playerPosition = _playerObj.transform.position;
        
        if (collidedObject.CompareTag("Ground"))
        {
            _isOnGround = true;
        }

        if (collidedObject.CompareTag("Obstacle"))
        {
            _isTouchingObstacle = true;
        }
        
        else if (collidedObject.CompareTag("Enemy"))
        {
            HandleEnemyCollision(playerPosition, collidedObject);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        GameObject collidedObject = other.gameObject;
        
        if (collidedObject.CompareTag("Obstacle"))
        {
            _isTouchingObstacle = false;
        }
    }

    private void HandleMoveLeftRight()
    {
        float movementDirection = Input.GetAxisRaw("Horizontal");
        float movementSpeed = (_isOnGround) ? _playerGroundMoveSpeed : _playerAirMoveSpeed;
        Vector3 movementVector = (_isFacingRight) ? Vector3.right : Vector3.left;
        
        // Change direction player is facing based on movement
        if (movementDirection < 0 && _isFacingRight)
        {
            _playerObj.transform.Rotate(new Vector3(0, 180, 0));
            _isFacingRight = false;
        } 
        else if (movementDirection > 0 && !_isFacingRight)
        {
            _playerObj.transform.Rotate(new Vector3(0, -180, 0));
            _isFacingRight = true;
        }
        
        // Adjust run animation parameter
        _playerAnim.SetFloat(SpeedF, Mathf.Abs(movementDirection)); 
        
        // move player
        transform.Translate(movementVector * (movementDirection * (movementSpeed * Time.deltaTime)));
    }
    
    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (_isOnGround || (!_isOnGround && _isTouchingObstacle && _playerRb.velocity.y == 0)))
        {
            _playerRb.AddForce(_playerObj.transform.up * _playerJumpForce, ForceMode.Impulse);
            _isOnGround = false;
        }
    }

    private void HandleEnemyCollision(Vector3 playerPosition, GameObject collidedEnemy)
    {
        Debug.Log("Collided With Enemy");
            
        // Stop player movement animations
        _playerAnim.SetFloat(SpeedF, 0f);
        
        // Set battle status to true for player and enemy
        _isInBattle = true;
        collidedEnemy.GetComponent<EnemyController>().SetIsInBattle(true);
            
        // Show battle screen ui element and set enemy to fight to collided enemy
        battleScreen.SetActive(true);
        battleScreen.GetComponent<BattleScreenHandler>().SetEnemyToFight(collidedEnemy.GetComponent<EnemyController>());
            
        // move player away from enemy, relative to enemy x-position - y and z position unaffected
        _playerObj.transform.position = new Vector3(collidedEnemy.transform.position.x - 4, playerPosition.y, playerPosition.z);
        
    }

    public void SetIsInBattle(bool inBattle)
    {
        _isInBattle = inBattle;
    }
    
    public void SetPlayerHealth(int newPlayerHealth)
    {
        if (newPlayerHealth is < 0 or > _maxPlayerHealth) return;

        _playerHealth = newPlayerHealth;
    }

    public bool GetIsPlayerTouchingObstacle()
    {
        return _isTouchingObstacle;
    }
    
    public int GetPlayerHealth()
    {
        return _playerHealth;
    }

    public int GetMaxPlayerHealth()
    {
        return _maxPlayerHealth;
    }

    public int GetPlayerAttack()
    {
        return _playerAttack;
    }
}
