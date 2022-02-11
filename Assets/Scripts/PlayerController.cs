using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 2.0f;

    [SerializeField]
    private Transform cameraTransform;

    [SerializeField]
    private float mouseSensitivity = 100f;

    private float yRotation;

    private Rigidbody m_Rigidbody;

    private WeaponController weapon;

    
    private float aimRange = 100f;
    private EnemyController aimedEnemy;
    [SerializeField]
    LayerMask rayAimLayers;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        weapon = GetComponent<WeaponController>();
        weapon.weaponTransform = cameraTransform;
    }

    // Update is called once per frame
    void Update()
    {
        if(aimedEnemy != null)
        {
            //previous aimedEnemy
            aimedEnemy.SetInPlayerAim(false);
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -45f, 45f);
        cameraTransform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        if (Input.GetButtonDown("Fire1"))
        {
            FileLogger.WriteString("Fire");
            weapon.Fire();
        }

        RaycastHit hit;

        if (Physics.Raycast(cameraTransform.position + cameraTransform.forward * 0.5f, cameraTransform.forward, out hit, aimRange, rayAimLayers))
        {
            if (hit.collider.tag == "Enemy")
            {
                aimedEnemy = hit.collider.GetComponent<EnemyController>();
                aimedEnemy.SetInPlayerAim(true);
            }
            else
            {
                aimedEnemy = null;
            }
        }
    }

    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = (transform.forward * z + transform.right * x) * speed;
        //fix diagonal movement
        move = Vector3.ClampMagnitude(move, speed);

        m_Rigidbody.MovePosition(transform.position + move * Time.fixedDeltaTime);    
    }
}
