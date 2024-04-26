using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Private Variables
    private Rigidbody _playerRb;
    private Animator _playerAnim;
    private AudioSource _playerAudio;

    // Public Variables
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    public AudioClip jumpSound;
    public AudioClip explosionSound;
    
    public bool isOnGround = true;
    public bool gameOver = false;
    
    public float jumpForce = 600f;
    public float gravityModifier = 1.5f;
    
    // Start is called before the first frame update
    public void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        _playerAnim = GetComponent<Animator>();
        _playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            _playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            dirtParticle.Stop();
            _playerAnim.SetTrigger("Jump_trig");
            _playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedObject = collision.gameObject;

        if (collidedObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
        }
        else if (collidedObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!!!!");
            
            dirtParticle.Stop();
            explosionParticle.Play();
            
            _playerAnim.SetBool("Death_b", true);
            _playerAnim.SetInteger("DeathType_int", 1);
            _playerAudio.PlayOneShot(explosionSound, 1.0f);
        }
        
    }
}
