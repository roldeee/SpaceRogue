using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyAttackEmitter : MonoBehaviour
{
    // Start is called before the first frame update
    void ExecuteAttack() {
        EventManager.TriggerEvent<EnemyAttacksEvent, Vector3>(transform.position);
    }
}
