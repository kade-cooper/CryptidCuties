using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class AttackHandler : NetworkBehaviour
{
    [Networked(OnChanged = nameof(OnAttackChanged))]
    public bool isAttacking { get; set; }

    public GameObject attack1;

    float lastTimeAttacked = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void FixedUpdateNetwork()
    {
        if(GetInput(out NetworkInputData networkInputData))
        {
            Debug.Log(networkInputData.isAttacking);
            if (networkInputData.isAttacking)
                Attack();
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

    IEnumerator AttackEnable()
    {
        isAttacking = true;

        attack1.SetActive(true);

        yield return new WaitForSeconds(.01f);

        isAttacking = false;
    }

    static void OnAttackChanged(Changed<AttackHandler> changed)
    {
        Debug.Log($"{Time.time} OnAttackChanged value {changed.Behaviour.isAttacking}");

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
}
