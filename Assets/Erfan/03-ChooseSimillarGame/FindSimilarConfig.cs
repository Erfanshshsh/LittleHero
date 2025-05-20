using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "FindSimilarConfig", menuName = "Erfan/Scriptable Objects/FindSimilarConfig")]
public class FindSimilarConfig : ScriptableObject
{
    public ChooseSimilarItem sampleItem;
    
    public List<ChooseSimilarItem> bottomWrongItems = new List<ChooseSimilarItem>();
    public List<ChooseSimilarItem> bottomCorrectItems = new List<ChooseSimilarItem>();
    
    public string howToPlayText;
    
    public List<ChooseSimilarItem> GetShuffledCombinedList()
    {
        var combined = bottomWrongItems.Concat(bottomCorrectItems).ToList();
        return combined.OrderBy(_ => Random.value).ToList();
    }
}