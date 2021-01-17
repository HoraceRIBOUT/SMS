using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    public RectTransform rectTr;

    public List<GameObject> leftGO;
    public List<GameObject> rightGO;
    public List<GameObject> descGO;

    public TMPro.TMP_Text textGO;
    public Image imageGO;
    public RectTransform imageRect;

    public Image iconLeft;
    public Image iconRight;

    public float margin = 11;

    public void Init(Sprite icon, bool heros, float height)
    {
        rectTr.anchoredPosition = Vector3.zero;
        
        iconLeft.sprite = icon;
        iconRight.sprite = icon;

        foreach (GameObject gO in leftGO)
            gO.SetActive(heros);
        foreach (GameObject gO in rightGO)
            gO.SetActive(!heros);
        foreach (GameObject gO in descGO)
            gO.SetActive(false);

//        Debug.Log("Height = " + height + " : " + rectTr.sizeDelta.x);
        rectTr.sizeDelta = new Vector2(rectTr.sizeDelta.x, height);

        endHeight = rectTr.anchoredPosition;
    }

    public void Init(float height)
    {
        rectTr.anchoredPosition = Vector3.zero;
        
        foreach (GameObject gO in leftGO)
            gO.SetActive(false);
        foreach (GameObject gO in rightGO)
            gO.SetActive(false);
        foreach (GameObject gO in descGO)
            gO.SetActive(true);


//        Debug.Log("Height = " + height + " : " + rectTr.sizeDelta.x);
        rectTr.sizeDelta = new Vector2(rectTr.sizeDelta.x, height);

        endHeight = rectTr.anchoredPosition;
    }


    public float SetText(string message)
    {
        textGO.gameObject.SetActive(true);
        imageGO.gameObject.SetActive(false);

        textGO.SetText(message);
        textGO.ForceMeshUpdate();

        float height = textGO.renderedHeight + margin * 2;//textGO.textInfo.lineCount * (textGO.fontSize + textGO.lineSpacing + ) + margin * 2;
        Init(height);


        return height;
    }

    public float SetText(string message, Sprite icon, bool heros)
    {
        if(message == "")
        {
            Debug.LogError("Empty message here !");
            message = ".";
        }

        textGO.gameObject.SetActive(true);
        imageGO.gameObject.SetActive(false);

        textGO.SetText(message);
        textGO.ForceMeshUpdate();

        float height = textGO.renderedHeight + margin * 2;//textGO.textInfo.lineCount * (textGO.fontSize + textGO.lineSpacing + ) + margin * 2;
        Init(icon, heros, height);


        return height;
    }

    public float SetImage(Sprite imageIndex, Sprite icon, bool heros)
    {
        float height = imageRect.rect.width;
        Init(icon, heros, height);

        textGO.gameObject.SetActive(false);
        imageGO.gameObject.SetActive(true);

        imageGO.sprite = imageIndex;

        

        return rectTr.sizeDelta.y;

    }

    public void SetDescColor(Color color)
    {
        descGO[0].GetComponent<Image>().color = color;
    }

    public Vector2 endHeight;

    public void MoveUp(float height, float speed, AnimationCurve messageCurve)
    {
        if (kill)
            return;
        StopAllCoroutines();
        StartCoroutine(MoveOneMessage(height, rectTr, speed, messageCurve));
    }

    public IEnumerator MoveOneMessage(float height, RectTransform message, float speed, AnimationCurve messageCurve)
    {
        float lerp = 0;
        Vector2 startHeight = message.anchoredPosition;
        endHeight = endHeight + Vector2.up * height;
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

    bool kill = false;

    public void Destroy()
    {
        StopAllCoroutines();
        kill = true;
        Destroy(this.gameObject);
    }

}
