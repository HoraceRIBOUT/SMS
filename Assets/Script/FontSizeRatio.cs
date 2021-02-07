using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FontSizeRatio : MonoBehaviour
{
    [Tooltip("If not, is ratio from the whole screen")]
    public bool ratioFromTheRect;
    public float ratio = 0.33f;

    private Vector2 screenSize;

    public RectTransform myRectTransform;
    public Text myText;
    public TMP_Text myTMP_Text;


    // Start is called before the first frame update
    void Start()
    {
        if(myRectTransform == null)
        {
            myRectTransform = this.GetComponent<RectTransform>();
        }

        if (myText == null && myTMP_Text == null)
        {
            myText = this.GetComponent<Text>();
            myTMP_Text = this.GetComponent<TMP_Text>();

        }

        screenSize = new Vector2(Screen.width, Screen.height);
        Resize();
    }
    
    private void LateUpdate()
    {
        if(screenSize.x != Screen.width || screenSize.y != Screen.height)
        {
            Resize();
            screenSize = new Vector2(Screen.width, Screen.height);
        }
    }


    void Resize()
    {
        if (ratioFromTheRect)
        {
            if (myText != null)
            {
                myText.fontSize =(int) (ratio * myRectTransform.rect.size.y);
            }
            if (myTMP_Text != null)
            {
                myTMP_Text.fontSize = ratio * myRectTransform.rect.size.y;
            }
        }
        else
        {
           
        }
    }
}
