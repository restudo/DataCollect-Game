using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuDummyPatrol : MonoBehaviour
{
 [Header("Patrol Points")] 
    [SerializeField] private Transform leftedge;
    [SerializeField] private Transform rightedge;

    [Header("enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement Parameter")]
    [SerializeField] private float speed;
    private Vector3 initscale;
    private bool movingLeft;

    private void awake()
    {
        initscale = enemy.localScale;
    }

    private void Update() 
    {
        if(movingLeft)
        {
            if(enemy.position.x >= leftedge.position.x)
            {
                MoveInDirection(-1);
            }
            else 
            {
                DirectionChange();
            }
        }
        
        else 
        {
            if(enemy.position.x <= rightedge.position.x)
            {
                MoveInDirection(1);
            }
            else 
            {
                DirectionChange();
            }
        }
    }

    private void DirectionChange()
    {
        movingLeft = !movingLeft;
    }

    private void MoveInDirection(int _dir) 
    {
        //make enemy face dir
        enemy.localScale = new Vector3(Mathf.Abs(enemy.localScale.x) * _dir, enemy.localScale.y, enemy.localScale.z);
        //move in that dir
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _dir * speed, enemy.position.y, enemy.position.z);

    }
}
