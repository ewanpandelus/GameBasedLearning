using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public Rigidbody2D rb;
    private float horizontalVelocity = 12f;
    float horizontalMove = 0f;
    bool isgrounded = true;
    private bool m_FacingRight = true;
    private bool isClimbing = false;
    private float verticalVelocity = 0f;
    float verticalMove = 0f;

    // Update is called once per frame
    void Update()
    {
        animator.speed = 1f;
        if (!isClimbing)
        {
            verticalVelocity = 0f;
            horizontalVelocity = 12f;
            NonClimbingMovement();
        }
        else
        {
            verticalVelocity = 9f;
            horizontalVelocity = 3f; 
            ClimbingMovement();
        }
    }
    private void ClimbingMovement()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * horizontalVelocity;
        verticalMove = Input.GetAxisRaw("Vertical") * verticalVelocity;
        animator.SetBool("IsJumping", false);
        animator.SetBool("IsClimbing", true);
        if (verticalMove < 0.01)
        {
            animator.speed = 0f;
        }
    }
    private void NonClimbingMovement()
    {
        
        animator.SetBool("IsClimbing", false);
        horizontalMove = Input.GetAxisRaw("Horizontal") * horizontalVelocity;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
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
        if (!isClimbing)
        {
            rb.velocity = new Vector2(horizontalMove, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(horizontalMove, verticalMove);
        }
    }
    private void Jump()
    {

        if (isgrounded == true)
        {
            animator.SetBool("IsJumping", true);
            rb.velocity = new Vector2(rb.velocity.x, 17.5f);
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
            animator.SetBool("IsJumping", false);
        }
    }

    void OnCollisionExit2D(Collision2D theCollision)
        {
         isgrounded = false;
        }
    public void SetIsClimbing(bool _isClimbing)
    {
        isClimbing = _isClimbing;
    }
}

