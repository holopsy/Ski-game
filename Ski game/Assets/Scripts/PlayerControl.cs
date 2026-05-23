using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    private InputAction move;
    [SerializeField] private float turnSpeed = 10;
    [SerializeField] private float speed = 10;
    Rigidbody rb;

    private void Awake()
    {
        move = InputSystem.actions.FindAction("Player/Move");
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 moveVector = move.ReadValue<Vector2>();
        float rotateSpeed = moveVector.x* turnSpeed* Time.deltaTime;
        rb.AddTorque(new Vector3(0, rotateSpeed, 0));
        float speedMultiplier = Mathf.Abs(Mathf.Cos(Mathf.Deg2Rad * transform.eulerAngles.y));
        rb.AddForce(transform.forward * speed * speedMultiplier* Time.deltaTime);
    }

    private void OnCollision()
    {
        Debug.Log("Hit Obstacle");
        rb.AddForce(obstacleKnockback, ForceMode.Impulse);
        canMove = false;
        
    }

    private void AllowMove()
    {
        canMove = true;
    }
}
