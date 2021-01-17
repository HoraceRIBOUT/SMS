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

    public void AddADescription(string message)
    {
        GameObject gO = Instantiate(prefabDiscussion, Vector3.zero, Quaternion.identity, discussionFolder.transform);
        float height = gO.GetComponent<Message>().SetText(message);
        MoveEveryPastMessageFrom(height);

        messageCreated.Add(gO.GetComponent<RectTransform>());
    }

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
        StopAllCoroutines();
        StartCoroutine(MoveEveryPastMessage(height, new List<RectTransform>(messageCreated)));
        
    }

    public float speed = 1f;
    public AnimationCurve messageCurve;

    public IEnumerator MoveEveryPastMessage(float height, List<RectTransform> messages)
    {
        messages.Reverse();
        foreach (RectTransform rectTr in messages)
        {
            StartCoroutine(MoveOneMessage(height, rectTr));
            yield return new WaitForSeconds(0.05f);
        }
    }

    public IEnumerator MoveOneMessage(float height, RectTransform message)
    {
        float lerp = 0;
        Vector2 startHeight = message.anchoredPosition;
        Vector2 endHeight = message.anchoredPosition + Vector2.up * height;
        while (lerp < 1)
        {
            lerp += Time.deltaTime * speed;
            float interpolatedLerp = messageCurve.Evaluate(lerp);
            Vector2 finalHeight = Vector2.LerpUnclamped(startHeight, endHeight, interpolatedLerp);

            message.anchoredPosition = finalHeight;

            yield return new WaitForSeconds(0.01f);
        }

        message.anchoredPosition = endHeight;
    }

}
