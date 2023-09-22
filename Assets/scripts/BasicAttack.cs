using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    float timeSinceAttack = 0;

    public GameObject attackObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine(AttackCor());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator AttackCor()
    {

        yield return new WaitForSeconds(.3f);

        attackObject.SetActive(false);

    }

}
