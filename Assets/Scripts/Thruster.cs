using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Thruster : MonoBehaviour {

    [SerializeField]
    [Tooltip("Force of thruster")]
    private float force = 10;

    [SerializeField]
    public ParticleSystem jet;

    private Rigidbody2D rb;

    public bool fireThruster = false;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        jet.Stop();
	}

    private void CheckThrusterInput(KeyCode key)
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
