using UnityEngine;
using System.Collections;
// Errors:
using UnityEngine.Assertions;

public class Player : MonoBehaviour
{
    [SerializeField] private float jumpForce = 100f;
    [SerializeField] private AudioClip sfxJump;
    [SerializeField] private AudioClip sfxDeath;
    [SerializeField] private AudioClip sfxCoin;
    [SerializeField] PlayerHealthBar energyBar;
    [SerializeField] float coinAmount = 5f;

    private Animator anim;
    private Rigidbody rigidBody;
    private bool jump = false;
    private AudioSource audioSource;

    void Awake()
    {
        Assert.IsNotNull(sfxJump);
        Assert.IsNotNull(sfxDeath);
        Assert.IsNotNull(sfxCoin);

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
                if (!GameManager.instance.PlayerActive)
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
            PlayerDeath(150f, 50f);
            GameManager.instance.GameIsOver();
        }

        if(collision.gameObject.tag == "bridge")
        {
            PlayerDeath(150f, 130f);
            GameManager.instance.GameIsOver();
        }

        // destroys the coin
        if(collision.gameObject.tag == "coin")
        {
            Score.instance.AddCoinScore();
            audioSource.PlayOneShot(sfxCoin);
            energyBar.EnergyChange(coinAmount);
            Destroy(collision.gameObject);
        }
    }

    public void PlayerDeath(float x, float y)
    {
        rigidBody.detectCollisions = false;
        rigidBody.AddForce(new Vector2(150f, 130), ForceMode.Impulse);
        audioSource.PlayOneShot(sfxDeath);
    }
    

    public void ResetPosition()
    {
        transform.position = new Vector3(-0.054243f, 9.9984f, -12.89f);
    }
}
