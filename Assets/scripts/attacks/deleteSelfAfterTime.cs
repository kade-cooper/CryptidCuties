using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleteSelfAfterTime : MonoBehaviour
{
    public float time = 3;

    //public GameObject attackObject;

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

        yield return new WaitForSeconds(time);

        Destroy(this.gameObject);

    }

}
