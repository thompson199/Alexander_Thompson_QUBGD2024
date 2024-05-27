using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Course_Library.Scripts
{
    public class GameManager : MonoBehaviour
    {

        public List<GameObject> targets;
        public GameObject titleScreen;
        public GameObject gameOverScreen;
        public TextMeshProUGUI scoreText;

        private bool _isGameActive;
        private int _scoreValue;
        private float _spawnRate;
    
        // Start is called before the first frame update
        private void Start()
        {
            
        }

        // Update is called once per frame
        private void Update()
        {
        
        }

        private IEnumerator SpawnTarget()
        {
            while (_isGameActive)
            {
                yield return new WaitForSeconds(_spawnRate);
                int index = Random.Range(0, targets.Count);
                Instantiate(targets[index]);
            }
        }
    
        public void UpdateScore(int scoreToAdd)
        {
            _scoreValue += scoreToAdd;
            scoreText.text = $"Score: {_scoreValue}";
        }

        public void GameOver()
        {
            gameOverScreen.gameObject.SetActive(true);
            _isGameActive = false;
        }

        public void StartGame(int difficulty)
        {
            titleScreen.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(true);
            
            _scoreValue = 0;
            _isGameActive = true;
            _spawnRate = 1.0f/difficulty;
        
            UpdateScore(0);
            StartCoroutine(SpawnTarget());
        }
        
        public void RestartGame()
        {
            gameOverScreen.gameObject.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public bool GetIsGameActive()
        {
            return _isGameActive;
        }
    }
}
