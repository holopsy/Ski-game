using System.Collections;
using UnityEngine;

public class PlayerKnockback : MonoBehaviour
{
    [Header("Knockback Settings")]
    public float knockbackForce = 12f;
    public float knockbackTime = 0.5f;

    [Header("Audio")]
    public AudioSource audioSource;
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

        if (isKnockedBack)
        {
            return;
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

        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }

        rb.linearVelocity = Vector3.zero;
        rb.AddForce(hitDirection * knockbackForce, ForceMode.Impulse);

        yield return new WaitForSeconds(knockbackTime);

        if (playerController != null)
        {
            playerController.SetCanControl(true);
        }

        isKnockedBack = false;
    }
}