using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class infoSend : MonoBehaviour
{

    public recieveName recievename;
    public string code;
    public TMP_Text text;
    public bool host;
    public bool client;
    public bool server;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    // Update is called once per frame

    private void Update()
    {
        recievename = GameObject.FindObjectOfType<recieveName>();
        if (recievename == null)
        {
            return;
        }
        recievename.isHost = host;
        recievename.isClient = client;
        recievename.isServer = server;
        recievename.roomName = code;
    }

    public void loadHost()
    {
        code = text.text;
        host = true;
        SceneManager.LoadScene("host");
    }

    public void loadClient()
    {
        code = text.text;
        client = true;
        SceneManager.LoadScene("host");
    }

    public void loadServer()
    {
        code = text.text;
        server = true;
        SceneManager.LoadScene("host");
    }
}
