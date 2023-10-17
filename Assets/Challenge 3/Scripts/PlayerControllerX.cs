
using System.Transactions;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;

    public float floatForce;
    private float gravityModifier = 1.5f;
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;

    public AudioClip bounceSound;

    public float upperBound = 10.0f;

    public float lowerBound = -1.0f;

    public bool isOnGround = false;



    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();
      

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

    }

    // Update is called once per frame
 void Update()
{
    // While space is pressed and player is low enough, float up 
    if (Input.GetKeyDown(KeyCode.Space) && !gameOver && transform.position.y > lowerBound && transform.position.y < upperBound)
    {
        playerRb.AddForce(Vector3.up * floatForce, ForceMode.Impulse);
    }
    else if (transform.position.y > upperBound)
    {
        // Set the position to the upper bound
        Vector3 newPosition = transform.position;
        newPosition.y = upperBound;
        transform.position = newPosition;
    }
    else if (transform.position.y < lowerBound)
    {
        // Set the position to the lower bound
        Vector3 newPosition = transform.position;
        newPosition.y = lowerBound;
        transform.position = newPosition;
    }
}


    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
            
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

        }  else if (other.gameObject.CompareTag("Ground")){
            isOnGround = true; 
            playerAudio.PlayOneShot(bounceSound, 1.0f);
        }

    }

}