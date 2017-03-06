using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipBody : MonoBehaviour {

    [SerializeField]
    [Tooltip("Can set a custom center of mass for the object. Basically a pivot point when applying forces e.g. with thrusters")]
    private Transform centerOfMass;

    private Rigidbody2D rb;

    private Thruster[] thrusters;
    private Weapon[] weapons;

	// Use this for initialization
	void Start () {
		if(centerOfMass != null)
        {
            rb = GetComponent<Rigidbody2D>();
            rb.centerOfMass = centerOfMass.position;
        }

        // Thrusters are in the second child of the ship object
        thrusters = transform.GetChild(1).GetComponentsInChildren<Thruster>();
        Debug.Log("Thrusters found: " + thrusters.Length);

        // Weapons are in the third child of the ship object
        weapons = transform.GetChild(2).GetComponentsInChildren<Weapon>();
        Debug.Log("Weapons found: " + weapons.Length);

        // TODO: Sort through available thrusters and figure out where they are on the ship and what they do
        // Which thursters move the ship forward, rotate, lateral movement, backwards?
        // Could be calculated by referencing thruster position and ship center of mass position
    }

    // TODO: Update function
    // Registers player inputs and calls appropriate thruster to turn on
}
