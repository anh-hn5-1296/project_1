using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DiChuyen : MonoBehaviour
{
    public Animator animator;
    public bool isRight = true;
    private Rigidbody2D rb;
    private bool moveLeft;
    private bool moveRight;
    private float horizontalMove;
    public float speed = 5;
    public float jumpSpeed = 10;
    public bool isGrounded;
    private bool nen;
    public GameObject panel, button, text;
    public TextMeshProUGUI diemText;
    private int tong = 0;
    public GameObject PSBiAn;
    public bool isPause = false;
    public AudioSource sound_death;
    public AudioSource sound_main;
    // Start is called before the first frame update
    void Start()
    {
        sound_main.Play();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        TinhTong(0);
        moveLeft = false;
        moveRight = false;
    }

    public void PointerDownLeft()
    {
        moveLeft = true;
    }    

    public void PointerUpLeft()
    {
        moveLeft = false;
    }
    public void PointerDownRight() { moveRight = true;}
    public void PointerUpRight() { moveRight = false;}
    // Update is called once per frame
    void Update()
    {
        MovePlay();
        if (Input.GetKey(KeyCode.RightArrow))
        {
            isRight = true;
            animator.SetBool("isRunning", true);
            transform.Translate(Time.deltaTime * 5, 0, 0);
            Vector2 scale = transform.localScale;
            scale.x *= scale.x > 0 ? 1 : -1;
            transform.localScale = scale;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            isRight = false;
            animator.SetBool("isRunning", true);
            transform.Translate(-Time.deltaTime * 5, 0, 0);
            Vector2 scale = transform.localScale;
            scale.x *= scale.x > 0 ? -1 : 1;
            transform.localScale = scale;
        }
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            if (nen == true)
            { 
                if (isRight == true)
                {
                    // transform.Translate(Time.deltaTime * 5, Time.deltaTime * 10, 0);
                    rb.AddForce(new Vector2(0, 500));
                    Vector2 scale = transform.localScale;
                    scale.x *= scale.x > 0 ? 1 : -1;
                    transform.localScale = scale;
                }
                else
                {
                    // transform.Translate(-Time.deltaTime * 5, Time.deltaTime * 10, 0);
                    rb.AddForce(new Vector2(0, 500));
                    Vector2 scale = transform.localScale;
                    scale.x *= scale.x > 0 ? -1 : 1;
                    transform.localScale = scale;
                }
                nen = false;
            }
        }   
    }
    private void MovePlay()
    {
        if (moveLeft) 
        {
            horizontalMove = -speed;
            transform.localScale = new Vector3( -0.7045F, 0.6597F, 1F);
        }
        else if (moveRight)
        {
            horizontalMove = speed;
            transform.localScale = new Vector3(0.7045F, 0.6597F, 1F);
        }    
        else { horizontalMove = 0; }
    }
    public void PauseGame()
    {
        isPause = !isPause;
        if (isPause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "nen_dat")
        {
            nen = true;
        }

        if (collision.gameObject.tag == "qua_man")
        {
            SceneManager.LoadScene("man_2");
        }   
        
        if (collision.gameObject.tag == "nen_dat")
        {
            isGrounded = true;
        }
    }

    public void jumpButton()
    {
        if (isGrounded)
        {
            isGrounded = false;
            rb.velocity = Vector2.up * jumpSpeed;
        }
        
    }  
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ben_tren")
        {
            // nấm die
            var name = collision.attachedRigidbody.name;
            Destroy(GameObject.Find(name));
            TinhTong(8);
        }
        if (collision.gameObject.tag == "ben_trai" || collision.gameObject.tag == "roi_xuong")
        {
            sound_main.Stop();
            sound_death.Play();

            // game over, replace màn chơi
            Time.timeScale = 0; // dừng scence
            panel.SetActive(true);  // Show panel
            button.SetActive(true); // Show button
            text.SetActive(true);   // Show text
        }

        if (collision.gameObject.tag == "coin")
        {
            var name = collision.attachedRigidbody.name;
            Destroy(GameObject.Find(name));
            TinhTong(5);
        }    
        
        if (collision.gameObject.tag == "bi_an")
        {
            var name = collision.attachedRigidbody.name;
            Destroy(GameObject.Find(name));
            Instantiate(PSBiAn,
                collision.gameObject.transform.position, 
                collision.gameObject.transform.localRotation);
            TinhTong(10);
        }   
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalMove, rb.velocity.y);
    }

    void TinhTong(int score)
    {
        tong += score;
        diemText.text = "Điểm: " + tong;
    }    
}
