using UnityEngine;

public class Projectile : MonoBehaviour
{
  [SerializeField] private float speed;
  private float direction;
  private bool hit;

  private Animator anim;
  private BoxCollider2D boxCollider;

  private void awake() {
      anim = GetComponent<Animator>();
      boxCollider = GetComponent<BoxCollider2D>();
  }

  private void Update()
  {
      if (hit) return;
      float MoveSPD = speed * Time.deltaTime * direction;
      transform.Translate(MoveSPD, 0, 0);
  }

  private void ontrigger(Collider2D collision)
  {
      hit = true;
      boxCollider.enabled = false;
      anim.SetTrigger("explode");
  }

  public void SetDirection(float _direction) {
      direction = _direction;
      gameObject.SetActive(true);
      hit = false;
      boxCollider.enabled = true;

      float localScalex = transform.localScale.x;
      if (Mathf.Sign(localScalex) != _direction) {
          localScalex = -localScalex;
      }
      transform.localScale = new Vector3(localScalex, transform.localScale.y, transform.localScale.z);
  }

  private void deactivated() {
      gameObject.SetActive(false);
  }
}
