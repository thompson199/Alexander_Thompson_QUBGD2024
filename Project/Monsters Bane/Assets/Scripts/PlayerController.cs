using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    
    private float playerGroundMoveSpeed = 7.5f;
    private float playerAirMoveSpeed = 5f;
    private float playerJumpForce = 22.5f;

    private bool isOnGround = true;
    public bool isInBattle = false;
    
    public GameObject battleScreen;
    public int playerHealth = 50;
    public int playerAttack = 15;
    
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        bool movingLeft = Input.GetKey(KeyCode.LeftArrow);
        bool movingRight = Input.GetKey(KeyCode.RightArrow);
        Vector3 moveDirection = (movingLeft) ? Vector3.left : (movingRight) ? Vector3.right : Vector3.zero;
        
        if (isInBattle) return;
        
        HandleMoveLeftRight(moveDirection); 
        HandleJump();
    }

    private void OnCollisionEnter(Collision other)
    {
        GameObject collidedObject = other.gameObject;
        Vector3 playerPosition = transform.position;

        if (collidedObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        
        else if (collidedObject.CompareTag("Enemy"))
        {
            HandleEnemyCollision(playerPosition, collidedObject);
        }
    }

    private void HandleMoveLeftRight(Vector3 playerMoveDirection)
    {
        float moveSpeed = (isOnGround) ? playerGroundMoveSpeed : playerAirMoveSpeed;
        transform.Translate(playerMoveDirection * (Time.deltaTime * moveSpeed));
    }
    
    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(transform.up * playerJumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
    }

    private void HandleEnemyCollision(Vector3 playerPosition, GameObject collidedEnemy)
    {
        Debug.Log("Collided With Enemy");
            
        // Set battle status to true
        isInBattle = true;
            
        // Show battle screen ui element
        battleScreen.SetActive(true);
        battleScreen.GetComponent<BattleScreenHandler>().player = this;
        battleScreen.GetComponent<BattleScreenHandler>().enemy = collidedEnemy.GetComponent<EnemyController>();
            
        // move player away from enemy
        transform.position = new Vector3(playerPosition.x - 4, playerPosition.y, playerPosition.z);
    }
}
