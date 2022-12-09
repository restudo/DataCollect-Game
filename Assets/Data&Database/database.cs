using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class database : MonoBehaviour
{
    public PlayerController Win;
    private PlayerController thePlayer;
    public SpriteRenderer theSR;
    public Sprite databaseOpenSprite;
    public bool databaseOpen, waitingToOpen;

    public GameObject collectEffect;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(thePlayer.followingData.transform.position, transform.position) < 0.1f){
            waitingToOpen = false;
            databaseOpen = true;
            theSR.sprite = databaseOpenSprite;
            thePlayer.followingData.gameObject.SetActive(false);
            thePlayer.followingData = null;
            FindObjectOfType<audioManager>().Play("dropData");
            collectEffect.SetActive(true);
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            if(thePlayer.followingData != null){
                thePlayer.followingData.followTarget = transform;
                waitingToOpen = true;
                Win.WinLevel();
            }
        }
    }
}
