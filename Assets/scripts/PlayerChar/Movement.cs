using UnityEditor.Rendering.LookDev;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private Animator animator;
    private TrailRenderer trailRendered;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    [Header ("Movement")]
    public float speed = 5f;
    public float currentSpeed;

    [Header ("Dashing")]
    public float dashSpeed = 7f;
    public float dashTime = .2f;
    public float cooldown = .1f;

    private bool isOnCooldown = false;
    private bool _isDashing = false;
    private bool canDash = true;
    private Vector2 dashDir;

    public bool isDashing => _isDashing;   

    void Start()
    {
        currentSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        trailRendered = GetComponent<TrailRenderer>();
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput = moveInput.normalized;
        var dashInput = Input.GetKey(KeyCode.LeftShift);

        if (dashInput && canDash && !isOnCooldown)
        {
            _isDashing = true;
            canDash = false;
            isOnCooldown = true;
            trailRendered.emitting = true;

            dashDir = moveInput;
            if (dashDir == Vector2.zero)
            {
                dashDir = new Vector2(transform.localScale.x, 0f);
            }
            StartCoroutine(StopDashing());
        }
    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashTime);
        _isDashing = false;
        trailRendered.emitting = false;

        yield return new WaitForSeconds(cooldown);
        canDash = true;
        isOnCooldown = false;
    }

    public void Move(InputAction.CallbackContext context)
    {
        animator.SetBool("isMoving", true);

        if (context.canceled)
        {
            animator.SetBool("isMoving", false);
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
        }

        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);

        moveInput = context.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            rb.linearVelocity = dashDir.normalized * dashSpeed;
        }
        else
        {
            rb.linearVelocity = moveInput * currentSpeed;
        }
    }
}