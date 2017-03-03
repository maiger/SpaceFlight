using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Thruster : MonoBehaviour {

    [SerializeField]
    [Tooltip("Button to activate thruster")]
    KeyCode key;

    [SerializeField]
    [Tooltip("Force of thruster")]
    float force = 10;

    [SerializeField]
    ParticleSystem jet;

    Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        jet.Stop();

	}

    // FIX: Getting user input in fixedupdate will likely end up with input loss
    private void FixedUpdate()
    {
        if (Input.GetKey(key))
        {
            rb.AddForce(transform.right * force);
            jet.Play();
            //Debug.Log("Thrusting!");
        }

        if (Input.GetKeyUp(key))
        {
            jet.Stop();
        }
    }
}
