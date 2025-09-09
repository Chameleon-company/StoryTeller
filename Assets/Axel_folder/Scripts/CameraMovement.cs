using System;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // VARIABLES
    public float panSpeed = 4.0f;

    private Vector3 mouseOrigin;
    private Vector3 reset;
    private bool isPanning;

    void Start()
    {
        reset = Camera.main.transform.position;
    }
    void Update()
    {


        if (Input.GetMouseButtonDown(0))
        {
            //right click was pressed	
            mouseOrigin = Input.mousePosition;
            isPanning = true;
        }


        // cancel on button release
        if (!Input.GetMouseButton(0))
        {
            isPanning = false;
        }
        if (Input.GetMouseButton(1))
        {
            Camera.main.transform.position = reset;
        }

        //move camera on X & Y
        if (isPanning)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            // update x and y but not z
            Vector3 move = new Vector3(pos.x * - panSpeed, 0, pos.y * - panSpeed);

            Camera.main.transform.Translate(move, Space.World);
        }

        if (Input.mouseScrollDelta.y > 0f)
        {
            Debug.Log(Input.mouseScrollDelta);
            Vector3 move = new Vector3(0, -1, 0);

            Camera.main.transform.Translate(move, Space.World);
        }
        if (Input.mouseScrollDelta.y < 0f)
        {
            Debug.Log(Input.mouseScrollDelta);
            Vector3 move = new Vector3(0, +1, 0);
            Camera.main.transform.Translate(move, Space.World);
        }
    }
}

