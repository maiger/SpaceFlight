using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField]
    [Tooltip("Speed of projectile")]
    int moveSpeed = 20;

    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        Destroy(gameObject, 1f);
    }
}
