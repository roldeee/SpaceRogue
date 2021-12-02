using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShootsEvent : UnityEvent<Vector3>{}

public class DoorOpensEvent : UnityEvent<Vector3>{}

public class PlayerDamageEvent : UnityEvent<Vector3>{}
public class EnemyAttacksEvent : UnityEvent<Vector3>{}
public class EnemyDamagedEvent : UnityEvent<Vector3>{}