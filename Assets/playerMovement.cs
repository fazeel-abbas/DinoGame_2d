using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{
    Animator anim;
    public CharacterController2D controller;

    float lefthorizontalMove = -30f;
    float righthorizontalMove = 30f;
    bool jump = false;
    bool crouch = false;

    public GameObject cloud;


    public float health = 100;
    public Slider healthBar;
    float score = 0;
    public Text scoreText;
    public Text WinText;

    float startTouchPosition, endTouchPosition;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        scoreText.text = "Score: " + score.ToString();
        WinText.text = " ";
    }

    // Update is called once per frame
    void FixedUpdate() // Important is the flow of how if and elseif works
    {
        //for key Inputs
        //if (Input.GetKey(KeyCode.RightArrow)) //for right movement
        //{
        //    controller.Move(righthorizontalMove * Time.fixedDeltaTime, crouch, jump);
        //    anim.SetTrigger("walk");

        //}
        //else if (Input.GetKey(KeyCode.LeftArrow)) //for left movement 
        //{
        //    controller.Move(lefthorizontalMove * Time.fixedDeltaTime, crouch, jump);
        //    anim.SetTrigger("walk");
        //}

        //else
        //{
        //    anim.SetTrigger("idle");
        //}

        //touch Inputs  // must know how to change inputs from touch to buttons
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);   // left mouse 
            if (touch.position.x < Screen.width / 2) // left half of screen
            {
                controller.Move(lefthorizontalMove * Time.fixedDeltaTime, crouch, jump);
                anim.SetTrigger("walk");
            }

            if (touch.position.x > Screen.width / 2)   // right half of screen
            {
                controller.Move(righthorizontalMove * Time.fixedDeltaTime, crouch, jump);
                anim.SetTrigger("walk");
            }

            else
            {
                anim.SetTrigger("idle");
            }
        }


    }

    private void Update()
    {
        //jump with keys
        //if (Input.GetKey(KeyCode.Space)) //for Jump
        //{
        //    transform.Translate(Vector2.up * 10 * Time.fixedDeltaTime);
        //}

        //jump with swipe up

        //  for loop to jump. swipe up to jump and it'll jump as long as the finger is touching the screen
        for (int i = 0; i < Input.touchCount; i++)
        {
            var touch = Input.GetTouch(i);
            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position.y;
            }

            else if (touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = touch.position.y;

                // touch.position x to swipe right to left or left to right

            }

            if (endTouchPosition > startTouchPosition)   //if endTouchPosition is greater swipe up else swipe down
            {
                transform.Translate(Vector2.up * 10 * Time.fixedDeltaTime);
            }
        }

        Vector2 dinopos = transform.position;
        if (dinopos.y <= -5)
        {
            anim.SetTrigger("dead");
            SceneManager.LoadScene("gameover");
        }

        if (score == 100)
        {
            WinText.text = "YOU WIN";
            Time.timeScale = 0; //pause the game
        }

        if (health <= 0)
        {
            anim.SetTrigger("dead");
            SceneManager.LoadScene("gameover");
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.StartsWith("cloud")) // to make cloud parent of dino
        {
            transform.gameObject.transform.parent = cloud.transform;

        }

        if (!col.gameObject.name.StartsWith("cloud"))
        {
            transform.gameObject.transform.parent = null;
        }

        if (col.gameObject.name.StartsWith("Saw"))
        {
            health -= 10;
            healthBar.value = health;

        }

        if (col.gameObject.name.StartsWith("apple"))
        {
            health += 10;
            healthBar.value = health;
            Destroy(col.gameObject);

        }

        if (col.gameObject.name.StartsWith("coin"))
        {
            score += 10;
            scoreText.text = "Score: " + score.ToString();
            Destroy(col.gameObject);

        }


    }


}