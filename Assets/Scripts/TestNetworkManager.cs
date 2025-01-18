using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[System.Serializable]
public class ResponseData
{
    public string message;
    public long timestamp;
}

public class TestNetworkManager : MonoBehaviour
{

    [SerializeField]
    Button PutButton;
    [SerializeField]
    Button GetButton;
    private void Start()
    {
        PutButton.onClick.AddListener(OnClickPutButton);
        GetButton.onClick.AddListener(OnClickGetButton);

    }

    public void OnClickPutButton()
    {
        StartCoroutine(OnPutConnect());
    }

    public void OnClickGetButton()
    {
        StartCoroutine(OnGetConnect());
    }

    private IEnumerator OnPutConnect()
    {
        string url = "192.168.1.37:3000/unity";
        WWWForm form = new WWWForm();
        form.AddField("test", "testvalue");        

        UnityWebRequest request = UnityWebRequest.Post(url, form);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            Debug.Log("Response from Node.js: " + JsonUtility.FromJson<ResponseData>(request.downloadHandler.text).message);
        }
    }

    private IEnumerator OnGetConnect()
    {
        string url = "http://localhost:3000/send-to-unity";
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            Debug.Log("Response from Node.js: " + JsonUtility.FromJson<ResponseData>(request.downloadHandler.text).message);
        }
    }


}
