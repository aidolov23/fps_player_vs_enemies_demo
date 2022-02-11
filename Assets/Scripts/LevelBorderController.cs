using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBorderController : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                GameController.instance.GameOver();
                break;

            case "Bullet":
                Destroy(other.gameObject);
                break;
        }
        
    }
}
