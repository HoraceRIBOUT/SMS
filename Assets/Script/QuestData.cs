using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "Quest", menuName = "ScriptableObjects/QuestData", order = 1)]
public class QuestData : ScriptableObject
{
    //[HideInInspector]
    public string id = "Quest Name";
    public enum iconeIndex
    {
        none = -1,
        chevalier = 0,
        inconnu = 1,
        inconnu_force,
        gobelin,
        gobelin_musculeux,
        bot = 5,
        mamie,
        marave,
        mechant,
        pirate,
        princess = 10,
        princess_moustache,
        roi,
        squellette,
        squellette_vampire,
        choca = 15,
        horace,
        horace2,

    }


    [System.Serializable]
    public class Message
    {
        public iconeIndex iconeIndex = iconeIndex.inconnu;
        public bool isAPhoto = false;
        [HideIf("isAPhoto")]
        [TextArea(1,6)]
        public string message = "";

        [ShowIf("isAPhoto")]
        public Sprite image = null;

        public float timingForReadIt = 3f;
    }
    

    public List<Message> messages = new List<Message>();
    public float timerForQuest = 15f;
    public List<Message> messageIfMagic = new List<Message>();
    public List<QuestSet> setUnlockIfMagic = new List<QuestSet>();
    public List<Message> messageIfForce = new List<Message>();
    public List<QuestSet> setUnlockIfForce = new List<QuestSet>();


}
