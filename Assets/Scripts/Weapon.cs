using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject shooter;

    public GameObject explosionEffect;
    public LineRenderer lineRenderer;

    private Transform firePoint;
    

    void Awake()
    {
        firePoint = transform.Find("FirePoint");
    }

    void Start()
    {
        /*Invoke("Shoot", 1f);
        Invoke("Shoot", 2f);
        Invoke("Shoot", 3f);*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        if (bulletPrefab != null && firePoint != null && shooter != null) {
            GameObject myBullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity) as GameObject;

            Bullet bulletComponent = myBullet.GetComponent<Bullet>();

            if (shooter.transform.localScale.x < 0f) {
                // Left
                bulletComponent.direction = Vector2.left; // new vector (-1f, 0f)
            } else {
                //Right
                bulletComponent.direction = Vector2.right; // new vector (1f, 0f)
            }
        }
    }

    public IEnumerator ShootWithRaycast()
    {
        if (explosionEffect != null && lineRenderer != null) {
            RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right);

            if (hitInfo)
            {
                /* Example code
                 * if (hitInfo.collider.tag == "Player") {
                 * Transform player = hitInfo.transform;
                 * player.GetComponent<PlayerHealth>().ApplyDamage (5);
                 * }
                 * 
                 * Instantiante explosion on hit point
                 */
                Instantiate(explosionEffect, hitInfo.point, Quaternion.identity);
                //Set line renderer
                lineRenderer.SetPosition(0, firePoint.position);
                lineRenderer.SetPosition(1, hitInfo.point);
            } else {
                lineRenderer.SetPosition(0, firePoint.position);
                lineRenderer.SetPosition(1, hitInfo.point + Vector2.right * 100);

            }

            lineRenderer.enabled = true;

            yield return null;

            lineRenderer.enabled = false;
        }
              
            
        
    }
}
