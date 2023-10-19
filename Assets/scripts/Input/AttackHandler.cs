using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class AttackHandler : NetworkBehaviour
{
    [Networked(OnChanged = nameof(OnAttackChanged))]
    public bool isAttacking { get; set; }

    [Networked(OnChanged = nameof(OnRomanceAttackChanged))]
    public bool isRomanceAttacking { get; set; }

    public GameObject attack1;

    public GameObject romanceAttk;

    float lastTimeAttacked = 0;


    NetworkCharacterControllerPrototypeCustom networkCharacterControllerPrototypeCustom;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void FixedUpdateNetwork()
    {
        bool thing = GetInput(out NetworkInputData networkInputData);
        Debug.Log("input is :"+thing);
        if (thing)
        {

            Debug.Log(networkInputData.isAttacking);
            if (networkInputData.isAttacking)
                Attack();
            else if (networkInputData.isRomanceAttk)
                    RAttack();
        }
    }

    void Attack()
    {
        if (Time.time - lastTimeAttacked < 0.9f)
        {
            Debug.Log("WAIT");
            return;
        }

        StartCoroutine(AttackEnable());

        lastTimeAttacked = Time.time;
    }

    void RAttack()
    {
        if (Time.time - lastTimeAttacked < 0.9f)
        {
            Debug.Log("WAIT");
            return;
        }

        StartCoroutine(RAttackEnable());

        lastTimeAttacked = Time.time;
    }

    IEnumerator AttackEnable()
    {
        isAttacking = true;

        attack1.SetActive(true);

        yield return new WaitForSeconds(.01f);

        isAttacking = false;
    }

    IEnumerator RAttackEnable()
    {
        isRomanceAttacking = true;

        romanceAttk.SetActive(true);

        yield return new WaitForSeconds(.01f);

        isRomanceAttacking = false;
    }

    static void OnAttackChanged(Changed<AttackHandler> changed)
    {
        //Debug.Log($"{Time.time} OnAttackChanged value {changed.Behaviour.isAttacking}");

        bool isAttackingCurrent = changed.Behaviour.isAttacking;

        changed.LoadOld();

        bool isAttackingOld = changed.Behaviour.isAttacking;

        if(isAttackingCurrent && !isAttackingOld)
        {
            changed.Behaviour.OnAttackRemote();
        }
    }

    void OnAttackRemote()
    {
        if (!Object.HasInputAuthority)
        {
            attack1.SetActive(true);
        }
    }


    static void OnRomanceAttackChanged(Changed<AttackHandler> changed)
    {
        //Debug.Log($"{Time.time} OnAttackChanged value {changed.Behaviour.isAttacking}");

        bool isAttackingCurrent = changed.Behaviour.isRomanceAttacking;

        changed.LoadOld();

        bool isAttackingOld = changed.Behaviour.isRomanceAttacking;

        if (isAttackingCurrent && !isAttackingOld)
        {
            changed.Behaviour.OnRAttackRemote();
        }
    }

    void OnRAttackRemote()
    {
        if (!Object.HasInputAuthority)
        {
            romanceAttk.SetActive(true);
        }
    }

}
