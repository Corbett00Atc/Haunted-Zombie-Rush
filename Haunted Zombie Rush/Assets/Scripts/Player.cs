using UnityEngine;
using System.Collections;
// Errors:
using UnityEngine.Assertions;

public class Player : MonoBehaviour
{
    [SerializeField] private float jumpForce = 100f;
    [SerializeField] private AudioClip sfxJump;
    [SerializeField] private AudioClip sfxDeath;

    private Animator anim;
    private Rigidbody rigidBody;
    private bool jump = false;
    private AudioSource audioSource;

    void Awake()
    {
        Assert.IsNotNull(sfxJump);
        Assert.IsNotNull(sfxDeath);
    }

	// Use this for initialization
	void Start()
    {
        anim = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update()
    {
        if (!GameManager.instance.GameOver && GameManager.instance.GameStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameManager.instance.PlayerStartedGame();
                anim.Play("Jump");
                rigidBody.useGravity = true;

                audioSource.PlayOneShot(sfxJump);

                // activates the conditional in FixedUpdate
                jump = true;
            }
        }
	}

    void FixedUpdate()
    {
        if (jump == true)
        {
            jump = false;

            // resets velocity when jumping
            rigidBody.velocity = new Vector2(0, 0); 
            // adds force to jump
            rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode.Impulse);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "obstacle")
        {
            rigidBody.AddForce(new Vector2(150f, 50), ForceMode.Impulse);
            rigidBody.detectCollisions = false; 
            audioSource.PlayOneShot(sfxDeath);
            GameManager.instance.PlayerCollided();
        }

        if(collision.gameObject.tag == "bridge")
        {
            rigidBody.AddForce(new Vector2(150f, 130), ForceMode.Impulse);
            rigidBody.detectCollisions = false;
            audioSource.PlayOneShot(sfxDeath);
            GameManager.instance.PlayerCollided();
        }

        // destroys the coin
        if(collision.gameObject.tag == "coin")
        {
            Score.instance.AddCoinScore();
            Destroy(collision.gameObject);
        }
    }

    public void ResetPosition()
    {
        transform.position = new Vector3(-0.054243f, 9.9984f, -12.89f);
    }
}
