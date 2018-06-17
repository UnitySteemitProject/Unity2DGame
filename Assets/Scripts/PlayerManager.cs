﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    Animator anim;
    Rigidbody2D rb;

    public float speedX;
    public float jumpSpeedY;
    public float delayBeforeDoubleJump;

    bool facingRight, Jumping, isGrounded, canDoubleJump;
    float speed;


    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        facingRight = true;
    }

    // Update is called once per frame
    void Update () {
        Flip();
        MovePlayer(speed);
        HandleJump();
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            speed = -speedX;
        }
        if(Input.GetKeyUp(KeyCode.LeftArrow))
        {
            speed = 0;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            speed = +speedX;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            speed = 0;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
            //Jumping = true;
            //rb.AddForce(new Vector2(rb.velocity.x, jumpSpeedY)); //add particular direction
            //anim.SetInteger("State", 3);
        }
    }

    private void HandleJump()
    {
        if(Jumping)
        {
            if (rb.velocity.y > 0) //moving up!!
            {
                anim.SetInteger("State", 3);
            }
            else
            {
                anim.SetInteger("State", 1);
            }


        }
    }

    void MovePlayer(float playerSpeed)
    {
        if(playerSpeed<0 && !Jumping || playerSpeed>0 && !Jumping)
            anim.SetInteger("State", 2);
        if(playerSpeed == 0 && !Jumping)
            anim.SetInteger("State", 0);

        rb.velocity = new Vector3(speed, rb.velocity.y, 0);

    }

    void Flip()
    {
        if(speed>0 && !facingRight || speed<0 && facingRight)
        {
            facingRight = !facingRight;
            //transform.localScale.x = -transform.localScale.x;
            Vector3 curScale = transform.localScale;
            curScale.x *= -1;
            transform.localScale = curScale;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            Jumping = false;
            canDoubleJump = false;
            anim.SetInteger("State", 0);
        }
    }

    public void WalkLeft()
    {
        speed = -speedX;
    }

    public void WalkRight()
    {
        speed = speedX;
    }

    public void StopMoving()
    {
        speed = 0;
    }

    public void Jump()
    {
        // single jump
        if (isGrounded)
        {
            isGrounded = false;
            Jumping = true;
            rb.AddForce(new Vector2(rb.velocity.x, jumpSpeedY)); //add particular direction
            anim.SetInteger("State", 3);
            Invoke("EnableDoubleJump", delayBeforeDoubleJump);
        }
        // double jump
        if (canDoubleJump)
        {
            canDoubleJump = false;
            rb.AddForce(new Vector2(rb.velocity.x, jumpSpeedY)); //add particular direction
            anim.SetInteger("State", 3);
        }

    }

    void EnableDoubleJump()
    {
        canDoubleJump = true;
    }

}
