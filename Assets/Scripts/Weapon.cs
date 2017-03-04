using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Weapon : MonoBehaviour {

    [SerializeField]
    [Tooltip("Projectile the weapon uses")]
    GameObject projectile;

    [SerializeField]
    [Tooltip("Point to spawn projectile")]
    Transform firePoint;

    [SerializeField]
    [Tooltip("Force to be applied to weapon when fired")]
    float force;

    [SerializeField]
    [Tooltip("Button to activate weapon")]
    KeyCode key;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update () {
        if (Input.GetKeyDown(key))
        {
            Shoot();
        }
	}

    void Shoot()
    {
        Instantiate(projectile, firePoint.position, firePoint.rotation);
        // Add force to the opposite direction of firing. This could be done better by
        // making this depend on the projectile used and not a value on the weapon itself.
        rb.AddForce(-transform.right * force);
    }
}
