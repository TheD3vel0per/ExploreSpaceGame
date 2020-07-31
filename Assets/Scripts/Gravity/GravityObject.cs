using UnityEngine;

/**
 * @author Devam Sisodraker
 * @author Aiden Kerr
 * @author Vishal Desh
 * @author Pedro Machado
 */
public class GravityObject : MonoBehaviour {
    private Rigidbody _rb = new Rigidbody();
    public Vector3 acceleration;
    public float mass = 1f;

    public Vector3 velocity;

    // Start is called before the first frame update
    private void Start() {
        _rb = gameObject.GetComponent<Rigidbody>();

        // Disable gravity
        _rb.useGravity = false;

        // Set the angular drag to 0
        _rb.angularDrag = 0f;
    }

    public Vector3 UpdateVelocity(Vector3 acceleration, float timeStep) {
        velocity = velocity + acceleration * timeStep;
        return velocity;
    }

    public Vector3 UpdatePosition(Vector3 velocity, float timeStep) {
        transform.position = transform.position + velocity * timeStep;
        return transform.position;
    }
}