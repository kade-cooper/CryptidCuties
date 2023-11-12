using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class recieveName : NetworkBehaviour
{
    public string roomName;
    public bool isHost;
    public bool isClient;
    public bool isServer;
    public NetworkDebugStart thing;
    // Start is called before the first frame update

    private void Start()
    {
        thing = GameObject.FindObjectOfType<NetworkDebugStart>();
    }
    void Update()
    {
        if(isHost == false && isClient == false && isServer == false)
        {
            return;
        }
        else
        {
            doThing();
        }
    }

    void doThing()
    {
        thing.DefaultRoomName = roomName;
        if (isHost)
        {
            thing.StartHost();
            isHost = false;
        }
        else if (isClient)
        {
            thing.StartClient();
            isClient = false;
        }
        else if (isServer)
        {
            thing.StartServer();
            isServer = false;
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
