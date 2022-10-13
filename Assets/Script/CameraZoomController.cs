using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomController : MonoBehaviour
{

    private Camera cam;
    public float zoom;
    public float zoomSpeed = 3f;

    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        zoom = cam.orthographicSize;

        //get player of camera
        player = GameObject.FindWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        zoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        
        zoom = Mathf.Clamp(zoom, player.GetComponent<Controller>().minZoom, player.GetComponent<Controller>().maxZoom);

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, zoom, Time.deltaTime * zoomSpeed);


    }
}
