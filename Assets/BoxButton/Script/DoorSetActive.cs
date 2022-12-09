using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSetActive : MonoBehaviour, IDoor
{
    private bool IsOpen;
    public void opendoor() 
    {
        gameObject.SetActive(false);
    }

    public void closedoor() 
    {
        gameObject.SetActive(false);
    }

    public void toggle() 
    {
        IsOpen = !IsOpen;
        if (IsOpen) 
        {
            opendoor();
        } else 
        {
            closedoor();
        }

    }
}
