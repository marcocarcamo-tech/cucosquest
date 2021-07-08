using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingRaycast2D : MonoBehaviour
{
    private Animator animator;
    private Weapon weapon;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        weapon = GetComponentInChildren<Weapon>();
    }

    // Update is called once per frame
    void Start()
    {
        animator.SetBool("Idle", true);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            animator.SetTrigger("Shoot");
        }
    }

    void CanShoot()
    {
        if (weapon != null) {
            // Shoot!!
            StartCoroutine(weapon.ShootWithRaycast());
        }
    }

}
