using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;
using TMPro;

public class CryptidScript : NetworkBehaviour
{
    public float maxHealth;

    [Networked]
    public float netHealth {get; set;}

    public float health;

    public bool canBeDamaged = true;
    public static CryptidScript Local { get; set; }

    public float redAttackPower = 10;
    public float blueAttackPower = 15;
    public float trapAttkPower = 150;
    public float lungeAttkPower = 300;
    public float trapTime = 2;
    public Slider healhBar;

    public sliderBar healthAbove;

    // public Transform arrow;


    //romance levels
    public float maxRomance = 1000;
    [Networked]
    public float romanceMEt2 { get; set; }
    [Networked]
    public CryptidScript player2 { get; set; }
    public CryptidScript player2temp;
    [Networked]
    public float romanceMEt3 { get; set; }
    [Networked]
    public float romanceMEt4 { get; set; }

    public Slider romanceBar;



    public Vector3 spawnpoint;
    public Transform wholePlayer;
    public GameObject wholePlayerForLayer;
    public NetworkPlayer player;

    public GameObject canvas;

    public CharacterInputHandler cih;
    public CharacterMovementHandler cmh;

    public NetworkObject blood;


    TextMeshProUGUI ScoreUI1;
    TextMeshProUGUI ScoreUI2;
    TextMeshProUGUI ScoreUI3;
    TextMeshProUGUI ScoreUI4;

    public int player0Score=0;
    public int player1Score=0;
    public int player2Score=0;
    public int player3Score=0;

    // Start is called before the first frame update
    void Start()
    {
        spawnpoint = player.spawnpoint;
        ScoreUI1 = GameObject.FindGameObjectWithTag("ScoreUI1").GetComponent<TextMeshProUGUI>();
        ScoreUI2 = GameObject.FindGameObjectWithTag("ScoreUI2").GetComponent<TextMeshProUGUI>();
        ScoreUI3 = GameObject.FindGameObjectWithTag("ScoreUI3").GetComponent<TextMeshProUGUI>();
        ScoreUI4 = GameObject.FindGameObjectWithTag("ScoreUI4").GetComponent<TextMeshProUGUI>();
        canBeDamaged = true;
    }

    private void OnEnable()
    {
        wholePlayerForLayer.layer = LayerMask.NameToLayer("player");
    }

    private void OnDisable()
    {
        wholePlayerForLayer.layer = LayerMask.NameToLayer("ghosts");
        player.selectedCharacter = 0;
        canvas.SetActive(true);
    }


    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            Local = this;
            netHealth = maxHealth;
            RPC_SendHealth();
            netHealth = health;
            

        }
        else
        {
            netHealth = maxHealth;
            RPC_SendHealth();
            netHealth = health;
            healthAbove.changeTo(health / maxHealth);
            RPC_SetPlayerTarget(this);
            //player2 = player2temp;

        }
    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_SendHealth()
    {
        Debug.Log("rpc health:" + netHealth);
        health = netHealth;
        
    }

    [Rpc(RpcSources.All, RpcTargets.InputAuthority)]
    public void RPC_SetPlayerTarget(CryptidScript otherPlayer)
    {
        player2 = otherPlayer;
    }



    // Update is called once per frame
    void Update()
    {
      //  Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

      //  arrow.up = (mousePos - (Vector3)transform.position).normalized;
    }

    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!canBeDamaged)
        {
            return;
        }
        if (collision.gameObject.CompareTag("redAttack1") && collision.GetComponent<attackScript>())
        {
            onHit(redAttackPower, collision.GetComponent<attackScript>().tagthing.ToString());
        }
        else if (collision.gameObject.CompareTag("blueAttack1") && collision.GetComponent<attackScript>())
        {
            onHit(blueAttackPower, collision.GetComponent<attackScript>().tagthing.ToString());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canBeDamaged)
        {
            return;
        }
        if (collision.gameObject.CompareTag("trap1Attk") && collision.GetComponent<attackScript>())
        {
            Debug.Log("entered trap1");
            onTrapHit(trapAttkPower, collision.GetComponent<attackScript>().tagthing.ToString(), collision);
        }
        if(collision.gameObject.CompareTag("lungeAttk") && collision.GetComponent<attackScript>())
        {
            onHit(lungeAttkPower, collision.GetComponent<attackScript>().tagthing.ToString());
        }
        Debug.Log("entered trigger");
    }

    void onHit(float attackPower, string attacker)
    {
        netHealth -= attackPower;
        healhBar.value = netHealth / maxHealth;
        healthAbove.onchange(attackPower, maxHealth, 1);
        if (netHealth <= 0)
        {
            onDie(attacker);

        }
        Debug.Log(netHealth);
    }


    void onTrapHit(float attackPower, string attacker, Collider2D trapHit)
    {
        Debug.Log("trapHit");
        netHealth -= attackPower;
        healhBar.value = netHealth / maxHealth;
        healthAbove.onchange(attackPower, maxHealth, 1);
        if (netHealth <= 0)
        {
            onDie(attacker);
        }
        cih.canInputNoVelocity = false;
        StartCoroutine(MoveCor(trapHit.gameObject));
        Debug.Log(netHealth);
    }

    IEnumerator MoveCor(GameObject trap)
    {
        yield return new WaitForSeconds(trapTime);
        cih.canInputNoVelocity = true;
        Destroy(trap);


    }


    public void onDie(string attacker)
    {
        cih.canInput = false;
        wholePlayer.GetComponent<CharacterController>().Move(new Vector3(0, 0, -100));
        wholePlayer.GetComponent<CharacterController>().Move(getRespawnVector());
        wholePlayer.GetComponent<CharacterController>().Move(new Vector3(0, 0, 100));
        NetworkRunner runner = GameObject.FindObjectOfType<NetworkRunner>();
        runner.Spawn(blood,this.transform.position);
        if (attacker == "player0")
        {
            player0Score += 1;
            ScoreUI1.text = player0Score.ToString();
        }
        else if (attacker == "player1")
        {
            player1Score += 1;
            ScoreUI2.text = player1Score.ToString();
        }
        else if (attacker == "player2")
        {
            player2Score += 1;
            ScoreUI3.text = player2Score.ToString();
        }
        else if (attacker == "player3")
        {
            player3Score += 1;
            ScoreUI4.text = player3Score.ToString();
        }
        //this.GetComponent<Collider2D>().gameObject.SetActive(true);
        cih.canInput = true;
        netHealth = maxHealth;
        healhBar.value = netHealth / maxHealth;
        healthAbove.onfull();
        this.gameObject.SetActive(false);

    }

    /*
    void onHitRomance(float attackPower)
    {
        romance -= attackPower;
        romanceBar.value = netHealth / maxHealth;
        if(romance == 1000)
        {
            //implement thing
        }
        Debug.Log(romance);
    }
    */

    Vector3 getRespawnVector()
    {
        return spawnpoint - wholePlayer.position + new Vector3(0,0,-100);
    }
    
}
