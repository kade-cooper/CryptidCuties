using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;

public class CharacterSelectScreenInput : MonoBehaviour
{
    public NetworkPlayer PlayerPrefab;
    public void OnBlueSelected()
    {
        Debug.Log("Blue Selected");
        PlayerPrefab.SendMessage("CharacterBlueSelected");
    }

    public void OnRedSelected()
    {
        Debug.Log("Red Selected");
        PlayerPrefab.SendMessage("CharacterRedSelected");
    }
}
