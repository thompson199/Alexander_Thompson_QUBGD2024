using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class EnemyController : MonoBehaviour
{
    private GameObject _self;

    private float _speed = 2f;
    private float _leftBoundaryX;
    private float _rightBoundaryX;
    private bool _moveRight = true;
    private bool _isInBattle = false;
    
    public int enemyHealth = 20;
    public int enemyAttack = 10;
    
    // Start is called before the first frame update
    private void Start()
    {
        _self = gameObject;

        Vector3 currentPosition = _self.transform.position;
        _leftBoundaryX = currentPosition.x - 5f;
        _rightBoundaryX = currentPosition.x + 5f;
        
        MoveDirection(Vector3.right);
    }

    // Update is called once per frame
    private void Update()
    {
        if (_isInBattle) return;
        
        Vector3 directionToMove = (_moveRight) ? Vector3.right : Vector3.left;
        MoveDirection(directionToMove);

        // If current x-pos is close to right boundary, enemy should now move left
        if (_self.transform.position.x >= _rightBoundaryX)
        {
            _moveRight = false;
        }

        // If current x-pos is close to left boundary, enemy should now move right
        if (_self.transform.position.x <= _leftBoundaryX)
        {
            _moveRight = true;
        }
        
    }

    private void MoveDirection(Vector3 moveDirection)
    {
        transform.Translate(moveDirection * (_speed * Time.deltaTime));
    }

    public void SetIsInBattle(bool inBattle)
    {
        _isInBattle = inBattle;
    }
}
