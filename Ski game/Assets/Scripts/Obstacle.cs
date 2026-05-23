using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public delegate void OnHitAction();
    public static event OnHitAction OnObstacleHit;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player")) OnObstacleHit?.Invoke();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
