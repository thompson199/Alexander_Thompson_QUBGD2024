using UnityEngine;
using UnityEngine.UI;

namespace Course_Library.Scripts
{
    public class DifficultyButton : MonoBehaviour
    {
        private GameObject _self;
        private GameManager _gameManager;
        
        private Button _button;

        public int difficulty;
    
        // Start is called before the first frame update
        private void Start()
        {
            _self = gameObject;
            _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
            _button = GetComponent<Button>();
            
            _button.onClick.AddListener(SetDifficulty);
        }

        // Update is called once per frame
        private void Update()
        {
            
        }

        private void SetDifficulty()
        {
            Debug.Log($"{_self.name} was clicked");
            _gameManager.StartGame(difficulty);
        }
    }
}
