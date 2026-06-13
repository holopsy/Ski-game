using System.Collections;
using UnityEngine;

public class PlayerKnockback : MonoBehaviour
{
    [Header("Knockback")]
    public float knockbackForce = 10f;
    public float knockbackTime = 0.45f;

    [Header("Hit Sound")]
    public AudioSource hitAudioSource;
    public AudioClip hitSound;

    private Rigidbody rb;
    private PlayerController playerController;
    private bool isKnockedBack;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
    }

    void OnEnable()
    {
        Obstacle.OnObstacleHit += HandleObstacleHit;
    }

    void OnDisable()
    {
        Obstacle.OnObstacleHit -= HandleObstacleHit;
    }

    void HandleObstacleHit(GameObject player, Vector3 hitDirection)
    {
        if (player != gameObject)
        {
            return;
        }

        ApplyKnockback(hitDirection);
    }

    public void ApplyKnockback(Vector3 hitDirection)
    {
        if (isKnockedBack)
        {
            return;
        }

        hitDirection.y = 0f;
        if (hitDirection.sqrMagnitude < 0.001f)
        {
            hitDirection = -transform.forward;
        }

        StartCoroutine(KnockbackRoutine(hitDirection));
    }

    IEnumerator KnockbackRoutine(Vector3 hitDirection)
    {
        isKnockedBack = true;

        if (playerController != null)
        {
            playerController.SetCanControl(false);
        }

        if (hitAudioSource != null && hitSound != null)
        {
            hitAudioSource.PlayOneShot(hitSound);
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(hitDirection.normalized * knockbackForce, ForceMode.Impulse);
        }

        yield return new WaitForSeconds(knockbackTime);

        if (playerController != null)
        {
            playerController.SetCanControl(true);
        }

        isKnockedBack = false;
    }
}
