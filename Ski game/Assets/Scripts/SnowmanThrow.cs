using System.Collections;
using UnityEngine;

public class SnowmanThrow : MonoBehaviour
{
    [Header("Throw")]
    public GameObject snowBall;
    public float throwDistance = 50f;
    public float throwSpeed = 1250f;
    public float aimHeight = 1f;
    public float movementPrediction = 0.25f;

    [Header("Timing")]
    public float minThrowCooldown = 3f;
    public float maxThrowCooldown = 5f;
    public float snowballLifetime = 8f;

    private Transform target;
    private Rigidbody targetRigidbody;
    private Coroutine throwRoutine;

    void OnEnable()
    {
        throwRoutine = StartCoroutine(ThrowRoutine());
    }

    void OnDisable()
    {
        if (throwRoutine != null)
        {
            StopCoroutine(throwRoutine);
            throwRoutine = null;
        }
    }

    IEnumerator ThrowRoutine()
    {
        yield return new WaitForSeconds(Random.Range(0f, maxThrowCooldown));

        while (true)
        {
            FindTarget();

            if (target != null &&
                Vector3.SqrMagnitude(target.position - transform.position) <=
                throwDistance * throwDistance)
            {
                ThrowSnowball();
            }

            yield return new WaitForSeconds(GetRandomCooldown());
        }
    }

    void FindTarget()
    {
        if (target != null)
        {
            return;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
            targetRigidbody = player.GetComponent<Rigidbody>();
        }
    }

    void ThrowSnowball()
    {
        if (snowBall == null)
        {
            return;
        }

        Vector3 aimPoint = target.position + Vector3.up * aimHeight;
        if (targetRigidbody != null)
        {
            aimPoint += targetRigidbody.linearVelocity * movementPrediction;
        }

        Vector3 targetDirection = (aimPoint - transform.position).normalized;
        Quaternion throwRotation = targetDirection.sqrMagnitude > 0f
            ? Quaternion.LookRotation(targetDirection)
            : transform.rotation;

        GameObject spawnedSnowball =
            Instantiate(snowBall, transform.position, throwRotation);

        if (snowballLifetime > 0f)
        {
            Destroy(spawnedSnowball, snowballLifetime);
        }

        Rigidbody snowballRigidbody = spawnedSnowball.GetComponent<Rigidbody>();
        if (snowballRigidbody == null)
        {
            return;
        }

        snowballRigidbody.AddForce(targetDirection * throwSpeed);
    }

    float GetRandomCooldown()
    {
        float minimum = Mathf.Max(0.1f, minThrowCooldown);
        float maximum = Mathf.Max(minimum, maxThrowCooldown);
        return Random.Range(minimum, maximum);
    }
}
