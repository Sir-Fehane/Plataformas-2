using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Video;

public class PlayerScript : MonoBehaviour
{
    public TextMeshProUGUI textManzanas;
    public TextMeshProUGUI textTimer;
    float timer;
    int level_timer;
    int num_manzanas;
    SpriteRenderer sprite;
    Rigidbody2D rigidbody;
    BoxCollider2D boxCollider;
    public LayerMask layerMask;
    Animator animator;
    bool double_jump;
    bool hit;
    bool control;
    bool water;

    int vidas;
    int corazones;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        double_jump = false;
        hit = false;
        control = true;
        water = false;
        num_manzanas = 0;

        timer = Time.time;
        level_timer = 250;
        textTimer.SetText("" + level_timer);

        vidas = PlayerPrefs.GetInt("player_lives", 5);
        corazones = 3;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Timer
        if (Time.time > timer)
        {
            level_timer--;
            textTimer.SetText("" + level_timer);

            if (level_timer == 0)
            {
                animator.SetTrigger("delete");
            }
            timer++;
        }

        if (control)
        {
            //Control
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                sprite.flipX = true;

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    rigidbody.linearVelocity = new Vector2(-10f, rigidbody.linearVelocity.y);
                    animator.speed = 2;
                }
                else
                {
                    rigidbody.linearVelocity = new Vector2(-5f, rigidbody.linearVelocity.y);
                    animator.speed = 1;
                }
            }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            sprite.flipX = false;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                rigidbody.linearVelocity = new Vector2(10f, rigidbody.linearVelocity.y);
                animator.speed = 2;
            }
            else
            {
                rigidbody.linearVelocity = new Vector2(5f, rigidbody.linearVelocity.y);
                animator.speed = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (water)
            {
                double_jump = true;
                rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x, 4);    
            }
            else if (isGrounded())
            {
                double_jump = true;
                rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x , 15f);  
            }
            else if (double_jump)
            {
                double_jump = false;
                rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x , 15f);     
            }
        }
        }

        if (!hit)
        {
            //Animator
            if (isGrounded() && !Input.GetKey(KeyCode.LeftArrow)
            && !Input.GetKey(KeyCode.RightArrow))
            {
                animator.SetInteger("estado", 0);
            }
            else if (isGrounded() && (Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKey(KeyCode.RightArrow)))
            {
                animator.SetInteger("estado", 1);
            }
            else if (rigidbody.linearVelocity.y > 0)
            {
                if (double_jump)
                   animator.SetInteger("estado", 2);
                else
                    animator.SetInteger("estado", 4);
            }
            else if (rigidbody.linearVelocity.y < 0)
            {
                animator.SetInteger("estado", 3);
            }
        }
    }

    public bool isGrounded()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, 
        boxCollider.bounds.size, 0, Vector2.down, 
        0.1f, layerMask);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Meta")
        {
            control = false;
            collision.gameObject.GetComponent<Animator>()
            .SetTrigger("metaend");
        }

        if (collision.gameObject.tag == "Apple")
        {
            num_manzanas++;
            
            if (num_manzanas == 5)
            {
                PlayerPrefs.SetInt("player_lives", PlayerPrefs.GetInt("player_lives") + 1);
                num_manzanas = 0;
            }
          
            textManzanas.SetText("" + num_manzanas);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Strawberry")
        {
            if(corazones < 3)
            {
                corazones++;
            }

            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Spikehead")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            collision.gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
            collision.gameObject.GetComponent<Rigidbody2D>().mass = 100;
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 10;

        }

        if (collision.gameObject.tag == "Trampoline")
        {
            collision.gameObject.GetComponent<Animator>().SetInteger("trampestado", 1);
            rigidbody.linearVelocity = new Vector2(rigidbody.linearVelocity.x , 35f);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
      if (collision.gameObject.tag == "Fan")
      {
            rigidbody.linearVelocity = new Vector2(0 , 4f);
      }  

      if (collision.gameObject.tag == "Water")
      {
            water = true;
            rigidbody.gravityScale = 0.5f;
      }  
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Trampoline")
        {
            collision.gameObject.GetComponent<Animator>().SetInteger("trampestado", 0);
        }

      if (collision.gameObject.tag == "Water")
      {
            water = false;
            rigidbody.gravityScale = 4;
      }  
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Rino")
        {
            if (!isGrounded())
            {
                collision.gameObject.GetComponent<Animator>().
                SetTrigger("rinoend");
                rigidbody.linearVelocity = 
                new Vector2(rigidbody.linearVelocity.x, 15);
            }
            else
            {
                corazones--;
                if (corazones <= 0)
                {
                    animator.SetTrigger("delete");
                }
                else
                {
                    control = false;
                    hit = true;
                    animator.SetInteger("estado", 5);
                    if (sprite.flipX)
                    {
                        rigidbody.linearVelocity =
                        new Vector2(10f, 4f);
                    }
                    else
                    {
                        rigidbody.linearVelocity =
                        new Vector2(-10f, 4f);
                    }
                }
            }
        }

        if (collision.gameObject.tag == "Limite"
        || collision.gameObject.tag == "Spikehead")
        {
            animator.SetTrigger("delete");
        }

        if (collision.gameObject.tag == "Fire")
        {
            control = false;
            hit = true;

            animator.SetInteger("estado", 5);

            if (sprite.flipX)
            {
                rigidbody.linearVelocity = 
                new Vector2(10f, 4f);
            }
            else
            {
                rigidbody.linearVelocity = 
                new Vector2(-10f, 4f);
            }

            corazones--;
            if (corazones <= 0)
            {
                animator.SetTrigger("delete");
            }
            
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Plataforma")
        {
            transform.SetParent(collision.gameObject.transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Plataforma")
        {
            transform.SetParent(null);
        }
    }

    public void gotoDesapperearing()
    {
        restarVida();
        int level_select = PlayerPrefs.GetInt("level_select");

        if (PlayerPrefs.GetInt("player_lives") <= 0)
        {
            SceneManager.LoadScene(6);

        }
        else
        {
            switch (level_select)
            {
                case 1:
                    SceneManager.LoadScene(2);
                    break;
                case 2:
                    SceneManager.LoadScene(3);
                    break;
                case 3:
                    SceneManager.LoadScene(4);
                    break;
                case 4:
                    SceneManager.LoadScene(5);
                    break;
            }
        }
        
    }

    public void gotoIdle()
    {
        control = true;
        hit = false;

        animator.SetInteger("estado", 0);
    }

    public void restarVida()
    {
        PlayerPrefs.SetInt("player_lives", PlayerPrefs.GetInt("player_lives") - 1);
    }
}
