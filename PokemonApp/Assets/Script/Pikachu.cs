using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System;


public class Pikachu : MonoBehaviour
{

    public string pikaurl = "https://pokeapi.co/api/v2/pokemon/pikachu";
    public RawImage PikachuImage;
    public TextMeshProUGUI nameText;
    public string name;


    void Start()
    {
        StartCoroutine(LoadPikahchu());
    }





    IEnumerator LoadPikahchu()
    {

        UnityWebRequest req = UnityWebRequest.Get(pikaurl);
        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            // 1
            Debug.Log("JSON: " + req.downloadHandler.text);

            // 2
            //Jsonをオブジェクトに変換して取得
            PikachuInfo inf = JsonUtility.FromJson<PikachuInfo>(req.downloadHandler.text);
            int id = inf.id;
            string name = inf.name;
            string imageUrl = inf.sprites.front_default;


            // 3
            Debug.Log("僕の名前は" + name + "番号は" + id);
            Debug.Log(imageUrl);


            //取得した名前をテキストに貼り付ける
            if (nameText != null)
            {
                nameText.text = name;
            }




            // 4
            UnityWebRequest req2 = UnityWebRequestTexture.GetTexture(imageUrl);
            yield return req2.SendWebRequest();
            if (req2.isNetworkError || req2.isHttpError)
            {
                Debug.Log(req2.error);
            }
            else
            {
                //取得した画像のテクスチャをRawImageのテクスチャに貼り付ける。
                PikachuImage.texture = ((DownloadHandlerTexture)req2.downloadHandler).texture;
            }


        }
        else
        {
            Debug.LogError("Error:" + req.error);
        }

    }
}


