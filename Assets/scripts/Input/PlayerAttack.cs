using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemies;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        if(timeBtwAttack <= 0){
            //then you can attack

            if(Input.GetKey(KeyCode.Space)){
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++){
                    enemiesToDamage[i].GetComponent<NetworkCharacterControllerPrototypeCustom>().Health -= damage;
                    Debug.Log("damage TAKEN !");
                }
            }
        timeBtwAttack = startTimeBtwAttack;
    } else {
        timeBtwAttack -= Time.deltaTime;
    }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
