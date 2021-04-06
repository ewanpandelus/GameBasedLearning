///BSD 3 - Clause License

/// Copyright(c) 2021, ewanpandelus
///All rights reserved.

///Redistribution and use in source and binary forms, with or without
///modification, are permitted provided that the following conditions are met:

///1.Redistributions of source code must retain the above copyright notice, this
///list of conditions and the following disclaimer.

///2. Redistributions in binary form must reproduce the above copyright notice,
///this list of conditions and the following disclaimer in the documentation
///and/or other materials provided with the distribution.

///3. Neither the name of the copyright holder nor the names of its
///contributors may be used to endorse or promote products derived from
///this software without specific prior written permission.

///THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
///AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
///IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
///DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
///FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
///DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
///SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
///CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
///OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
///OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE

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
    private Vector3 startPoint = new Vector3(4, 0, 0);
    private float horizontalVelocity = 12f;
    float horizontalMove = 0f;
    bool isgrounded = true;
    private bool m_FacingRight = true;
    private bool isClimbing { get; set; }
    private float verticalVelocity = 0f;
    float verticalMove = 0f;
    private bool isJumping = false;
    private int cherryCount = 0;
    private AudioManager audioManager;

    private void Start()
    {
        globalDataHolder = GameObject.Find("GlobalDataHolder").GetComponent<GlobalDataHolder>();
        cherryCount = globalDataHolder.GetCherries();
        LoadPlayer();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        globalDataHolder.DisplayCherryCount();
    }

    /// <summary>
    /// Update function is called every frame
    /// for player it decides the movement for players based on certain variables
    /// If the player is jumping a vertical velocity is applied.
    /// If the player falls off the map their position is reset.
    /// If the player is climbing on a ladder they are given climbing movement,
    /// else they can move left and right using the arrow keys
    /// </summary>
    private void Update()
    {
        if(this.transform.position.y <= -35|| Input.GetKeyDown("r"))
        {
            this.transform.position = startPoint;
            rb.velocity = new Vector2(0f,0f);
            return;
        }
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

    /// <summary>
    /// Provides vertical movement while climbing
    /// </summary>
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

    /// <summary>
    /// Provides non-climbing movement, character can jump and walk/run
    /// left and right.
    /// </summary>
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

    /// <summary>
    /// Moves the character in fixed increments, based on preset velocities
    /// </summary>
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

    /// <summary>
    /// Flips the direction the character is facing
    /// </summary>
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

    /// <summary>
    /// Sets the character as not jumping while colliding with the floor,
    /// so that it can jump.
    /// </summary>
    /// <param name="theCollision">
    /// The variable which holds the collision data</param>
    private void OnCollisionEnter2D(Collision2D theCollision)
    {
        if (theCollision.gameObject.transform.tag == "Floor" && theCollision.gameObject.name != "Ladder")
        {
            isgrounded = true;
            animator.SetBool("IsJumping", false);
            isJumping = false;
        }
    }
    /// <summary>
    /// Increment cherry count and play sound if character
    /// collides with cherry.
    /// </summary>
    /// <param name="collider">Variable which holds collision dara</param>
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.transform.tag == "Cherry")
        {
            cherryCount++;
            globalDataHolder.SetCherries(cherryCount);
            globalDataHolder.DisplayCherryCount();
            audioManager.Play("Crunch");
            Destroy(collider.gameObject);
        }
    }

    void OnCollisionExit2D(Collision2D theCollision)
    {
        isgrounded = false;
    }

    /// <summary>
    /// Loads player data when scene is initialised
    /// </summary>
    private void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data!=null)
        {
            this.cherryCount = globalDataHolder.GetCherries();
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