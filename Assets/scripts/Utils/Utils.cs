using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//kade's branch

public class Utils : MonoBehaviour
{
    public int playerCnt = 0;
    public Vector3 GetNewPlayerSpawnPoint()
    {
        playerCnt += 1;
        if (playerCnt == 1)
        {
            return new Vector3(-32, 18.92, 0);
        }
        else if(playerCnt == 2)
        {
            return new Vector3(-35.87, -19.45, 0);
        }
        else if (playerCnt == 3)
        {
            return new Vector3(31.66, -17.57, 0);
        }
        else if (playerCnt == 4)
        {
            return new Vector3(35.39, 9.63, 0);
        }
        else
        {
            return new Vector3(0, 0, 0);
        }
    }
    public static void SetRenderLayerInChildren(Transform transform, int layerNumber)
    {
        foreach(Transform trans in transform.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }
}
