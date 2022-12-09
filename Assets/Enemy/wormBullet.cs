using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wormBullet : MonoBehaviour
{
    public float dieTime;
    public GameObject diePEffect;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDownTimer());
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;
        if(collisionGameObject.name != "enemy"){
            if(collisionGameObject.GetComponent<enemyHealth>() != null){
                collisionGameObject.GetComponent<playerCollision>().TakingDamage();
            }
            Die();
        }
    }

    IEnumerator CountDownTimer(){
        yield return new WaitForSeconds(dieTime);
        Die();
    }

    void Die(){
        if(diePEffect != null){
            Instantiate(diePEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
