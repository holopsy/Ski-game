using UnityEngine;

public class PenaltyZone : MonoBehaviour
{
    public float penaltyAmount = 1f;
    public AudioSource wrongSideAudioSource;
    public AudioClip wrongSideSound;

    private bool triggered;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        if (triggered)
        {
            return;
        }

        if (RaceManager.Instance == null ||
            !RaceManager.Instance.AddPenalty(penaltyAmount))
        {
            return;
        }

        triggered = true;

        if (wrongSideAudioSource != null && wrongSideSound != null)
        {
            wrongSideAudioSource.PlayOneShot(wrongSideSound);
        }

        Debug.Log("Wrong side penalty: +" + penaltyAmount + " second");
    }
}
