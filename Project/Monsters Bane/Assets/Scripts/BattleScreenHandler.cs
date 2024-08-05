using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleScreenHandler : MonoBehaviour
{
    private GameObject _battleScreenObj;
    private PlayerController _player;
    private EnemyController _enemy;
    private PlayerUIHandler _playerUI;
    
    // Start is called before the first frame update
    private void Start()
    {
        _battleScreenObj = gameObject;
        _player = GameObject.Find("Player").GetComponent<PlayerController>();

        _playerUI = GameObject.Find("Player UI").GetComponent<PlayerUIHandler>();
    }

    public void SetEnemyToFight(EnemyController enemyToFight)
    {
        _enemy = enemyToFight;
    }
    
    public void HandleAttackButton()
    {
        if (_enemy.GetEnemyHealth() <= _player.GetPlayerAttack())
        {
            Debug.Log("You defeated the enemy");
            Destroy(_enemy.gameObject);
            HideBattleScreen();
            _player.SetIsInBattle(false);
        }
        else
        {
            _enemy.SetEnemyHealth(_enemy.GetEnemyHealth() - _player.GetPlayerAttack());
            Debug.Log($"Damaged enemy for {_player.GetPlayerAttack()} points");
            ActivateEnemyTurn(false);
        }
    }

    public void HandleBlockButton()
    {
        Debug.Log("Pressed the block button");
        ActivateEnemyTurn(true);
    }

    public void HandleFleeButton()
    {
        Debug.Log("You have fled from the battle");
        // Hide the battle screen
        HideBattleScreen();
        
        // set the player isInBattle status to false
        _player.SetIsInBattle(false);
    }

    private void ActivateEnemyTurn(bool playerIsBlocking)
    {
        int damageFromEnemy = (playerIsBlocking) ? (_enemy.GetEnemyAttack() / 2) : _enemy.GetEnemyAttack();
        
        // any possible future modifications/checks regarding enemy damage - e.g. status effects etc.

        _player.SetPlayerHealth(_player.GetPlayerHealth() - damageFromEnemy);
        Debug.Log($"Enemy attacked for {damageFromEnemy} points");
        
        _playerUI.UpdatePlayerHealth(damageFromEnemy, false);
    }
    
    private void HideBattleScreen()
    {
        _battleScreenObj.SetActive(false);
    }
}
