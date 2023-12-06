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
            return new Vector3(-32, 19, 0);
        }
        else if(playerCnt == 2)
        {
            return new Vector3(-36, -19, 0);
        }
        else if (playerCnt == 3)
        {
            return new Vector3(31, -17, 0);
        }
        else if (playerCnt == 4)
        {
            return new Vector3(35, 10, 0);
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
