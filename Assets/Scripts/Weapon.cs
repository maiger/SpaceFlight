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

    [SerializeField]
    private float strayFactor;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Shoot()
    {
        var randomNumberX = Random.Range(-strayFactor, strayFactor);
        var randomNumberY = Random.Range(-strayFactor, strayFactor);
        var randomNumberZ = Random.Range(-strayFactor, strayFactor);

        GameObject projectileInstance =  Instantiate(projectile, firePoint.position, firePoint.rotation);
        // Add the current velocity of weapon to projectile
        projectileInstance.GetComponent<Projectile>().Init(rb.velocity);

        projectileInstance.transform.Rotate(randomNumberX, randomNumberY, randomNumberZ);
        projectileInstance.GetComponent<Rigidbody2D>().AddForce(projectileInstance.transform.right * force);

        // Add force to the opposite direction of firing. This could be done better by
        // making this depend on the projectile used and not a value on the weapon itself.
        rb.AddForce(-transform.right * force);
    }
}
