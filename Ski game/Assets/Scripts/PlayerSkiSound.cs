using UnityEngine;

public class PlayerSkiSound : MonoBehaviour
{
    [Header("Ski Sound")]
    public AudioSource skiAudioSource;

    [Header("Settings")]
    public float minSpeedToPlay = 1.5f;
    public float maxSpeedForPitch = 18f;
    public float minPitch = 0.85f;
    public float maxPitch = 1.25f;

    private Rigidbody rb;
    private PlayerController playerController;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (skiAudioSource == null)
        {
            return;
        }

        if (Time.timeScale == 0f ||
            (RaceManager.Instance != null && RaceManager.Instance.raceFinished))
        {
            if (skiAudioSource.isPlaying)
            {
                skiAudioSource.Stop();
            }

            return;
        }

        if (rb == null || playerController == null)
        {
            return;
        }

        float horizontalSpeed = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z).magnitude;

        bool shouldPlay = playerController.IsGrounded() && horizontalSpeed > minSpeedToPlay;

        if (shouldPlay)
        {
            if (!skiAudioSource.isPlaying)
            {
                skiAudioSource.Play();
            }

            float speedPercent = Mathf.Clamp01(horizontalSpeed / maxSpeedForPitch);
            skiAudioSource.pitch = Mathf.Lerp(minPitch, maxPitch, speedPercent);
        }
        else
        {
            if (skiAudioSource.isPlaying)
            {
                skiAudioSource.Stop();
            }
        }
    }
}
