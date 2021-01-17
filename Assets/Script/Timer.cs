using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public Animator _animatork;
    public TMPro.TMP_Text timerText;

    public void SetTimer(float value)
    {
        StartCoroutine(downTimer(value));
    }
    IEnumerator downTimer(float value)
    {
        float fullPart = Mathf.Floor(value);
        if (fullPart != value)
        {
            if (value < 10)
            {
                timerText.SetText("0" + fullPart.ToString());
            }
            else
            {
                timerText.SetText(fullPart.ToString());
            }
            yield return new WaitForSeconds(value - fullPart);
        }

        while (value >= 0)
        {
            if (value < 10)
            {
                timerText.SetText("0" + value.ToString());
            }
            else
            {
                timerText.SetText(value.ToString());
            }
            if (value <= 5f)
            {
                //Animation !
                _animatork.SetTrigger("RedMove");
            }
            value -= 1;
            yield return new WaitForSeconds(1f);
        }
    }
}
