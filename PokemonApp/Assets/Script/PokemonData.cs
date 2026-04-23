//データクラス設定
//APIのJsonの形に合わせてかく
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine;
[System.Serializable]
public class PokemonList
{
    public PokemonEntry[] results;
}

[System.Serializable]
public class PokemonEntry
{
    public string name;
    public string url;

}



[System.Serializable]
public class PokemonDetail
{
    public string types;
    public string moves;
    public string status;
    public Sprites sprites;


}
[System.Serializable]
public class PikachuInfo
{
    public string name;
    public Sprites sprites;
    public int id;

}
[System.Serializable]
public class Sprites
{
    public string front_default;

}














