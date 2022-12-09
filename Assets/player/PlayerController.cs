using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Transform dataFollowPoint;
    public data followingData;
    public GameObject levelWinUI;	

    public float speed;
    public float jumpFroce;
    private float moveInput;
    private Rigidbody2D rb;
    private float jumpTimeCounter;
    public float jumpTime;
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    
    private bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumptime;
    private bool isJumping;
    private bool facingRight = true;
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private bool isThoucingFront;
    public Transform frontCheck;
    private bool wallSliding;
    public float wallSlidingSpeed;

    //dash
    public float baseSpeed;
    public float dashForce;
    public float dashTime;
    private bool isDashing = false;

    private float nextDash = 1;

    // push

    [SerializeField]public float Distance = 1f;
    public LayerMask Box;
    public bool carry;
    GameObject kotak;

    [Header("Shoot")]
    public float shootSpeed;
    public float shootTimer;
    private bool isShooting;
    public Transform shootPos;
    public GameObject bullet;
    public float maxAmmo;
    private float currentAmmo;
    public TextMeshProUGUI ammoText;

    private Animator anim;

    void Start(){
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        speed = baseSpeed;
        isShooting = false;
    }

    void FixedUpdate(){
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if(facingRight == false && moveInput > 0 && !carry){
            Flip();
        }else if(facingRight == true && moveInput < 0 && !carry){
            Flip();
        }
    }

    void Update(){
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        if(isGrounded){
            coyoteTimeCounter = coyoteTime;
        }else{
            coyoteTimeCounter -= Time.deltaTime;
        }

        if(coyoteTimeCounter > 0f && Input.GetKeyDown(KeyCode.Space)){
            anim.SetTrigger("takeOff");
            FindObjectOfType<audioManager>().Play("jump");
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpFroce;
        }
        
        if(isGrounded == true){
            anim.SetBool("isJumping", false);
        } else{
            anim.SetBool("isJumping", true);
        }

        if(Input.GetKey(KeyCode.Space) && isJumping == true){
            if(jumpTimeCounter > 0){
                rb.velocity = Vector2.up * jumpFroce;
                jumpTimeCounter -= Time.deltaTime;
            } else {
                isJumping = false;
            }
        }

        if(Input.GetKeyUp(KeyCode.Space)){
            coyoteTimeCounter = 0f;
            isJumping = false;
        }

        isThoucingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);

        if(isThoucingFront == true && isGrounded == false && moveInput != 0){
            wallSliding = true;
        } else {
            anim.SetBool("isWallSliding", false);
            wallSliding = false;
            
        }

        if (wallSliding){
            anim.SetBool("isWallSliding", true);
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }

        if(Input.GetKeyDown(KeyCode.Space) && wallSliding == true){
            wallJumping = true;
        }
        if(wallJumping){
            FindObjectOfType<audioManager>().Play("jump");
            rb.velocity = new Vector2(moveInput * -xWallForce, yWallForce);
            Invoke("SetWallJumpingFalse", wallJumptime);
        }

        if(Input.GetKeyDown(KeyCode.K) && Time.time > nextDash){
            FindObjectOfType<audioManager>().Play("dash");
            anim.SetTrigger("dash");
            nextDash = Time.time + 1;
            if(!isDashing){
                StartCoroutine(Dash());
            }
        }

        if(moveInput == 0){
            anim.SetBool("isRunning", false);
        } else {
            anim.SetBool("isRunning", true);
        }

        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.right * transform.localScale.x, Distance, Box);

        if(hit.collider != null && hit.collider.gameObject.tag == "Box" && Input.GetKeyDown(KeyCode.I)) 
        {   
            kotak = hit.collider.gameObject;
            carry = true;
            kotak.GetComponent<FixedJoint2D> ().enabled = true;
            kotak.GetComponent<FixedJoint2D> ().connectedBody = this.GetComponent<Rigidbody2D> ();

        } else if (Input.GetKeyUp(KeyCode.I))
        {
            carry = false;
            kotak.GetComponent<FixedJoint2D> ().enabled = false;
        }

        //Shoot
        if(Input.GetKeyDown(KeyCode.J) && !isShooting && maxAmmo > 0){
            
            StartCoroutine(Shoot());
            
            maxAmmo--;
            ammoText.text = maxAmmo.ToString();
        }
        //endShoot

    }

    void SetWallJumpingFalse(){
        wallJumping = false;
    }

    //Shoot
    IEnumerator Shoot(){
        int direction(){
            if(transform.localScale.x < 0f){
                return -1;
            }else{
                return 1;
            }
        }
        isShooting = true;
        anim.SetTrigger("shoot");
        FindObjectOfType<audioManager>().Play("shoot");
        GameObject newBullet = Instantiate(bullet, shootPos.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * direction() * Time.fixedDeltaTime, 0f);
        newBullet.transform.localScale = new Vector2(newBullet.transform.localScale.x * direction(), newBullet.transform.localScale.y);
        yield return new WaitForSeconds(shootTimer);
        isShooting = false;
    }
    //endShoot

    IEnumerator Dash(){
        isDashing = true;
        speed *= dashForce;
        yield return new WaitForSeconds(dashTime);
        speed = baseSpeed;
        isDashing = false;
    }

    void Flip(){
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    public bool canAttack() {
        return moveInput == 0 && isGrounded && isThoucingFront;
    }

    public void WinLevel(){
		StartCoroutine(plaColDeactive());
        FindObjectOfType<audioManager>().PlayDelayed("levelDone");
		levelWinUI.SetActive(true);
	}

    IEnumerator plaColDeactive(){
        Physics2D.IgnoreLayerCollision(6,8, true);
        yield return new WaitForSeconds(2);
        Physics2D.IgnoreLayerCollision(6,8, false);
    }

    void OnDrawGizmos ()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x);
    }
}
