using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArrangingGameItems", menuName = "Erfan/Scriptable Objects/ArrangingGameItems")]
public class ArrangingGameItems : ScriptableObject
{
    public List<DragObject> items = new List<DragObject>();
}
