using UnityEngine;

public class PenaltyZone : MonoBehaviour
{
    public float penaltyAmount = 1f;
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

        triggered = true;

        if (RaceManager.Instance != null)
        {
            RaceManager.Instance.AddPenalty(penaltyAmount);
        }

        Debug.Log("Wrong side penalty: +" + penaltyAmount + " second");
    }
}