using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerShoot : MonoBehaviour
{
    public float shootSpeed, shootTimer;
    private bool isShooting;
    public Transform shootPos;
    public GameObject bullet;
    public float maxAmmo;
    private float currentAmmo;
    public TextMeshProUGUI ammoText;
    private Animator anim;
    
    void start()
    {
        anim = GetComponent<Animator>();
        isShooting = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J) && !isShooting && maxAmmo > 0){
            StartCoroutine(Shoot());
            maxAmmo--;
            ammoText.text = maxAmmo.ToString();
        }
    }

    IEnumerator Shoot(){
        int direction(){
            if(transform.localScale.x < 0f){
                return -1;
            }else{
                return 1;
            }
        }
        isShooting = true;
        FindObjectOfType<audioManager>().Play("shoot");
        GameObject newBullet = Instantiate(bullet, shootPos.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * direction() * Time.fixedDeltaTime, 0f);
        newBullet.transform.localScale = new Vector2(newBullet.transform.localScale.x * direction(), newBullet.transform.localScale.y);
        yield return new WaitForSeconds(shootTimer);
        anim.SetTrigger("shoot");
        isShooting = false;
    }
}