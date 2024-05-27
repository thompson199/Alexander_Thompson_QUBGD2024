using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Course_Library.Scripts
{
    public class Target : MonoBehaviour
    {

        private Rigidbody _targetRb;
        private GameObject _self;
        private GameManager _gameManager;

        private const float MinSpeed = 12;
        private const float MaxSpeed = 14;
        private const float MaxTorque = 10;
        private const float SpawnPosX = 4;
        private const float SpawnPosY = -2;

        public ParticleSystem explosionParticle;
        public int pointValue;

        // Start is called before the first frame update
        private void Start()
        {
            _self = gameObject;
            _targetRb = GetComponent<Rigidbody>();
            _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        
            _targetRb.AddForce(RandomUpForce(), ForceMode.Impulse);
            _targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

            _self.transform.position = RandomSpawnPosition();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnTriggerEnter(Collider other)
        {
            GameObject collidedObject = other.gameObject;
            
            // If this object has collided with the sensor, destroy it
            if (collidedObject.CompareTag("Sensor"))
            {
                Destroy(_self);
                
                // If the object that has collided with the sensor is not a "bad" object, set game over
                if (!_self.CompareTag("Bad"))
                {
                    _gameManager.GameOver();
                }
            }
        }

        private void OnMouseDown()
        {
            // If the game is not active, then return
            if (!_gameManager.GetIsGameActive()) return;
            
            Destroy(_self);
            Instantiate(explosionParticle, _self.transform.position, explosionParticle.transform.rotation);
            _gameManager.UpdateScore(pointValue);
        }

        private Vector3 RandomUpForce()
        {
            return Vector3.up * Random.Range(MinSpeed, MaxSpeed);
        }

        private float RandomTorque()
        {
            return Random.Range(-MaxTorque, MaxTorque);
        }

        private Vector3 RandomSpawnPosition()
        {
            return new Vector3(Random.Range(-SpawnPosX, SpawnPosX), SpawnPosY);
        }
    }
}
