using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraController : MonoBehaviour
{
    Camera camera;// Start is called before the first frame update
    
    void Start()
    {
        camera = gameObject.GetComponent<Camera> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (camera.fieldOfView > 45) {
            camera.fieldOfView -= Time.deltaTime * .25f;
        }
    }

}
