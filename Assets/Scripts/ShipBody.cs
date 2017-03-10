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

    [SerializeField]
    KeyCode fireWeapons = KeyCode.Space;

    private Rigidbody2D rb;

    private Thruster[] thrusters;
    private Weapon[] weapons;

    private List<Thruster> thrusterForwards;
    private List<Thruster> thrusterBackwards;
    private List<Thruster> thrusterLeft;
    private List<Thruster> thrusterRight;
    private List<Thruster> thrusterRotateLeft;
    private List<Thruster> thrusterRotateRight;


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
        thrusterRotateLeft = new List<Thruster>();
        thrusterRotateRight = new List<Thruster>();

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
        FindRotateLeftThrusters();
        FindRotateRightThrusters();
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
        }
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

    private void FindRotateLeftThrusters()
    {
        foreach (Thruster thruster in thrusters)
        {
            if (thruster.transform.localRotation.eulerAngles.z == 270 && (thruster.transform.localPosition.x - centerOfMass.transform.localPosition.x) < 0)
            {
                thrusterRotateLeft.Add(thruster);
            }

            if (thruster.transform.localRotation.eulerAngles.z == 90 && (thruster.transform.localPosition.x - centerOfMass.transform.localPosition.x) > 0)
            {
                thrusterRotateLeft.Add(thruster);
            }
        }
    }

    private void FindRotateRightThrusters()
    {
        foreach (Thruster thruster in thrusters)
        {
            if (thruster.transform.localRotation.eulerAngles.z == 270 && (thruster.transform.localPosition.x - centerOfMass.transform.localPosition.x) > 0)
            {
                thrusterRotateRight.Add(thruster);
            }

            if (thruster.transform.localRotation.eulerAngles.z == 90 && (thruster.transform.localPosition.x - centerOfMass.transform.localPosition.x) < 0)
            {
                thrusterRotateRight.Add(thruster);
            }
        }
    }

    private void Update()
    {
        FireThrusters(forward, thrusterForwards);
        FireThrusters(backward, thrusterBackwards);
        FireThrusters(left, thrusterRight);
        FireThrusters(right, thrusterLeft);
        FireThrusters(rotationLeft, thrusterRotateLeft);
        FireThrusters(rotationRight, thrusterRotateRight);

        FireWeapons(fireWeapons, weapons);
    }

    private void FireThrusters(KeyCode key, List<Thruster> thrusters)
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

    private void FireWeapons(KeyCode key, Weapon[] weapons)
    {
        if (Input.GetKeyDown(key))
        {
            foreach (Weapon weapon in weapons)
            {
                weapon.Shoot();
            }
        }
    }
}
