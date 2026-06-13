using UnityEngine;

public class SnowballHit : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        PlayerKnockback playerKnockback =
            collision.collider.GetComponentInParent<PlayerKnockback>();

        if (playerKnockback == null)
        {
            return;
        }

        Vector3 hitDirection = collision.transform.position - transform.position;
        playerKnockback.ApplyKnockback(hitDirection);
        Destroy(gameObject);
    }
}
