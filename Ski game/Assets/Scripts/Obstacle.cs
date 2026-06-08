using UnityEngine;
using System;

public class Obstacle : MonoBehaviour
{
    public static event Action<GameObject, Vector3> OnObstacleHit;

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 hitDirection = collision.transform.position - transform.position;
            hitDirection.y = 0f;
            hitDirection.Normalize();

            Debug.Log("Player hit obstacle: " + gameObject.name);

            OnObstacleHit?.Invoke(collision.gameObject, hitDirection);

            OnPlayerHit(collision.gameObject);
        }
    }

    protected virtual void OnPlayerHit(GameObject player)
    {
        // Extra obstacle behavior can go here.
    }
}