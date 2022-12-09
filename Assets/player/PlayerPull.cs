using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerPull : MonoBehaviour
{

    [SerializeField]public float Distance = 5f;
    public LayerMask Box;
    public bool carry;

    GameObject kotak;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.right * transform.localScale.x, Distance, Box);

        if(hit.collider != null && hit.collider.gameObject.tag == "Box" &&Input.GetKeyDown(KeyCode.E)) 
        {
            kotak = hit.collider.gameObject;
            carry = true;

            kotak.GetComponent<FixedJoint2D> ().enabled = true;
            kotak.GetComponent<FixedJoint2D> ().connectedBody = this.GetComponent<Rigidbody2D> ();
        } else if (Input.GetKeyUp(KeyCode.E))
        {
            carry = false;
            kotak.GetComponent<FixedJoint2D> ().enabled = false;
        }
    }

    void OnDrawGizmos ()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x);
    }
}
