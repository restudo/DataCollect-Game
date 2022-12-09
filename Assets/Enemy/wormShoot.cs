using System.Collections;
using UnityEngine;

public class wormShoot : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Ranged Attack")]
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject bullet;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    public float shootSpeed;
    private bool canShoot;
    
    //References
    //private Animator anim;
    private Patrol enemyPatrol;
    public GameObject player;
    

    private void Awake()
    {
        canShoot = true;
        //anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<Patrol>();
    }

    private void Update()
    {
        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y),
            0, Vector2.left, 0, playerLayer);
            if(canShoot)
                StartCoroutine(Shoot());


        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector2(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y));
    }

    IEnumerator Shoot(){
        canShoot = false;
        GameObject newBullet = Instantiate(bullet, firepoint.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * -transform.localScale.x * Time.fixedDeltaTime, 0f);
        yield return new WaitForSeconds(attackCooldown);
        canShoot = true;
    }
}