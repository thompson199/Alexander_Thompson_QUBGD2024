using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class EnemyController : MonoBehaviour
{
    private GameObject _enemyObj;

    private float _speed = 2f;
    private float _leftBoundaryX;
    private float _rightBoundaryX;
    private bool _moveRight = true;
    private bool _isInBattle = false;
    
    private int _enemyHealth = 20;
    private int _enemyAttack = 10;
    private const int _maxEnemyHealth = 20;
    private const float startPositionOffset = 5f;
    
    // Start is called before the first frame update
    private void Start()
    {
        _enemyObj = gameObject;

        Vector3 startPosition = _enemyObj.transform.position;
        _leftBoundaryX = startPosition.x - startPositionOffset;
        _rightBoundaryX = startPosition.x + startPositionOffset;
        
        MoveDirection(Vector3.right);
    }

    // Update is called once per frame
    private void Update()
    {
        if (_isInBattle) return;
        
        HandleHorizontalMovement();
    }

    private void HandleHorizontalMovement()
    {
        Vector3 directionToMove = Vector3.right;
        float enemyPositionX = _enemyObj.transform.position.x;
        
        // If enemy x-pos has reached right boundary, enemy should now move left
        if (enemyPositionX >= _rightBoundaryX && _moveRight)
        {
            _enemyObj.transform.Rotate(new Vector3(0, 180, 0));
            _moveRight = false;
            directionToMove = Vector3.left;
        }

        // If enemy x-pos has reached left boundary, enemy should now move right
        else if (enemyPositionX <= _leftBoundaryX && !_moveRight)
        {
            _enemyObj.transform.Rotate(new Vector3(0, 180, 0));
            _moveRight = true;
            directionToMove = Vector3.right;
        }
        
        MoveDirection(directionToMove);
    }
    
    private void MoveDirection(Vector3 moveDirection)
    {
        transform.Translate(moveDirection * (_speed * Time.deltaTime));
    }

    public void SetIsInBattle(bool inBattle)
    {
        _isInBattle = inBattle;
    }

    public void SetEnemyHealth(int newEnemyHealth)
    {
        if (newEnemyHealth is < 0 or > _maxEnemyHealth) return;
        
        _enemyHealth = newEnemyHealth;
    }
    
    public int GetEnemyHealth()
    {
        return _enemyHealth;
    }

    public int GetEnemyAttack()
    {
        return _enemyAttack;
    }
}
