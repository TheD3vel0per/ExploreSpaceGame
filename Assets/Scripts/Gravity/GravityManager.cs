using UnityEngine;

public class GravityManager : MonoBehaviour {
    private void FixedUpdate() {
        NextGravityTick(Time.deltaTime * GravityConfig.timeStep);
    }

    public void NextGravityTick(float timeStep) {
        var targets = (GravityObject[]) FindObjectsOfType(typeof(GravityObject));

        var G = GravityConfig.gravitationalConstant;

        foreach (var targetA in targets) {
            Vector3 acceleration = Vector3.zero;
            foreach (var targetB in targets) {
                // Getting the RigidBody
                var rbA = targetA.gameObject.GetComponent<Rigidbody>();
                var rbB = targetB.gameObject.GetComponent<Rigidbody>();

                // Not the same rigidBody 0/0 = NaN !!!
                if (rbA != rbB) {
                    // Mass of A
                    var massA = rbA.mass;

                    // Mass of B
                    var massB = rbB.mass;

                    // Calculating Distance between Centre of Gravity as a Scalar
                    var r = Vector3.Distance(
                        rbA.gameObject.transform.position,
                        rbB.gameObject.transform.position
                    );

                    // Gets a vector that points from the player's position to the target's.
                    var heading = rbB.gameObject.gameObject.transform.position -
                                  rbA.gameObject.gameObject.transform.position;
                    var distance = heading.magnitude;
                    var direction = heading / distance; // This is now the normalized direction.

                    // Drum roll calculation for gravitational force
                    var force = direction * (-1 * (massA * massB * GravityConfig.gravitationalConstant) /
                                             (distance * distance));


                    Vector3 forceDir = (targetB.gameObject.transform.position - targetA.gameObject.transform.position).normalized;
                    float sqrDst = (targetB.gameObject.transform.position - targetA.gameObject.transform.position).sqrMagnitude;
                    acceleration += forceDir * GravityConfig.gravitationalConstant * targetB.mass / sqrDst;
                }
            }
            // Update the position
            targetA.UpdatePosition(
                targetA.UpdateVelocity(acceleration, timeStep),
                timeStep
            );
        }
    }
}