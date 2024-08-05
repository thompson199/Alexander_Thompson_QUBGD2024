using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUIHandler : MonoBehaviour
{
    private PlayerController _player;
    private GameObject _playerHealthBar;
    
    private float _origHealthBarWidth;
    
    public TextMeshProUGUI playerHealthText;
    
    // Start is called before the first frame update
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerController>();
        _playerHealthBar = GameObject.Find("Health Bar Foreground");
        
        _origHealthBarWidth = _playerHealthBar.GetComponent<RectTransform>().rect.width;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
    
    private void UpdatePlayerHealthBar(int healthModification, bool increaseHealth)
    {
        RectTransform healthBarRectTransform = _playerHealthBar.GetComponent<RectTransform>();
        Rect healthBarRect = healthBarRectTransform.rect;
        Vector3 healthBarPos = healthBarRectTransform.position;
        
        float sizeModification = _origHealthBarWidth * ((float) healthModification / _player.GetMaxPlayerHealth());

        float newWidth;
        float newPositionX;
        if (increaseHealth)
        {
            newWidth = healthBarRect.width + sizeModification;
            newPositionX = healthBarPos.x + (sizeModification / 2);
        }
        else
        {
            newWidth = healthBarRect.width - sizeModification;
            newPositionX = healthBarPos.x - (sizeModification / 2);
        }
        
        healthBarRectTransform.sizeDelta = new Vector2(newWidth, healthBarRect.height);;
        healthBarRectTransform.position = new Vector3(newPositionX, healthBarPos.y, healthBarPos.z);
    }
    
    private void UpdatePlayerHealthText()
    {
        playerHealthText.text = $"{_player.GetPlayerHealth()}/{_player.GetMaxPlayerHealth()}";
    }

    public void UpdatePlayerHealth(int healthModification, bool increaseHealth)
    {
        UpdatePlayerHealthBar(healthModification, increaseHealth);
        UpdatePlayerHealthText();
    }
}
