using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FindPathConfig", menuName = "Erfan/Scriptable Objects/FindPathConfig")]
public class FindPathConfig : ScriptableObject
{
    public Butterfly butterflyPrefab;
    public FindPathLevel FindPathLevel;
    public string howToPlayText;

}