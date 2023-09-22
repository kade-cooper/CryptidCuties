using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

//kade's branch

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemies;
    public int damage;
    float lastTimeAttacked = 0;
   // [Networked(OnChanged = nameof(OnAttackChanged))]

    // Start is called before the first frame update
    void Start()
    {
        if(timeBtwAttack <= 0){
            //then you can attack

            // if(Input.GetKeyDown("space")){
            //     Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
            //     for (int i = 0; i < enemiesToDamage.Length; i++){
            //         enemiesToDamage[i].GetComponent<NetworkCharacterControllerPrototypeCustom>().Health -= damage;
            //     }
            //     Debug.Log("Player attacked!");
            // }
        timeBtwAttack = startTimeBtwAttack;
    } else {
        timeBtwAttack -= Time.deltaTime;
    }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // public void FixedUpdateNetwork()
    // {
    //     if(GetInput(out NetworkInputData networkInputData))
    //     {
    //         if(networkInputData.isAttacking)
    //         Attack(networkInputData.attackPos);
    //     }
    // }

    void Attack(Vector2 attackPos)
    {
        if(Time.time - lastTimeAttacked < 0.15)
        return;

        lastTimeAttacked = Time.time;
    }

    // static void OnAttackChanged(Changed<PlayerAttack> changed)
    // {
    //     Debug.Log($"{Time.time} OnAttackChanged value {changed.Behaviour.isAttacking}");

    //     bool isAttackingCurrent = changed.Behaviour.isAttacking;

    //     changed.LoadOld();

    //     bool isAttackingOld = changed.Behaviour.isAttacking;

    //     if(isAttackingCurrent && !isAttackingOld)
    //     changed.Behaviour.OnAttackRemote();
    // }

    void OnAttackRemote()
    {

    }
    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
