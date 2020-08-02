using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomScrollMap : MonoBehaviour {
    private Camera _topCamera;

    // Start is called before the first frame update
    void Start() {
        _topCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.E))
            _topCamera.orthographicSize -= 0.75f;
        if (Input.GetKey(KeyCode.Q))
            _topCamera.orthographicSize += 0.75f;
    }
}
