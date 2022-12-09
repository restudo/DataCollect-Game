using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserDestroy : MonoBehaviour
{
    public float timeTilDestroy;

    private void Update()
    {
        Destroy(gameObject, timeTilDestroy);
    }
}
