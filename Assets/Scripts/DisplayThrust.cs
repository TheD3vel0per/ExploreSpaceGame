using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayThrust : MonoBehaviour {

    public GameObject rocket;
    private Text _thrust;
    private RocketManeuver _script;
    
    // Start is called before the first frame update
    void Start() {
        _thrust = GetComponent<Text>();
        _script = rocket.GetComponent<RocketManeuver>();
        Debug.Log(_script);
    }

    // Update is called once per frame
    void Update() {
        _thrust.text = "Thrust Multiplier: " + _script.thrustMultiplier.ToString("0.00");
    }
}
