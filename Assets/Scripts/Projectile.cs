using UnityEngine;

public class Projectile : MonoBehaviour {

    private Rigidbody2D rb;

    public void Init(Vector2 parentVelocity)
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = parentVelocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag != this.gameObject.tag)
        {
            Destroy(this.gameObject);
        }
    }
}
