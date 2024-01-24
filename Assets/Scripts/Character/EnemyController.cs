using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;


public enum EnemyStates{GUARD,PATROL,CHASE,DEAD}
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
   private EnemyStates enemyStates;
   private NavMeshAgent agent;
   private Animator anim;

   [Header("Basic Settings")] 
   public float signtRadius;
   public bool isGuard;
   
   private float speed;
   private GameObject attackTarget;

   public float lookAtTime;
   private float remainLookAtTime;
   
   [Header("Patrol State")] 
   public float patrolRange;

   private Vector3 wayPoint;
   private Vector3 guardPos;
   
   //bool配合动画
    bool isWalk;
    bool isChase; 
    bool isFollow;
  
   void Awake()
 { 
    agent = GetComponent<NavMeshAgent>();
    anim = GetComponent<Animator>();
    speed = agent.speed;
    guardPos = transform.position;
    remainLookAtTime = lookAtTime;
 }


   private void Start()
   {
       if (isGuard)
       {
           enemyStates = EnemyStates.GUARD;
       }
       else
       {
           enemyStates = EnemyStates.PATROL;
           GetNewWayPoint();
       }
   }

   private void Update()
   {
       SwitchStates();
       SwitchAnimation();
   }


   void SwitchAnimation()
   {
       anim.SetBool("Walk",isWalk);
       anim.SetBool("Chase",isChase);
       anim.SetBool("Follow",isFollow);
   }
   void SwitchStates()
   {
       //如果发现player切换到CHASE
       if (FoundPlayer())
       {
           enemyStates =  EnemyStates.CHASE;
           // Debug.Log("找到player");
       }
       switch (enemyStates)
       {
           case EnemyStates.GUARD:
               break;
           case EnemyStates.PATROL:
               isChase = false;
               agent.speed = speed * 0.5f;
               
               //判断是否到了随机巡逻点;
               if (Vector3.Distance(wayPoint,transform.position)<=agent.stoppingDistance)
               {
                   isWalk = false;
                   if (remainLookAtTime > 0)
                   {
                       remainLookAtTime -= Time.deltaTime;
                   }
                   else
                   {
                       GetNewWayPoint();
                   }
               }
               else
               {
                   isWalk = true;
                   agent.destination = wayPoint;
               }
               
               break;
           case EnemyStates.CHASE:
               //TODO:追Player
               
               //TODO：在攻击范围内则攻击
               //TODO：配合动画
               isWalk = false;
               isChase = true;
               
               
               agent.speed = speed;
               
               if (!FoundPlayer())
               {
                   isFollow = false;
                   if (remainLookAtTime>0)
                   { 
                    agent.destination = transform.position;
                   remainLookAtTime -= Time.deltaTime;
                   }
                   else if(isGuard)
                   {
                       enemyStates = EnemyStates.GUARD;
                   }
                   else
                   {
                       enemyStates = EnemyStates.PATROL;
                   }
                   
               }
               else
               {
                   isFollow = true;
                   agent.destination = attackTarget.transform.position;
               }
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
               attackTarget = target.gameObject;
               return true;
           }
       }
       attackTarget = null;
       return false;
   }

   void GetNewWayPoint()
   {
       remainLookAtTime = lookAtTime;
       
       float randomX = UnityEngine.Random.Range(-patrolRange, patrolRange);//这两句问一下，照着那个老师写的会报错
       float randomZ = UnityEngine.Random.Range(-patrolRange, patrolRange);

       Vector3 randomPoint = new Vector3(guardPos.x + randomX, transform.position.y,
           guardPos.z + randomZ);
       //FIXME:可能出现的问题
       NavMeshHit hit;
       wayPoint = NavMesh.SamplePosition(randomPoint, out hit, patrolRange, 1) ? hit.position : transform.position;
   }
   private void OnDrawGizmosSelected()
   {
       Gizmos.color = Color.blue;
       Gizmos.DrawWireSphere(transform.position,signtRadius);
   }
}

