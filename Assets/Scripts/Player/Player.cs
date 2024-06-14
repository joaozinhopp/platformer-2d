using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public SOPlayerSetup soPlayerSetup;

    public Rigidbody2D myRigidbody;
    public Vector2 friction = new Vector2(.1f, 0);
    public float speed;
    public float speedRun;
    public float forceJump = 2;
    private float _currentSpeed;
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = 0.7f;
    public float animationDuration = .3f;
    public string boolRun = "Run";
    public Animator animator;
    public SOFloat soJumpScaleY;
    public SOFloat soJumpScaleX;
    public SOFloat soAnimationDuration;
    private bool isJumping = false;
    public Collider2D collider2D;
    public float distToGround;
    public float spaceToGround = .1f;
    public ParticleSystem jumpVFX;

    // Add these fields for the movement sound
    public AudioSource movementAudioSource;
    public AudioClip movementClip;

    // Add these fields for the jump sound
    public AudioSource jumpAudioSource;
    public AudioClip jumpClip;

    private void Awake()
    {
        if (collider2D != null)
        {
            distToGround = collider2D.bounds.extents.y;
        }

        // Set up the AudioSource component for movement sound
        if (movementAudioSource == null)
        {
            movementAudioSource = gameObject.AddComponent<AudioSource>();
            movementAudioSource.clip = movementClip;
            movementAudioSource.loop = true; // Loop the movement sound
            movementAudioSource.playOnAwake = false; // Do not play on awake
        }

        // Set up the AudioSource component for jump sound
        if (jumpAudioSource == null)
        {
            jumpAudioSource = gameObject.AddComponent<AudioSource>();
            jumpAudioSource.clip = jumpClip;
            jumpAudioSource.playOnAwake = false; // Do not play on awake
        }
    }

    private bool isGrounded()
    {
        Debug.DrawRay(transform.position, -Vector2.up, Color.magenta, distToGround + spaceToGround);
        return Physics2D.Raycast(transform.position, -Vector2.up, distToGround + spaceToGround);
    }

    private void Update()
    {
        isGrounded();
        HandleJump();
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.LeftControl))
            _currentSpeed = speedRun;
        else
            _currentSpeed = speed;

        bool isMoving = false;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            myRigidbody.velocity = new Vector2(-_currentSpeed, myRigidbody.velocity.y);
            if (myRigidbody.transform.localScale.x != -1)
            {
                myRigidbody.transform.DOScaleX(-1, .1f);
            }
            animator.SetBool(boolRun, true);
            isMoving = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            myRigidbody.velocity = new Vector2(_currentSpeed, myRigidbody.velocity.y);
            if (myRigidbody.transform.localScale.x != 1)
            {
                myRigidbody.transform.DOScaleX(1, .1f);
            }
            animator.SetBool(boolRun, true);
            isMoving = true;
        }
        else
        {
            animator.SetBool(boolRun, false);
        }

        if (isMoving)
        {
            if (!movementAudioSource.isPlaying)
            {
                movementAudioSource.Play();
            }
        }
        else
        {
            if (movementAudioSource.isPlaying)
            {
                movementAudioSource.Stop();
            }
        }

        if (myRigidbody.velocity.x > 0)
        {
            myRigidbody.velocity += friction;
        }
        else if (myRigidbody.velocity.x < 0)
        {
            myRigidbody.velocity -= friction;
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping && isGrounded())
        {
            myRigidbody.velocity = Vector2.up * forceJump;
            isJumping = true;
            HandleScaleJump();
            PlayJumpVFX();
            PlayJumpSound(); // Play the jump sound
        }
    }

    private void PlayJumpVFX()
    {
        if (jumpVFX != null) jumpVFX.Play();
    }

    private void PlayJumpSound()
    {
        if (jumpAudioSource != null && jumpClip != null)
        {
            jumpAudioSource.PlayOneShot(jumpClip);
        }
    }

    private void HandleScaleJump()
    {
        myRigidbody.transform.DOScaleY(soJumpScaleY.value, soAnimationDuration.value).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
        {
            isJumping = false;
        });
        myRigidbody.transform.DOScaleX(soJumpScaleY.value, soAnimationDuration.value).SetLoops(2, LoopType.Yoyo);
    }
}
