using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    [SerializeField]
    private GameObject spiderBullet;

    [SerializeField]
    private Transform bulletSpawnPos;

    [SerializeField]
    private float minShootWaitTime = 1f, maxShootWaitTime = 3f;

    private float waitTime;

    [SerializeField]
    private List<GameObject> bullets;

    private bool canShoot;
    private int bulletIndex;

    [SerializeField]
    private int initialBulletCount = 2;

    [SerializeField]
    private LayerMask collisionLayer;

    private void Start()
    {
        for (int i = 0; i < initialBulletCount; i++)
        {
            bullets.Add(Instantiate(spiderBullet));
            bullets[i].SetActive(false);
        }
    }

    private void Update()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, 10f, collisionLayer))
        {
            if (Time.time > waitTime)
            {
                waitTime = Time.time + Random.Range(minShootWaitTime, maxShootWaitTime);
                Shoot();
            }
        }

        Debug.DrawRay(transform.position, Vector3.down * 10f, Color.red);

    }

    void Shoot()
    {
        canShoot = true;
        bulletIndex = 0;

        while (canShoot)
        {
            if (!bullets[bulletIndex].activeInHierarchy)
            {
                bullets[bulletIndex].SetActive(true);
                bullets[bulletIndex].transform.position = bulletSpawnPos.position;

                canShoot = false;
            }
            else
            {
                bulletIndex++;
            }

            if (bulletIndex == bullets.Count)
            {

                bullets.Add(Instantiate(spiderBullet, bulletSpawnPos.position, Quaternion.identity));

                canShoot = false;
            }
        }
    }

} // class