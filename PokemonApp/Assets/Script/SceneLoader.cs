//画面遷移の詳細設定
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public void SceneChangeMain()
    {
        SceneManager.LoadScene("Main");
    }
    public void SceneChangeDetail()
    {
        SceneManager.LoadScene("Detail");
    }




}