using UnityEngine;

public class DisappearingObstacle : Obstacle
{
    protected override void OnPlayerHit(GameObject player)
    {
        Debug.Log("Snowman disappeared: " + gameObject.name);
        gameObject.SetActive(false);
    }
}