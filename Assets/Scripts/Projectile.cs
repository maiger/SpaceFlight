using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField]
    [Tooltip("Force applied to the projectile at start")]
    private int force = 20;

    private Rigidbody2D rb;

    public void Init(Vector2 parentVelocity)
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = parentVelocity;

        rb.AddForce(transform.right * force);
    }
}
