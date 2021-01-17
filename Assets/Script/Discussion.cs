using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Discussion : MonoBehaviour
{
    public GameObject prefabDiscussion;
    public GameObject discussionFolder;

    public List<Sprite> iconList = new List<Sprite>();
    //public List<Sprite> photoList = new List<Sprite>();

    [Header("All Creation")]
    public List<Message> messageCreated = new List<Message>();

    public void AddADescription(string message)
    {
        GameObject gO = Instantiate(prefabDiscussion, Vector3.zero, Quaternion.identity, discussionFolder.transform);
        float height = gO.GetComponent<Message>().SetText(message);
        MoveEveryPastMessageFrom(height);

        messageCreated.Add(gO.GetComponent<Message>());
    }

    public void AddAMessage(string message, int icon, bool heros)
    {
        if (icon == -1)
            AddADescription(message);

        GameObject gO = Instantiate(prefabDiscussion, Vector3.zero, Quaternion.identity, discussionFolder.transform);
        float height =  gO.GetComponent<Message>().SetText(message, iconList[icon], heros);
        MoveEveryPastMessageFrom(height);

        messageCreated.Add(gO.GetComponent<Message>());
    }


    public void AddAnImage(Sprite imageSprite, int icon, bool heros)
    {
        GameObject gO = Instantiate(prefabDiscussion, Vector3.zero, Quaternion.identity, discussionFolder.transform);
        float height = gO.GetComponent<Message>().SetImage(imageSprite, iconList[icon], heros);
        MoveEveryPastMessageFrom(height);

        messageCreated.Add(gO.GetComponent<Message>());
    }

    public void MoveEveryPastMessageFrom(float height)
    {
        StartCoroutine(MoveEveryPastMessage(height, new List<Message>(messageCreated)));
    }

    public float speed = 1f;
    public AnimationCurve messageCurve;

    public IEnumerator MoveEveryPastMessage(float height, List<Message> messages)
    {
        messages.Reverse();
        foreach (Message mes in messages)
        {
            mes.MoveUp(height, speed, messageCurve);
            yield return new WaitForSeconds(0.05f);
        }
    }


}
