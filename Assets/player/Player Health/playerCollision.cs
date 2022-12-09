using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class playerCollision : MonoBehaviour
{
    public GameObject deathVFXPrefab;
    private Animator anim;
    int enemyLayer;

    public void Awake(){
        anim = GetComponent<Animator>();
        enemyLayer = LayerMask.NameToLayer("enemy");
        Instantiate(deathVFXPrefab, transform.position, transform.rotation);
    }

    public void OnCollisionEnter2D(Collision2D collision){
        if(collision.transform.tag == "enemy"){
            TakingDamage();
        }
    
        if(collision.transform.tag == "void"){
            FindObjectOfType<audioManager>().Play("damaged");
            FindObjectOfType<audioManager>().Play("gameOver");
            Instantiate(deathVFXPrefab, transform.position, transform.rotation);
            gameObject.SetActive(false);
            GameManager.PlayerDied();
        }
    }

    public void OnTriggerEnter2D(Collider2D col){
        if(col.transform.tag == "laser"){
            TakingDamage();
        }
    }

    IEnumerator GetHurt(){
        Physics2D.IgnoreLayerCollision(6,8, true);
        //Shake.camShake();
        anim.SetTrigger("getHurt");
        yield return new WaitForSeconds(1);
        Physics2D.IgnoreLayerCollision(6,8, false);
    }

    public void TakingDamage(){
        playerHealthSystem.health--;
        FindObjectOfType<audioManager>().Play("damaged");
        if(playerHealthSystem.health <= 0){
            FindObjectOfType<audioManager>().Play("gameOver");
            Instantiate(deathVFXPrefab, transform.position, transform.rotation);
            gameObject.SetActive(false);
            GameManager.PlayerDied();
        }
        else{
            StartCoroutine(GetHurt());
        }
    }
}
