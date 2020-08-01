using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * @author Devam Sisodraker
 * @author Aiden Kerr
 * @author Vishal Desh
 * @author Pedro Machado
 */
public class RocketManeuver : MonoBehaviour {
    public bool applyThrust;
    public float rotationSpeed;
    public float thrustMultiplier;
    private Rigidbody _rb;

    private void Start() {
        _rb = GetComponent<Rigidbody>();
    }

    // Check for misc keypresses
    private void CheckMiscKeys() {
        // Start applying thrust
        if (Input.GetKey(KeyCode.W))
            ApplyRocketThrust(true);
        if (Input.GetKey(KeyCode.S))
            ApplyRocketThrust(false);
        if (Input.GetKey(KeyCode.UpArrow))
            thrustMultiplier += 0.01f;
        if (Input.GetKey(KeyCode.DownArrow))
            thrustMultiplier -= 0.01f;
        if (Input.GetKey(KeyCode.Escape))
            SceneManager.LoadScene("TitleScreen", LoadSceneMode.Single);
        Debug.Log(thrustMultiplier);
    }

    // Check for rotation keypresses
    private void CheckRotationKeys() {
        // Rotate left
        if (Input.GetKey(KeyCode.D)) transform.Rotate(rotationSpeed * new Vector3(0, 0, -1));

        // Rotate right
        if (Input.GetKey(KeyCode.A)) transform.Rotate(rotationSpeed * new Vector3(0, 0, 1));
    }

    // Apply thrust to the rocket's rigidbody
    private void ApplyRocketThrust(bool forward) {
        Vector2 force = transform.up * thrustMultiplier;
        _rb.AddForce(forward ? force : -force, ForceMode.Impulse);
    }

    // Run physics calculations and misc events
    private void Update() {
        CheckMiscKeys();
        CheckRotationKeys();
    }
}