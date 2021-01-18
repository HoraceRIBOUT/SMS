using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "ScriptableObjects/QuestSet", order = 1)]
public class QuestSet : ScriptableObject
{
    public string id = "Set Name";
    public List<QuestData> allCardToThatSet = new List<QuestData>();
    public List<QuestData> cardToRemove = new List<QuestData>();
}
