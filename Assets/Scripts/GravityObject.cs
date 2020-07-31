using System;
using UnityEngine;

/**
 * @author Devam Sisodraker
 * @author Aiden Kerr
 * @author Vishal Desh
 * @author Pedro Machado
 */
public class GravityObject : MonoBehaviour {
    public Vector2 initialVelocity = new Vector2(0, 0);

    private Rigidbody _rb = new Rigidbody();
    
    // Start is called before the first frame update
    private void Start() {
        _rb = gameObject.GetComponent<Rigidbody>();
        
        // Disable gravity
        _rb.useGravity = false;
        
        // Set the initial velocity
        _rb.velocity = initialVelocity;
        
        // Set the angular drag to 0
        _rb.angularDrag = 0f;
    }

    // FixedUpdate is called at a constant rate
    private void FixedUpdate() {
        GravityObject[] targets = (GravityObject[]) FindObjectsOfType(typeof (GravityObject));

        // F = m1 m2 G / r**2 r^
        
        // G
        float G = GravityConfig.gravitationalConstant;
        
        foreach (GravityObject target in targets) {
            Rigidbody myRb = gameObject.GetComponent<Rigidbody>();
            Rigidbody otherRb = target.gameObject.GetComponent<Rigidbody>();

            if (myRb != otherRb) {
                // m1
                float m1 = myRb.mass;

                // m2
                float m2 = otherRb.mass;

                // gameshow bitches
                float r = Vector3.Distance(
                    myRb.gameObject.transform.position,
                    otherRb.gameObject.transform.position
                );

                // Gets a vector that points from the player's position to the target's.
                Vector3 heading = otherRb.gameObject.gameObject.transform.position -
                                  myRb.gameObject.gameObject.transform.position;
                float distance = heading.magnitude;
                Vector3 direction = heading / distance; // This is now the normalized direction.

                Debug.Log(direction * ((m1 * m2 * GravityConfig.gravitationalConstant) / (distance * distance)));

                // Drum roll calculation for gravitational force
                otherRb.AddForce(
                    direction * (-1 * (m1 * m2 * GravityConfig.gravitationalConstant) / (distance * distance)),
                    ForceMode.Impulse
                );
            }

        }
    }
}