using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Discussion : MonoBehaviour
{
    public GameObject prefabDiscussion;
    public GameObject discussionFolder;

    public List<Sprite> iconList = new List<Sprite>();
    public List<Sprite> photoList = new List<Sprite>();

    [Header("All Creation")]
    public List<RectTransform> messageCreated = new List<RectTransform>();


    public void AddAMessage(string message, int icon, bool heros)
    {
        GameObject gO = Instantiate(prefabDiscussion, Vector3.zero, Quaternion.identity, discussionFolder.transform);
        float height =  gO.GetComponent<Message>().SetText(message, iconList[icon], heros);
        MoveEveryPastMessageFrom(height);

        messageCreated.Add(gO.GetComponent<RectTransform>());
    }


    public void AddAnImage(int imageIndex, int icon, bool heros)
    {
        GameObject gO = Instantiate(prefabDiscussion, Vector3.zero, Quaternion.identity, discussionFolder.transform);
        float height = gO.GetComponent<Message>().SetImage(photoList[imageIndex], iconList[icon], heros);
        MoveEveryPastMessageFrom(height);

        messageCreated.Add(gO.GetComponent<RectTransform>());
    }

    public void MoveEveryPastMessageFrom(float height)
    {
        foreach(RectTransform rectTr in messageCreated)
        {
            rectTr.anchoredPosition += Vector2.up * height;
        }
    }

}
