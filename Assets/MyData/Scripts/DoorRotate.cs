using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRotate : MonoBehaviour
{
    public float _openCloseSpeed = 30f;
    private bool _isPressedButton;
    private float count;

    public void Initialization(bool isPressedButton)
    {
        _isPressedButton = isPressedButton;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isPressedButton && count <= 100)
        {
            transform.Rotate(Vector3.up * _openCloseSpeed * Time.fixedDeltaTime);
            //transform.rotation = Quaternion.Euler(0, 90, 0);
            count += _openCloseSpeed * Time.fixedDeltaTime;

        }
        if (!_isPressedButton && count > 0)
        {
            transform.Rotate(-Vector3.up * _openCloseSpeed * Time.fixedDeltaTime);
            //transform.rotation = Quaternion.Euler(0, 0, 0);
            count -= _openCloseSpeed * Time.fixedDeltaTime;
        }
    }
}
