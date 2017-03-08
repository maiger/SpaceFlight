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
    private List<Thruster> thrusterBackwards;
    private List<Thruster> thrusterLeft;
    private List<Thruster> thrusterRight;
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
        thrusterBackwards = new List<Thruster>();
        thrusterLeft = new List<Thruster>();
        thrusterRight = new List<Thruster>();

        // Thrusters are in the second child of the ship object
        thrusters = transform.GetChild(1).GetComponentsInChildren<Thruster>();
        Debug.Log("Thrusters found: " + thrusters.Length);

        // Weapons are in the third child of the ship object
        weapons = transform.GetChild(2).GetComponentsInChildren<Weapon>();
        Debug.Log("Weapons found: " + weapons.Length);

        FindForwardThrusters();
        FindBackwardThrusters();
        FindLeftThrusters();
        FindRightThrusters();
    }

    // TODO: Create one function to do all this
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

    private void FindBackwardThrusters()
    {
        foreach (Thruster thruster in thrusters)
        {
            if (thruster.transform.localRotation.eulerAngles.z == 180 && (thruster.transform.localPosition.x - centerOfMass.transform.localPosition.x) > 0)
            {
                thrusterBackwards.Add(thruster);
            }
        }
    }

    private void FindLeftThrusters()
    {
        foreach (Thruster thruster in thrusters)
        {
            if (thruster.transform.localRotation.eulerAngles.z == 270 && (thruster.transform.localPosition.y - centerOfMass.transform.localPosition.y) > 0)
            {
                thrusterLeft.Add(thruster);
            }
            Debug.Log(thruster.gameObject.name + " " + thruster.transform.localRotation.eulerAngles.z + " " + (thruster.transform.localPosition.y - centerOfMass.transform.localPosition.y));
        }
        Debug.Log("Found left thrusters: " + thrusterLeft.Count);
    }

    private void FindRightThrusters()
    {
        foreach (Thruster thruster in thrusters)
        {
            if (thruster.transform.localRotation.eulerAngles.z == 90 && (thruster.transform.localPosition.y - centerOfMass.transform.localPosition.y) < 0)
            {
                thrusterRight.Add(thruster);
            }
        }
    }

    private void Update()
    {
        fireThrusters(forward, thrusterForwards);
        fireThrusters(backward, thrusterBackwards);
        fireThrusters(left, thrusterRight);
        fireThrusters(right, thrusterLeft);

    }

    private void fireThrusters(KeyCode key, List<Thruster> thrusters)
    {
        if (Input.GetKeyDown(key))
        {
            foreach (Thruster thruster in thrusters)
            {
                thruster.fireThruster = true;
                thruster.jet.Play();
            }
        }

        if (Input.GetKeyUp(key))
        {
            foreach (Thruster thruster in thrusters)
            {
                thruster.fireThruster = false;
                thruster.jet.Stop();
            }
        }
    }
}
