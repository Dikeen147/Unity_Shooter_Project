using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRay : MonoBehaviour
{
    private Color rayColor = Color.yellow;
    public Transform Pointer;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward * 5f);
        Debug.DrawRay(transform.position, transform.forward * 5f, rayColor);

        if (Physics.Raycast(ray, out RaycastHit hit, 5f))
        {
            Debug.Log(hit.collider.name);
            Pointer.position = hit.point;
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.yellow;
        }

    }
}
