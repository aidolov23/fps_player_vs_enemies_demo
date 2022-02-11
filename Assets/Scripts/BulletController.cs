using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Target target;

    [SerializeField]
    private float bulletForce = 5f;

    [SerializeField]
    private LayerMask hitLayers;

    [SerializeField]
    private GameObject bangParticles;

    void OnCollisionEnter(Collision collision)
    {

        ContactPoint contactPoint = collision.GetContact(0);
        
        if (hitLayers == (hitLayers | (1 << collision.gameObject.layer)))
        {
            Hit(contactPoint.point);
        }
        else if (target == Target.Enemy && collision.gameObject.CompareTag("Enemy"))
        {
            FileLogger.WriteString("Enemy Down");
            Destroy(collision.gameObject);
            Hit(contactPoint.point);
        } 
        else if (target == Target.Player && collision.gameObject.CompareTag("Player"))
        {
            Rigidbody targetRb = collision.gameObject.GetComponent<Rigidbody>();
            //Push in forward of bullet
            targetRb.AddForce(transform.forward * bulletForce, ForceMode.Impulse);
            Destroy(gameObject);
        } 
        else if(collision.gameObject.GetComponent<BulletController>() != null)
        {
            //Bullet Destroy other Bullet
            Hit(contactPoint.point);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        
    }

    void Hit(Vector3 position)
    {
        Instantiate(bangParticles, position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void SetTarget(Target t)
    {
        target = t;
        if (target == Target.Player)
        {
            //add to layer, which ignore collision with enemies
            gameObject.layer = LayerMask.NameToLayer("EnemyBullets");
        }
    }
}
