using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BattleScreenHandler : MonoBehaviour
{
    private UIDocument document;
    
    private Button attackButton;
    private Button blockButton;
    private Button runButton;

    public PlayerController player;
    public EnemyController enemy;
    
    // Start is called before the first frame update
    void Start()
    {
        document = GetComponent<UIDocument>();
        InitialiseButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDisable()
    {
        attackButton.UnregisterCallback<ClickEvent>(OnAttackButtonClick);
        blockButton.UnregisterCallback<ClickEvent>(OnBlockButtonClick);
        runButton.UnregisterCallback<ClickEvent>(OnRunButtonClick);
    }

    private void InitialiseButtons()
    {
        attackButton = document.rootVisualElement.Q("AttackButton") as Button;
        attackButton.RegisterCallback<ClickEvent>(OnAttackButtonClick);
        
        blockButton = document.rootVisualElement.Q("BlockButton") as Button;
        blockButton.RegisterCallback<ClickEvent>(OnBlockButtonClick);
        
        runButton = document.rootVisualElement.Q("RunButton") as Button;
        runButton.RegisterCallback<ClickEvent>(OnRunButtonClick);
    }

    private void OnAttackButtonClick(ClickEvent evt)
    {
        
        if (enemy.enemyHealth < player.playerAttack)
        {
            Debug.Log("You defeated the enemy");
            Destroy(GameObject.Find("Enemy"));
            gameObject.SetActive(false);
            player.isInBattle = false;
        }
        else
        {
            Debug.Log("You damaged the enemy for " + player.playerAttack + " points - Enemy Health = " + (enemy.enemyHealth - player.playerAttack));
            enemy.enemyHealth -= player.playerAttack;
        }
        
    }
    
    private void OnBlockButtonClick(ClickEvent evt)
    {
        Debug.Log("You pressed the Block Button");
    }
    
    private void OnRunButtonClick(ClickEvent evt)
    {
        Debug.Log("You pressed the Run Button");
    }
}
