using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishZoneController : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EnemyController[] enemies = (EnemyController[])GameObject.FindObjectsOfType(typeof(EnemyController));
            if(enemies.Length == 0)
            {
                GameController.instance.WinGame();
            }
        }

    }
}
