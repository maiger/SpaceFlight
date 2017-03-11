using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Weapon : MonoBehaviour {

    [SerializeField]
    [Tooltip("Projectile the weapon uses")]
    private GameObject projectile;

    [SerializeField]
    [Tooltip("Point to spawn projectile")]
    private Transform firePoint;

    [SerializeField]
    [Tooltip("Force to be applied to weapon when fired")]
    private float force;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Shoot()
    {
        GameObject projectileInstance =  Instantiate(projectile, firePoint.position, firePoint.rotation);
        // Add the current velocity of weapon to projectile
        projectileInstance.GetComponent<Projectile>().Init(rb.velocity);
        // Add force to the opposite direction of firing. This could be done better by
        // making this depend on the projectile used and not a value on the weapon itself.
        rb.AddForce(-transform.right * force);
    }
}
