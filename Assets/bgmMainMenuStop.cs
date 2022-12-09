using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class bgmMainMenuStop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<audioManager>().Stop("mainMenu");
    }
}
