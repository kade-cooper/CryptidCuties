using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.SceneManagement;

public class recieveName : NetworkBehaviour
{
    public string roomName;
    public bool isHost;
    public bool isClient;
    public bool isServer;
    public NetworkDebugStart thing;
    public bool isWaiting=false;
    // Start is called before the first frame update

    private void Start()
    {
        thing = GameObject.FindObjectOfType<NetworkDebugStart>();
    }
    void Update()
    {
        if(isHost == false && isClient == false && isServer == false)
        {
            if (!isWaiting)
                StartCoroutine(wait());
        }
        else
        {
            doThing();
        }
        if (GameObject.FindObjectOfType<NetworkPlayer>())
        {
            this.gameObject.SetActive(false);
        }
    }

    IEnumerator wait()
    {
        isWaiting = true;

        yield return new WaitForSeconds(15);
        SceneManager.LoadScene("mainMenu");
    }

    void doThing()
    {
        thing.DefaultRoomName = roomName;
        if (isHost)
        {
            thing.StartHost();
            //this.gameObject.SetActive(false);
            //isHost = false;
        }
        else if (isClient)
        {
            thing.StartClient();
            //this.gameObject.SetActive(false);
            //isClient = false;
        }
        else if (isServer)
        {
            thing.StartServer();
            //this.gameObject.SetActive(false);
            //isServer = false;
        }
    }

    // Update is called once per frame

    public void RecieveNameAndType(string name, string type)
    {
        roomName = name;
        if(type == "host")
        {
            isHost =true;
        }
        else if (type == "client")
        {
            isClient = true;
        }
        else if(type == "server")
        {
            isServer = true;
        }
    }
}
