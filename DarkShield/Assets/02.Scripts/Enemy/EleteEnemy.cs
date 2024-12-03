using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public abstract class EleteEnemy : Enemy
{ 
    public float rangedAttackRange = 10f;

    public abstract override void Attack();
}
