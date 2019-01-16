using UnityEngine;

public class Ball : MonoBehaviour
{
    public Vector3 initialVelocity;
    [SerializeField] float minVelocity = 10f;
    [SerializeField] int deflectsRemaining = 10;

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
        if (--deflectsRemaining <= 0 || collision.collider.CompareTag(Constants.ENEMY_TAG))
            Destroy(gameObject);
        Bounce(collision.contacts[0].normal);
    }

    void Bounce(Vector3 collisionNormal)
    {
        var speed = prevVelocity.magnitude;
        var direction = Vector3.Reflect(prevVelocity.normalized, collisionNormal);
        ballRB.velocity = direction * Mathf.Max(speed, minVelocity);
    }
}