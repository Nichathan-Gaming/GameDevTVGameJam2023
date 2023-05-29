using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public GameObject player;
    private Vector3 offset = new Vector3(0, 0, -5);
    private bool changeCamera = false;

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;

        if (Input.GetKeyDown(KeyCode.V) && (changeCamera == false))
        {
            Camera.main.orthographicSize -= 2;
            changeCamera = true;
        }
        else if (Input.GetKeyDown(KeyCode.V) && (changeCamera == true))
        {
            Camera.main.orthographicSize += 2;
            changeCamera = false;
        }
    }
}
