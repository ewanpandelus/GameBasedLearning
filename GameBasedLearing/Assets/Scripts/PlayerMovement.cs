using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 3000f;
    public Rigidbody2D rb;
    private float speed = 12f;
    float horizontalMove = 0f;
    bool isgrounded = true;
    private bool m_FacingRight = true;

    // Update is called once per frame
    void Update()
    {
      horizontalMove = Input.GetAxisRaw("Horizontal")*speed;
      if (Input.GetButtonDown("Jump"))
      {
        Jump();
      }
        if (horizontalMove > 0 && !m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (horizontalMove < 0 && m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontalMove, rb.velocity.y);
    }
    private void Jump()
    {

        if (isgrounded == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, 15f);
        }
    }
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    //make sure u replace "floor" with your gameobject name.on which player is standing
    void OnCollisionEnter2D(Collision2D theCollision)
    {
        if (theCollision.gameObject.transform.tag == "floor")
        {
             isgrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D theCollision)
        {
         isgrounded = false;
        }
}

