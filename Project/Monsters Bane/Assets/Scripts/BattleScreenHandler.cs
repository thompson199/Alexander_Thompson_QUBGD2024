using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleScreenHandler : MonoBehaviour
{
    private GameObject _self;
    private PlayerController _player;
    private EnemyController _enemy;
    private GameObject _playerHealthBar;
    
    private float _origHealthBarWidth;
    
    public TextMeshProUGUI playerHealthText;
    
    // Start is called before the first frame update
    private void Start()
    {
        _self = gameObject;
        _player = GameObject.Find("Player").GetComponent<PlayerController>();
        _playerHealthBar = GameObject.Find("Health Bar Foreground");

        _origHealthBarWidth = _playerHealthBar.GetComponent<RectTransform>().rect.width;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void SetEnemyToFight(EnemyController enemyToFight)
    {
        _enemy = enemyToFight;
    }
    
    public void HandleAttackButton()
    {
        if (_enemy.enemyHealth <= _player.playerAttack)
        {
            Debug.Log("You defeated the enemy");
            Destroy(GameObject.Find("Enemy"));
            HideBattleScreen();
            _player.SetIsInBattle(false);
        }
        else
        {
            _enemy.enemyHealth -= _player.playerAttack;
            Debug.Log($"Damaged enemy for {_player.playerAttack} points");
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
        int damageFromEnemy = (playerIsBlocking) ? (_enemy.enemyAttack / 2) : _enemy.enemyAttack;
        
        // any possible future modifications/checks regarding enemy damage - e.g. status effects etc.

        _player.playerHealth -= damageFromEnemy;
        Debug.Log($"Enemy attacked for {damageFromEnemy} points");
        
        UpdatePlayerHealthBar(damageFromEnemy);
        UpdatePlayerHealthText();
    }

    private void UpdatePlayerHealthBar(int damageFromEnemy)
    {
        RectTransform healthBarRectTransform = _playerHealthBar.GetComponent<RectTransform>();
        Rect healthBarRect = healthBarRectTransform.rect;
        Vector3 healthBarPos = healthBarRectTransform.position;

        float sizeReduction = _origHealthBarWidth * ((float) damageFromEnemy / PlayerController.MaxPlayerHealth);
        
        Vector2 updatedHealthBarSize = new Vector2(healthBarRect.width - sizeReduction, healthBarRect.height);
        Vector3 updatedHealthBarPosition = new Vector3(healthBarPos.x - (sizeReduction / 2), healthBarPos.y, healthBarPos.z);
        
        healthBarRectTransform.sizeDelta = updatedHealthBarSize;
        healthBarRectTransform.position = updatedHealthBarPosition;
    }

    private void UpdatePlayerHealthText()
    {
        playerHealthText.text = $"{_player.playerHealth}/{PlayerController.MaxPlayerHealth}";
    }
    
    private void HideBattleScreen()
    {
        _self.SetActive(false);
    }
}
