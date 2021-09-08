using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenClose : MonoBehaviour
{
    public float _openCloseSpeed = 1f;
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
            transform.Translate(Vector3.down * _openCloseSpeed * Time.fixedDeltaTime);
            count += _openCloseSpeed * Time.fixedDeltaTime;
            
        }   
        if(!_isPressedButton && count > 0)
        {
            transform.Translate(Vector3.up * _openCloseSpeed * Time.fixedDeltaTime);
            count -= _openCloseSpeed * Time.fixedDeltaTime;
        }
    }
}
