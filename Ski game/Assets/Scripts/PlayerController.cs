using System.Collections;
using UnityEngine;
using TMPro;

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

    [Header("Boost")]
    public KeyCode boostKey = KeyCode.Space;
    public float boostMultiplier = 1.6f;
    public float boostDuration = 1.5f;
    public float boostCooldown = 4f;
    
    [Header("Boost Effects")]
    public ParticleSystem boostParticles;
    public AudioSource audioSource;
    public AudioClip boostSound;
    
    [Header("Boost UI")]
    public TMP_Text boostStatusText;
    public Color boostReadyColor = Color.green;
    public Color boostingColor = new Color(1f, 0.5f, 0f); // orange
    public Color boostCooldownColor = Color.red;

    private Rigidbody rb;

    private float startYRotation;
    private float currentTurnAngle = 0f;

    private bool isGrounded;
    private bool canControl = true;

    private bool isBoosting;
    private bool canBoost = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startYRotation = transform.eulerAngles.y;
        UpdateBoostUI("Boost Ready", boostReadyColor);
    }

    void Update()
    {
        CheckGround();

        if (canControl && isGrounded)
        {
            TurnPlayer();
        }

        if (canControl && Input.GetKeyDown(boostKey) && canBoost && isGrounded)
        {
            StartCoroutine(BoostRoutine());
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
        if (!canControl || !isGrounded)
        {
            return;
        }

        float downhillAmount = Mathf.Cos(currentTurnAngle * Mathf.Deg2Rad);
        downhillAmount = Mathf.Clamp01(downhillAmount);

        float speed = maxSpeed;

        if (isBoosting)
        {
            speed *= boostMultiplier;
        }

        float targetSpeed = speed * downhillAmount;

        Vector3 targetVelocity = transform.forward * targetSpeed;
        Vector3 smoothVelocity = Vector3.Lerp(rb.linearVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);

        rb.linearVelocity = new Vector3(smoothVelocity.x, rb.linearVelocity.y, smoothVelocity.z);
    }

    void CheckGround()
    {
        Vector3 rayStart = groundCheck != null ? groundCheck.position : transform.position;
        isGrounded = Physics.Raycast(rayStart, Vector3.down, groundCheckDistance, groundLayer);
    }

    IEnumerator BoostRoutine()
    {
        canBoost = false;
        isBoosting = true;

        UpdateBoostUI("Boosting!", boostingColor);

        if (boostParticles != null)
        {
            boostParticles.Play();
        }

        if (audioSource != null && boostSound != null)
        {
            audioSource.PlayOneShot(boostSound);
        }

        Debug.Log("Boost started");

        yield return new WaitForSeconds(boostDuration);

        isBoosting = false;

        if (boostParticles != null)
        {
            boostParticles.Stop();
        }

        UpdateBoostUI("Boost Recharging...", boostCooldownColor);

        Debug.Log("Boost ended. Cooldown started");

        yield return new WaitForSeconds(boostCooldown);

        canBoost = true;

        UpdateBoostUI("Boost Ready", boostReadyColor);

        Debug.Log("Boost ready");
    }
    
    void UpdateBoostUI(string message, Color color)
    {
        if (boostStatusText != null)
        {
            boostStatusText.text = message;
            boostStatusText.color = color;
        }
    }

    public void SetCanControl(bool value)
    {
        canControl = value;
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }
}
