using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class data : MonoBehaviour
{
    private bool isFollowing;
    public float followSpeed;
    public Transform followTarget;

    // Update is called once per frame
    void Update()
    {
        if(isFollowing){
            transform.position = Vector3.Lerp(transform.position, followTarget.position, followSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            if(!isFollowing){
                PlayerController thePlayer = FindObjectOfType<PlayerController>();
                followTarget = thePlayer.dataFollowPoint;
                FindObjectOfType<audioManager>().Play("getData");
                isFollowing = true;
                thePlayer.followingData = this;
            }
        }
    }
}
