using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * @author Devam Sisodraker
 * @author Aiden Kerr
 * @author Vishal Desh
 * @author Pedro Machado
 */
public class RocketManeuver : MonoBehaviour
{
  public float thrustMultiplier;
     public float rotationSpeed;
     private bool applyThrust = false;
 
     void Start () { transform.forward = transform.up; }
 
     // Check for misc keypresses
     void CheckMiscKeys ()
     {
         // Start applying thrust
         if (Input.GetKeyDown(KeyCode.W))
         {
             applyThrust = true;
         }
 
         // Stop applying thrust
         if (Input.GetKeyUp(KeyCode.W))
         {
             applyThrust = false;
         }
     }
 
     // Check for rotation keypresses
     void CheckRotationKeys ()
     {
         // Rotate forward
         
         /*if (Input.GetKey (KeyCode.W))
         {
             transform.Rotate (rotationSpeed * new Vector3 (1, 0, 0));
         }
 
         // Rotate backwards
         if (Input.GetKey (KeyCode.S))
         {
             transform.Rotate (rotationSpeed * new Vector3 (-1, 0, 0));
         }*/
 
         // Rotate left
         if (Input.GetKey (KeyCode.A))
         {
             transform.Rotate (rotationSpeed * new Vector3 (0, -1, 0));
         }
 
         // Rotate right
         if (Input.GetKey (KeyCode.D))
         {
             transform.Rotate (rotationSpeed * new Vector3 (0, 1, 0));
         }
     }
 
     // Apply thrust to the rocket's rigidbody
     void ApplyRocketThrust ()
     {
         Vector2 force = transform.forward * thrustMultiplier;
         if (applyThrust)
         {
             GetComponent<Rigidbody>().AddForce(force);
         }
         else
         {
             GetComponent<Rigidbody>().AddForce(Vector3.zero);
             GetComponent<Rigidbody>().velocity = Vector3.zero;
         }
         
     }
 
     // Run physics calculations and misc events
     void Update ()
     {
         CheckMiscKeys ();
         CheckRotationKeys ();
         ApplyRocketThrust ();
     } 
}
