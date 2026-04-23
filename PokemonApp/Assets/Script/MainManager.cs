using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    public GameObject pokemonCardPrefab;
    public RawImage PokemonImage;
    public TextMeshProUGUI PokeNameText;
    public TextMeshProUGUI PokeIdText;

    public Transform content;

    void Start()
    {
        StartCoroutine(LoadPokemonList());
    }

    IEnumerator LoadPokemonList()
    {
        string pokemonGroupurl = "https://pokeapi.co/api/v2/pokemon?limit=151&offset=0";

        using (UnityWebRequest request = UnityWebRequest.Get(pokemonGroupurl))
        {

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {

                //Jsonをオブジェクトに変換して取得
                PokemonList list = JsonUtility.FromJson<PokemonList>(request.downloadHandler.text);
                Debug.Log("JSON: " + request.downloadHandler.text); // 3. デバッグ出力

                for (int i = 0; i < list.results.Length; i++)
                {


                    int id = i + 1;
                    string name = list.results[i].name;
                    string url = $"https://pokeapi.co/api/v2/pokemon/{id}";


                    Debug.Log("id番号" + id + "番のポケモンの名前は" + name + "です。 URL : " + url);

                    //プレハブの生成
                    //Instantiate（生成するオブジェクト, (場所、回転), 回転はそのままならQuaternion.identity);
                    GameObject card = Instantiate(pokemonCardPrefab, content);


                    //個別urlを叩いてDetailの情報を返してもらう
                    UnityWebRequest secondRequest = UnityWebRequest.Get(url);
                    yield return secondRequest.SendWebRequest();

                    if (secondRequest.result == UnityWebRequest.Result.Success)
                    {
                        // 個別ポケモンのJson情報ゲット
                        Debug.Log("JSON: " + secondRequest.downloadHandler.text);

                        //Jsonをオブジェクトに変換して取得
                        PokemonDetail detail = JsonUtility.FromJson<PokemonDetail>(secondRequest.downloadHandler.text);
                        string PokemonImageUrl = detail.sprites.front_default;


                        // PokemonImageUrlのログだす
                        Debug.Log("画像URLは：" + PokemonImageUrl);
                        Debug.Log("画像を表示しました");


                        // PokemonImageUrlにリクエストを出してPokemonImageUrlの中のimagetextureをゲット
                        UnityWebRequest imageRequest = UnityWebRequestTexture.GetTexture(PokemonImageUrl);
                        yield return imageRequest.SendWebRequest();

                        if (imageRequest.isNetworkError || imageRequest.isHttpError)
                        {
                            Debug.Log(imageRequest.error);
                        }
                        else
                        {
                            //取得した画像のテクスチャをRawImageのテクスチャに貼り付ける。
                            PokemonImage.texture = ((DownloadHandlerTexture)imageRequest.downloadHandler).texture;
                        }

                        //IDをint型からString型に変更してテキストに反映する。
                        int number = id;
                        string strnum = number.ToString();


                        //取得した名前をテキストに貼り付ける
                        if (PokeNameText != null && PokeIdText)
                        {
                            PokeNameText.text = name;
                            PokeIdText.text = strnum;
                        }




                    }
                    else
                    {
                        Debug.LogError("Error:" + secondRequest.error);
                    }



                }
            }
            else
            {
                Debug.LogError("Error:" + request.error);
            }
        }
    }
}



