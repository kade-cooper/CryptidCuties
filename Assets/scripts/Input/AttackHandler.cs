using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class AttackHandler : NetworkBehaviour
{
    [Networked(OnChanged = nameof(OnAttackChanged))]
    public bool isAttacking { get; set; }
    [Networked(OnChanged = nameof(OnRChanged))]
    public bool isRomanceAttk { get; set; }

    public GameObject attack1;

    public GameObject romanceAttk;

    float lastTimeAttacked = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void FixedUpdateNetwork()
    {
        if(GetInput(out NetworkInputData networkInputData))
        {
            //Debug.Log(networkInputData.isAttacking);
            if (networkInputData.isAttacking)
                Attack(attack1, isAttacking);
            else if (networkInputData.isRomanceAttk)
                Attack(romanceAttk, isRomanceAttk);
        }
    }

    void Attack(GameObject attackType, bool type)
    {
        if (Time.time - lastTimeAttacked < 0.9f)
        {
            Debug.Log("WAIT");
            return;
        }

        StartCoroutine(AttackEnable(attackType, type));

        lastTimeAttacked = Time.time;
    }

    IEnumerator AttackEnable(GameObject attack1, bool type)
    {
        type = true;

        attack1.SetActive(true);

        yield return new WaitForSeconds(.01f);

        type = false;
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

    static void OnRChanged(Changed<AttackHandler> changed)
    {
        //Debug.Log($"{Time.time} OnAttackChanged value {changed.Behaviour.isAttacking}");

        bool isAttackingCurrent = changed.Behaviour.romanceAttk;

        changed.LoadOld();

        bool isAttackingOld = changed.Behaviour.romanceAttk;

        if (isAttackingCurrent && !isAttackingOld)
        {
            changed.Behaviour.OnRRemote();
        }
    }

    void OnAttackRemote()
    {
        if (!Object.HasInputAuthority)
        {
            attack1.SetActive(true);
        }
    }
    void OnRRemote()
    {
        if (!Object.HasInputAuthority)
        {
            romanceAttk.SetActive(true);
        }
    }
}
