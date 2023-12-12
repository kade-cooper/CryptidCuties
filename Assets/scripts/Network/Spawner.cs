using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//kade's branch

public class Spawner : MonoBehaviour, INetworkRunnerCallbacks
{

    public Vector3 spawn;

    public NetworkPlayer PlayerPrefab;
    // public NetworkCSelect cSelectPrefab;
    public Utils util;

    CharacterInputHandler characterInputHandler;
    // Start is called before the first frame update
    void Start()
    {

    }


    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsServer)
        {
            Debug.Log("OnplayerJoined we are server. Player spawning as ghost.  All your base are belong to us");
            //runner.Spawn(cSelectPrefab,new Vector3(0,0,0), Quaternion.identity,player);
            spawn = util.GetNewPlayerSpawnPoint();
            runner.Spawn(PlayerPrefab, spawn, Quaternion.identity, player);
        }
        else Debug.Log("OnPlayerJoined");
    }



    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        if (characterInputHandler == null && NetworkPlayer.Local != null)
        {
            characterInputHandler = NetworkPlayer.Local.GetComponent<CharacterInputHandler>();
        }
        if (characterInputHandler != null)
        {
            input.Set(characterInputHandler.GetNetworkInput());
        }
    }

    public void OnConnectedToServer(NetworkRunner runner) {
        Debug.Log("OnConnectedToServer");
        
    }


    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { Debug.Log("OnShutdown");
        SceneManager.LoadScene("mainMenu");
    }
    public void OnDisconnectedFromServer(NetworkRunner runner) {
        Debug.Log("OnDisconnectedFromServer");
        SceneManager.LoadScene("mainMenu");
    }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { Debug.Log("OnConnectRequest"); }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { Debug.Log("OnConnectFailed");
        SceneManager.LoadScene("mainMenu");
    }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationtoken) {
        SceneManager.LoadScene("mainMenu");
    }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }

}
