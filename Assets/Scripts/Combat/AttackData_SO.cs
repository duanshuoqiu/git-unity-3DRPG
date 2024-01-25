using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Attack",menuName = "Attack/Attack Data")]
public class AttackData_SO : ScriptableObject
{
   public float attackRange;
   public float skillRange;
   public float coolDown;
   public float minDamage;//注意这里视频少打了个A,后面写代码注意下我加了a
   public float maxDamage;

   public float criticalMultiplier;
   public float criticalChance;
}
