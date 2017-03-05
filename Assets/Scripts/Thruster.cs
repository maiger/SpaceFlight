using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Thruster : MonoBehaviour {

    [SerializeField]
    [Tooltip("Button to activate thruster")]
    private KeyCode key;

    [SerializeField]
    [Tooltip("Force of thruster")]
    private float force = 10;

    [SerializeField]
    private ParticleSystem jet;

    private Rigidbody2D rb;

    private bool fireThruster = false;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        jet.Stop();
	}

    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            jet.Play();
            fireThruster = true;
            Debug.Log("KeyDown");
        }

        if (Input.GetKeyUp(key))
        {
            jet.Stop();
            fireThruster = false;
            Debug.Log("KeyUp");
        }
    }

    private void FixedUpdate()
    {
        if (fireThruster)
        {
            rb.AddForce(transform.right * force);
        }
    }
}
