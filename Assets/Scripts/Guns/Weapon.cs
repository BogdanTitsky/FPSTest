using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 30f;
    [SerializeField] private float bulletLifeTime = 3f;
    

   private void Update()
   {
       if (Input.GetKeyDown(KeyCode.Mouse0)) Fire();
   }

   private void Fire()
   {
       GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
   }
}
