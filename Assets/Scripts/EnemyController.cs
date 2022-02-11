using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private LayerMask bloksLayer;

    private CapsuleCollider col;

    [SerializeField]
    private float speed = 0.8f;

    private GameObject player;

    private Vector3 moveDirection;

    private WeaponController weapon;

    private float lastFireTime = 0f;
    [SerializeField]
    private float fireInterval = 5f;

    private float scaleSize;

    private bool inPlayerAim;

    private Outline ol;

    // Start is called before the first frame update
    void Start()
    {
        

        scaleSize = (transform.localScale.x + transform.localScale.z) / 2;

        player = GameObject.FindWithTag("Player");
        col = GetComponent<CapsuleCollider>();
        moveDirection = transform.right;

        weapon = GetComponent<WeaponController>();
        weapon.weaponTransform = transform;

        ol = GetComponent<Outline>();
    }

    // Update is called once per frame
    void Update()
    {

        if(speed > 0)
        {
            GroundAndWallCheck();
            Move();
        }
        
        Aim();

        lastFireTime += Time.deltaTime;
        if(lastFireTime > fireInterval)
        {
            Fire();
            lastFireTime = 0f;
        }

        
    }

    private void LateUpdate()
    {
        if (inPlayerAim)
        {
            ol.OutlineMode = Outline.Mode.OutlineAll;
            ol.OutlineColor = Color.yellow;
        }
        else
        {
            ol.OutlineMode = Outline.Mode.OutlineHidden;
            ol.OutlineColor = Color.red;
        }
    }

    private void GroundAndWallCheck()
    {
        Vector3 bootomPosition = new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z);
        Debug.DrawRay(bootomPosition, moveDirection - transform.up, Color.green);
        bool isOnGround = Physics.Raycast(bootomPosition, (moveDirection - transform.up), 0.2f, groundLayer, QueryTriggerInteraction.Ignore);
        bool isAtBlock = Physics.Raycast(bootomPosition, moveDirection, col.radius * scaleSize, bloksLayer, QueryTriggerInteraction.Ignore);
        Debug.DrawRay(bootomPosition, moveDirection, Color.yellow);
        if (!isOnGround || isAtBlock)
        {
            //change direction to the opposite
            moveDirection *= -1;
        }
        
    }

    void Move()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    void Aim()
    {
        transform.LookAt(player.transform);
    }

    void Fire()
    {
        weapon.Fire();
    }

    public void SetInPlayerAim(bool a)
    {
        inPlayerAim = a;
    }
}
