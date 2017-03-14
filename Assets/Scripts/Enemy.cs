using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField]
    private string projectileTag = "Projectile";

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == projectileTag)
        {
            // A projectile hit us. We are destroyed ;(
            Destroy(this.gameObject);
        }
    }
}
