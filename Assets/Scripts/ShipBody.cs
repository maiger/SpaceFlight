using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipBody : MonoBehaviour {

    [SerializeField]
    [Tooltip("Can set a custom center of mass for the object. Basically a pivot point when applying forces e.g. with thrusters")]
    private Transform centerOfMass;

    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		if(centerOfMass != null)
        {
            rb = GetComponent<Rigidbody2D>();
            rb.centerOfMass = centerOfMass.position;
        }
	}
}
