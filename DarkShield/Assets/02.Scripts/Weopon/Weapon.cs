using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float skill1damage;
    public float skill2damage;
    public float skill1Interval;
    public float skill2Interval;

    public abstract void UseSkill1();
    public abstract void UseSkill2();
}
