using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Target target;

    [SerializeField]
    private float bulletSpeed = 10f;

    [HideInInspector]
    public Transform weaponTransform;

    public void Fire()
    {
        GameObject fB = Instantiate(bullet, weaponTransform.position + weaponTransform.forward * 0.2f, weaponTransform.rotation) as GameObject;
        fB.GetComponent<BulletController>().SetTarget(target);
        fB.GetComponent<Rigidbody>().velocity = weaponTransform.forward * bulletSpeed;
        
    }
}
