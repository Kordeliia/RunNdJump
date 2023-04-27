using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB; //por defecto null
    public float jumpForce = 10f; //por defecto 0
    public float gravityMultiplier;
    public bool isOnTheGround = true; //por defecto 0
    private bool _gameOver = false;
    private Animator _animator;
    private const string SPEED_MULTIPLIER = "Speed multiplier";
    private const string JUMP_TRIGGER = "Jump_trig";
    private const string SPEED_F = "Speed_f";
    private const string DEATH_B = "Death_b";
    private const string DEATHTYPE_INT = "DeathType_int";
    public ParticleSystem explosion;
    public ParticleSystem huella;
    public AudioClip jumpS;
    public AudioClip deadS;
    private AudioSource _audioSource;
    private float speedMultiplier;

    public bool GameOver
    {
        get => _gameOver;
        set
        {
            if(_gameOver == true)
            {
                _gameOver = true;
            }
            else
            {
                _gameOver = value;
            }
        }
    }    
    
    
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        Physics.gravity = gravityMultiplier * new Vector3(0, -9.81f, 0);
        _animator = GetComponent<Animator>();
        _animator.SetFloat(SPEED_F, 1);
        _animator.SetBool(DEATH_B, false);
        _animator.SetInteger(DEATHTYPE_INT, 1);
        huella.Pause();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        speedMultiplier += Time.deltaTime/10;
        _animator.SetFloat(SPEED_MULTIPLIER, speedMultiplier);
        if(Input.GetKeyDown(KeyCode.Space) && isOnTheGround && GameOver == false)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnTheGround=false;
            _animator.SetTrigger(JUMP_TRIGGER);
            huella.Stop();
            _audioSource.PlayOneShot(jumpS, 1);
        }
        
    }
    /*private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            huella.Play();
        }
        else
        {
            huella.Pause();
        }
    }*/
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isOnTheGround = true;
            huella.Play();
        } else if (collision.gameObject.CompareTag("Obstacle"))
        {
            _gameOver = true; 
            int number = Random.Range(1, 3);
            _animator.SetInteger(DEATHTYPE_INT, number);
            _animator.SetBool(DEATH_B, true);
            explosion.Play();
            huella.Stop();
            _audioSource.PlayOneShot(deadS, 1);
            Invoke("RestartGame", 3f);
        }
    }
    void RestartGame()
    {
        speedMultiplier = 1;
        SceneManager.UnloadSceneAsync("Prototype 3");
        SceneManager.LoadScene("Prototype 3");
    }
}
