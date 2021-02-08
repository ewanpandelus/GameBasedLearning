using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IPlayerMovement
{
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI cherryText;
    private GlobalDataHolder globalDataHolder;
    public Rigidbody2D rb;
    private float horizontalVelocity = 12f;
    float horizontalMove = 0f;
    bool isgrounded = true;
    private bool m_FacingRight = true;
    private bool isClimbing { get; set; }
    private float verticalVelocity = 0f;
    float verticalMove = 0f;
    private bool isJumping = false;
    private int cherryCount = 0;


    private void Start()
    {
        globalDataHolder = GameObject.Find("GlobalDataHolder").GetComponent<GlobalDataHolder>();
        cherryCount = globalDataHolder.GetCherries();
        LoadPlayer();
        globalDataHolder.DisplayCherryCount();
       
        
    }
    private void Update()
    {
        animator.speed = 1f;

        if (isJumping)
        {
            verticalVelocity = rb.velocity.y;
            horizontalVelocity = 12f;
        }
        if (!isClimbing)
        {
            verticalVelocity = 0f;
            horizontalVelocity = 12f;
            NonClimbingMovement();
        }
        if (!isJumping && isClimbing)
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
        animator.SetBool("IsClimbing", isClimbing);
        if (verticalMove < 0.01)
        {
            animator.speed = 0f;
        }
        if (Input.GetButtonDown("Jump"))
        {
            animator.speed = 1f;

            Jump();

        }
        FlipCharacter();
    }

    private void NonClimbingMovement()
    {

        animator.SetBool("IsClimbing", false);
        horizontalMove = Input.GetAxisRaw("Horizontal") * horizontalVelocity;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            animator.speed = 1f;

            Jump();

        }
        FlipCharacter();

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

        if (isgrounded == true || isClimbing == true)
        {
            isClimbing = false;
            isJumping = true;
            animator.SetBool("IsClimbing", false);
            animator.SetBool("IsJumping", true);
            rb.velocity = new Vector2(rb.velocity.x, 17.5f);
        }
    }
    private void FlipCharacter()
    {
        if (horizontalMove > 0 && !m_FacingRight)
        {
            Flip();
        }
        else if (horizontalMove < 0 && m_FacingRight)
        {
            Flip();
        }
    }
    private void Flip()
    {
        m_FacingRight = !m_FacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    private void OnCollisionEnter2D(Collision2D theCollision)
    {
        if (theCollision.gameObject.transform.tag == "Floor" && theCollision.gameObject.name != "Ladder")
        {

            isgrounded = true;
            animator.SetBool("IsJumping", false);
            isJumping = false;

        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.transform.tag == "Cherry")
        {
            cherryCount++;
            globalDataHolder.SetCherries(cherryCount);
            globalDataHolder.DisplayCherryCount();
            Destroy(collider.gameObject);
        }
    }


    void OnCollisionExit2D(Collision2D theCollision)
    {
        isgrounded = false;
    }
    private void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPLayer();
        if (data!=null)
        {
            this.cherryCount = data.totalCherries;
            Vector3 position;
            position.x = data.position[0];
            position.y = data.position[1];
            position.z = data.position[2];
            this.transform.position = position;
        }
       

    }
    public float GetHorizontalVelocity()
    {
        return this.horizontalVelocity;
    }
    public float GetVerticalVelocity()
    {
        return this.rb.velocity.y;

    }
    public bool GetIsJumping()
    {
        return this.isJumping;
    }
    public void SetIsJumping(bool _isJumping)
    {
        isJumping = _isJumping;
    }
    public bool GetIsClimbing()
    {
        return this.isClimbing;
    }
    public void SetIsClimbing(bool _isClimbing)
    {
        isClimbing = _isClimbing;
    }
    public void SetCherries(int count)
    {
        this.cherryCount = count;
    }
    public int GetCherries()
    {
        return this.cherryCount;
    }
    public Vector3 GetPosition()
    {
        return this.transform.position;
    }


}