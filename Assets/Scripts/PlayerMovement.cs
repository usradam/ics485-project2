using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 14f;
    [SerializeField] private float wallSlidingSpeed = 2f;
    [SerializeField] private float wallJumpingTime = 0.2f;
    [SerializeField] private float wallJumpingDuration = 0.4f;
    [SerializeField] private Vector2 wallJumpingPower = new Vector2(5f, 11f);
    [SerializeField] private float wallJumpCooldownDuration = 0.3f;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Animator anim;

    private float horizontal;
    private bool isFacingRight = true;
    private bool isWallSliding;
    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingCounter;
    private float wallJumpCooldown;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpingPower);
            anim.SetTrigger("jump");
        }

        if (Input.GetButtonUp("Jump") && body.linearVelocity.y > 0f)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, body.linearVelocity.y * 0.5f);
        }

        WallSlide();
        WallJump();

        if (!isWallJumping)
        {
            Flip();
        }

        // Update animation parameters
        anim.SetBool("run", horizontal != 0 && !isWallSliding);
        anim.SetBool("grounded", IsGrounded());
    }

    private void FixedUpdate()
    {
        if (!isWallJumping)
        {
            body.linearVelocity = new Vector2(horizontal * speed, body.linearVelocity.y);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            isWallSliding = true;
            body.linearVelocity = new Vector2(body.linearVelocity.x, Mathf.Clamp(body.linearVelocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (wallJumpCooldown > 0)
        {
            wallJumpCooldown -= Time.deltaTime;
        }

        if (isWallSliding && !isWallJumping)
        {
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else if (!isWallSliding)
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f && wallJumpCooldown <= 0)
        {
            isWallJumping = true;
            wallJumpCooldown = wallJumpCooldownDuration;  // Prevent immediate re-jump
            body.linearVelocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public bool canAttack()
    {
        return horizontal == 0 && IsGrounded() && !IsWalled();
    }
}
