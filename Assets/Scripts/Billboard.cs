using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    void LateUpdate()
    {
        // Makes the health bar always face the camera
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);  // Flip it around if necessary
    }
}
