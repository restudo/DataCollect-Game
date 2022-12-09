using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{

    [Header("Gate")] 
    [SerializeField] private Transform Gate1;

    [SerializeField] private float range;
    
    
    public bool open;
    public float speed = 1;

    private Animator anim;
    private AudioSource sfx;
    
    void Start(){
        sfx = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        anim.SetBool("Up", true);
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        anim.SetBool("Up", false);
        open = true;
        opendoor();
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        anim.SetBool("Up", true);
        open = false;
        closedoor();
    }

    private void opendoor()
    {
        sfx.Play();
        Gate1.transform.position += transform.up * speed * range;
    }

    private void closedoor()
    {
        sfx.Play();
        Gate1.transform.position -= transform.up * speed * range;
    }
}
