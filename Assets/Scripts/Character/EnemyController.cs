using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum EnemyStates{GUARD,PATROL,CHASE,DEAD}
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
   private EnemyStates enemyStates;
   private NavMeshAgent agent;

   [Header("Basic Settings")] 
   public float signtRadius;
   
   
   void Awake()
 { 
    agent = GetComponent<NavMeshAgent>();
  }

   private void Update()
   {
       SwitchStates();
   }

   void SwitchStates()
   {
       //如果发现player切换到CHASE
       if (FoundPlayer())
       {
           enemyStates =  EnemyStates.CHASE;
           Debug.Log("找到player");
       }
       switch (enemyStates)
       {
           case EnemyStates.GUARD:
               break;
           case EnemyStates.PATROL:
               break;
           case EnemyStates.CHASE:
               break;
           case EnemyStates.DEAD:
               break;
       }
   }

   bool FoundPlayer()
   {
       var colliders = Physics.OverlapSphere(transform.position, signtRadius);

       foreach (var target in colliders)
       {
           if (target.CompareTag("Player"))
           {
               return true;
           }
       }

       return false;
   } 
}

