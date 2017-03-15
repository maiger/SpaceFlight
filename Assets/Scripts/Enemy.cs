using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField]
    // This could be done with layermasks to allow for more customisation?
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
