using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using SimpleJSON;
using TMPro;
public class BackConnection : MonoBehaviour
{
    public string url = "localhost:3000/GameEndpoint";
    [SerializeField] private Button create;
    [SerializeField] TMPro.TMP_Text message;
    void Start()
    {
        create.onClick.AddListener(MakeTheCall);
    }
    void MakeTheCall()
    {
        StartCoroutine(GetMasterName(url));
    }
    IEnumerator GetMasterName(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();


        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
            message.text = "NO endpoint detected =(";
            message.gameObject.SetActive(true);
        }
        else
        {
            JSONNode root =  JSON.Parse (request.downloadHandler.text);
            Debug.Log($"Response text: {root["name"]}");
            message.text = "Hello " + root["name"] +" !";
            message.gameObject.SetActive(true);

        }
    }
}
