using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;

    private PlayerController _playerControllerScript;
    private Vector3 _spawnPos = new Vector3(25, 0, 0);

    private float _startDelay = 2f;
    private float _repeatRate = 2f;
    
    // Start is called before the first frame update
    public void Start()
    {
        _playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        
        InvokeRepeating(nameof(SpawnObstacle), _startDelay, _repeatRate);
    }

    // Update is called once per frame
    public void Update()
    {

    }

    private void SpawnObstacle()
    {
        if (!_playerControllerScript.gameOver)
        {
            Instantiate(obstaclePrefab, _spawnPos, obstaclePrefab.transform.rotation);
        }
    }
}
