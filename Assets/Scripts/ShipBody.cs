using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipBody : MonoBehaviour {

    [SerializeField]
    [Tooltip("Can set a custom center of mass for the object. Basically a pivot point when applying forces e.g. with thrusters")]
    private Transform centerOfMass;

    [SerializeField]
    KeyCode forward = KeyCode.W;

    [SerializeField]
    KeyCode backward = KeyCode.S;

    [SerializeField]
    KeyCode right = KeyCode.D;

    [SerializeField]
    KeyCode left = KeyCode.A;

    [SerializeField]
    KeyCode rotationRight = KeyCode.E;

    [SerializeField]
    KeyCode rotationLeft = KeyCode.Q;

    private Rigidbody2D rb;

    private Thruster[] thrusters;
    private Weapon[] weapons;

    private List<Thruster> thrusterForwards;
    //private Thruster[] thrusterBackwards;
    //private Thruster[] thrusterLeft;
    //private Thruster[] thrusterRight;
    //private Thruster[] thrusterRotateLeft;
    //private Thruster[] thrusterRotateRight;

    // Use this for initialization
    void Start () {
		if(centerOfMass != null)
        {
            rb = GetComponent<Rigidbody2D>();
            rb.centerOfMass = centerOfMass.position;
        }

        thrusterForwards = new List<Thruster>();

        // Thrusters are in the second child of the ship object
        thrusters = transform.GetChild(1).GetComponentsInChildren<Thruster>();
        Debug.Log("Thrusters found: " + thrusters.Length);

        // Weapons are in the third child of the ship object
        weapons = transform.GetChild(2).GetComponentsInChildren<Weapon>();
        Debug.Log("Weapons found: " + weapons.Length);

        FindForwardThrusters();

        // TODO: Sort through available thrusters and figure out where they are on the ship and what they do
        // Which thursters move the ship forward, rotate, lateral movement, backwards?
        // Could be calculated by referencing thruster position and ship center of mass position
    }

    private void FindForwardThrusters()
    {
        foreach (Thruster thruster in thrusters)
        {
            if (thruster.transform.localRotation.eulerAngles.z == 0 && (thruster.transform.localPosition.x - centerOfMass.transform.localPosition.x) < 0)
            {
                thrusterForwards.Add(thruster);
            }
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(forward))
        {
            foreach (Thruster thruster in thrusterForwards)
            {
                thruster.fireThruster = true;
                thruster.jet.Play();
            }
        }

        if (Input.GetKeyUp(forward))
        {
            foreach (Thruster thruster in thrusterForwards)
            {
                thruster.fireThruster = false;
                thruster.jet.Stop();
            }
        }
    }
    // TODO: Update function
    // Registers player inputs and calls appropriate thruster to turn on
}
