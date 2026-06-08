using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float maxSpeed = 18f;
    public float acceleration = 8f;
    public float turnSpeed = 90f;

    [Header("Turn Limit")]
    public float maxTurnAngle = 90f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckDistance = 1.2f;
    public LayerMask groundLayer;

    private Rigidbody rb;

    private float startYRotation;
    private float currentTurnAngle = 0f;

    private bool isGrounded;
    private bool canControl = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Saves the rotation the player already has in the scene
        startYRotation = transform.eulerAngles.y;
    }

    void Update()
    {
        CheckGround();

        if (canControl && isGrounded)
        {
            TurnPlayer();
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void TurnPlayer()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        currentTurnAngle += horizontalInput * turnSpeed * Time.deltaTime;
        currentTurnAngle = Mathf.Clamp(currentTurnAngle, -maxTurnAngle, maxTurnAngle);

        transform.rotation = Quaternion.Euler(0f, startYRotation + currentTurnAngle, 0f);
    }

    void MovePlayer()
    {
        if (!isGrounded)
        {
            return;
        }

        // 1 when facing straight, 0 when turned 90 degrees sideways
        float downhillAmount = Mathf.Cos(currentTurnAngle * Mathf.Deg2Rad);
        downhillAmount = Mathf.Clamp01(downhillAmount);

        float targetSpeed = maxSpeed * downhillAmount;

        Vector3 targetVelocity = transform.forward * targetSpeed;
        Vector3 smoothVelocity = Vector3.Lerp(rb.linearVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);

        rb.linearVelocity = new Vector3(smoothVelocity.x, rb.linearVelocity.y, smoothVelocity.z);
    }

    void CheckGround()
    {
        Vector3 rayStart = groundCheck != null ? groundCheck.position : transform.position;

        isGrounded = Physics.Raycast(rayStart, Vector3.down, groundCheckDistance, groundLayer);
    }

    public void SetCanControl(bool value)
    {
        canControl = value;
    }
}