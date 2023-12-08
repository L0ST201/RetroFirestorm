using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
            Debug.LogError("Camera not found for Billboard script.");
    }

    void LateUpdate()
    {
        if(cam != null)
        {
            Vector3 lookPos = cam.transform.position - transform.position;
            lookPos.y = 0; // Optional: Keeps the billboard upright
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Euler(0f, rotation.eulerAngles.y + 180, 0f);
        }
    }
}
