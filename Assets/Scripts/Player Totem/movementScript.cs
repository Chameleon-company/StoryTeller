using System;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UI;

public class movementScript : MonoBehaviour
{
    public float panSpeed = 20f;
    private Vector3 pos, reset, mouseOrigin;
    public float minX, maxX, minY, maxY, minZoom, maxZoom; //bounds min and max
    private float mouseX, mouseY; // X and Y deltas for mouse movement
    private bool moveUp, moveDown, moveLeft, moveRight, zoomIn, zoomOut, mouseEnabled, mouseMovement = false; // bools for UI button functions


    void Start()
    {
        // save inital camera position for reset
        reset = transform.position;
    }
    void Update()
    {
        pos = transform.position;

        //mouse drag movement controls
        if (mouseEnabled == true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                mouseMovement = true;
                mouseOrigin = Input.mousePosition;
            }
            if (!Input.GetMouseButton(1))
            {
                mouseMovement = false;
                pos = transform.position;
            }
            if (mouseMovement)
            {
                Vector3 delta = Input.mousePosition - mouseOrigin;
                mouseOrigin = Input.mousePosition;
                mouseX = delta.x;
                mouseY = delta.y;
            }

        }
        //wasd, UI, and mouse button movement controls
        if (Input.GetKey(KeyCode.W) || moveUp || mouseY > 0)
        {
            pos.z += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) || moveDown || mouseY < 0)
        {
            pos.z -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A) || moveLeft || mouseX < 0)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D) || moveRight || mouseX > 0)
        {
            pos.x += panSpeed * Time.deltaTime;
        }



        //scrollwheel and UI zoom controls
        float zoom = Input.GetAxis("Mouse ScrollWheel");
        if (zoomIn)
        {
            zoom = .01f;
        }
        if (zoomOut)
        {
            zoom = -.01f;
        }
        pos.y -= zoom * panSpeed * 100f * Time.deltaTime;

        //set bounds
        pos.x = Math.Clamp(pos.x, minX, maxX);
        pos.z = Math.Clamp(pos.z, minY, maxY);
        pos.y = Math.Clamp(pos.y, minZoom, maxZoom);

        transform.position = pos;

    }
    public void resetCamera()
    {
        transform.position = reset; 
    }
    public void enableMouseMovement()
    {
        if (!mouseEnabled)
        {
            mouseEnabled = true;
        }
        else
        {
            mouseEnabled = false;
        }
        Debug.Log("mouse enabled: " + mouseEnabled);
    }
    public void pointerDownUp()
    {
        moveUp = true;
    }
    public void pointerDownDown()
    {
        moveDown = true;
    }
    public void pointerDownLeft()
    {
        moveLeft = true;
    }
    public void pointerDownRight()
    {
        moveRight = true;
    }
    public void pointerDownZoomIn()
    {
        zoomIn = true;
    }
    public void pointerDownZoomOut()
    {
        zoomOut = true;
    }
    public void pointerUp()
    {
        moveUp = false;
        moveDown = false;
        moveLeft = false;
        moveRight = false;
        zoomIn = false;
        zoomOut = false;
    }
}
