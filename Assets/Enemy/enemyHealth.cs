using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : MonoBehaviour
{
    public float startHealth;
    private float hp;
    public GameObject diePEffect;
    // Start is called before the first frame update
    void Start()
    {
        hp = startHealth;
    }

    public void TakeDamage(float damage){
        hp -= damage;
        if(hp <= 0f){
            Die();
        }
    }

    void Die(){
        if(diePEffect != null){
            FindObjectOfType<audioManager>().Play("enemyDead");
            Instantiate(diePEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
