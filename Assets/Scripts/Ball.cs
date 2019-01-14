using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    Vector3 initialVelocity;

    [SerializeField]
    float minVelocity = 10f;

    Vector3 prevVelocity;
    Rigidbody ballRB;

    void OnEnable()
    {
        ballRB = GetComponent<Rigidbody>();
        ballRB.velocity = initialVelocity;
    }

    void Update()
    {
        prevVelocity = ballRB.velocity;
    }

    void OnCollisionEnter(Collision collision)
    {
        Bounce(collision.contacts[0].normal);
    }

    void Bounce(Vector3 collisionNormal)
    {
        var speed = prevVelocity.magnitude;
        var direction = Vector3.Reflect(prevVelocity.normalized, collisionNormal);
        ballRB.velocity = direction * Mathf.Max(speed, minVelocity);
    }
}