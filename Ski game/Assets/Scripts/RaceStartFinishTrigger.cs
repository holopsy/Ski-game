using UnityEngine;

public class RaceStartFinishTrigger : MonoBehaviour
{
    public enum TriggerType
    {
        Start,
        Finish
    }

    public TriggerType triggerType;

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

        if (triggerType == TriggerType.Start)
        {
            RaceManager.Instance.StartRace();
        }
        else if (triggerType == TriggerType.Finish)
        {
            RaceManager.Instance.FinishRace();
        }
    }
}