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
        AddADescription(message, Color.white);
    }
    public void AddADescription(string message, Color color)
    {
        PlaySound(false);

        GameObject gO = Instantiate(prefabDiscussion, Vector3.zero, Quaternion.identity, discussionFolder.transform);
        float height = gO.GetComponent<Message>().SetText(message);
        gO.GetComponent<Message>().SetDescColor(color);
        MoveEveryPastMessageFrom(height);

        messageCreated.Add(gO.GetComponent<Message>());

        CountLimit();
    }

    public void AddAMessage(string message, int icon, bool heros)
    {
        if (icon == -1)
        {
            AddADescription(message);
            return;
        }

        PlaySound(heros);

        GameObject gO = Instantiate(prefabDiscussion, Vector3.zero, Quaternion.identity, discussionFolder.transform);
        float height =  gO.GetComponent<Message>().SetText(message, iconList[icon], heros);
        MoveEveryPastMessageFrom(height);

        messageCreated.Add(gO.GetComponent<Message>());
        
        CountLimit();
    }


    public void AddAnImage(Sprite imageSprite, int icon, bool heros)
    {
        PlaySound(heros);

        if (icon == -1)
            icon = 0;

        GameObject gO = Instantiate(prefabDiscussion, Vector3.zero, Quaternion.identity, discussionFolder.transform);
        float height = gO.GetComponent<Message>().SetImage(imageSprite, iconList[icon], heros);
        MoveEveryPastMessageFrom(height);

        messageCreated.Add(gO.GetComponent<Message>());

        CountLimit();
    }
    
    public void CountLimit()
    {
        if (messageCreated.Count > 11)
        {
            messageCreated[0].Destroy();
            messageCreated.RemoveAt(0);
        }
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


    public void PlaySound(bool heros)
    {
        if (heros)
            GameManager.instance.audioManager.PlayMessageHeros();
        else
            GameManager.instance.audioManager.PlayMessageOther();

    }

}
