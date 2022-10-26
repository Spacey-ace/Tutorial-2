using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    private int scoreValue = 0;
    public Text livesCounter;
    private int livesCounterValue = 3;
    public GameObject winTextObject;
    public GameObject youLoseTextObject;
    public AudioClip musicClipOne;
    public AudioSource musicSource;
    Animator anim;
    private bool facingRight = true;



    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        scoreValue = 0;
        livesCounterValue = 3;

        SetCountText();
        winTextObject.SetActive(false);
        SetCountText();
        youLoseTextObject.SetActive(false);

        anim = GetComponent<Animator>();

    }

    void SetCountText()
    {
        
        if (scoreValue >= 8)
        {
            winTextObject.SetActive(true);
            musicSource.clip = musicClipOne;
            musicSource.Play();
        }

       
       
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }

        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }

    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetInteger("State", 4);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 2);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            
        }
        if (scoreValue == 4)
        {
            transform.position = new Vector3(47.8f, .8f, 0);
            livesCounterValue =3;
        }
        if (scoreValue == 8)
        {
            winTextObject.SetActive(true);
            musicSource.clip = musicClipOne;
            musicSource.Play();
        }
        if(collision.collider.tag == "Enemy")
        {
        
            score.text = scoreValue.ToString();
            livesCounterValue -= 1;
            livesCounter.text = livesCounterValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        if (livesCounterValue <= 0)
        {
            youLoseTextObject.SetActive(true);
        }
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
   
}
