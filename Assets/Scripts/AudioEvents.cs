using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShootsEvent : UnityEvent<Vector3>{}

public class DoorOpensEvent : UnityEvent<Vector3>{}