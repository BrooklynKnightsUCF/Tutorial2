using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public Text score;
    public Text lives;
    public Text win;

    public AudioClip musicClipOne;

    public AudioClip musicClipTwo;

    public AudioSource musicSource;
    
    Animator anim;

    private int scoreValue = 0;
    private int liveValue = 3;

    private SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        lives.text = "Lives: " + liveValue.ToString();
        renderer = GetComponent<SpriteRenderer>();
        if (renderer == null)
        {
            Debug.LogError("Player Sprite is missing a renderer");
        }

        win.text = "";

        musicSource.clip = musicClipOne;
        musicSource.loop = true;
        musicSource.Play();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        if(liveValue > 0 && scoreValue < 8)
        {
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            renderer.flipX = false;
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            renderer.flipX = true;
        }   



        

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

 
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            if (scoreValue == 4 && liveValue > 0)
             {
                liveValue = 3;
                lives.text = "Lives: " + liveValue.ToString();
                transform.position = new Vector2(-3, 11);
            }
            else if (scoreValue == 8 && liveValue > 0)
            {
            win.text = "You Win! Congratulations! A game by Brooke Lamothe";
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            musicSource.loop = false;
            }
        }
        if (collision.collider.tag == "Enemy")
        {
            liveValue -= 1;
            lives.text = liveValue.ToString();
            Destroy(collision.collider.gameObject);
            if (liveValue <= 0 && scoreValue < 8 )
            {
                win.text = "You Lose!";
            }
        }
        

    }



    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && liveValue > 0 && scoreValue < 8 )
        {
            if (Input.GetKey(KeyCode.W))
            {  
            rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            anim.SetInteger("State", 2);
             //the 3 in this line of code is the player's "jumpforce," and you change that number to get different jump behaviors. You can also create a public variable for it and then edit it in the inspector.
            }
                    else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) )
                {
                anim.SetInteger("State", 1);
                }
                 else if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
                {
                    anim.SetInteger("State", 0);
                }
            
        }
    }
}
