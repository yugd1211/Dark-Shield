using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract void UseSkill1();
    public abstract void UseSkill2();

    //public abstract void UseSkill(Player player, Skill skill);
}
