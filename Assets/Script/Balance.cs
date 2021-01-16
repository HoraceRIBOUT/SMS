using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balance : MonoBehaviour
{
    public Rigidbody2D _rgbd;
    [Range(0,1)]
    public float rotSpeed = 0.1f;

    [Header("Visual")]
    public SpriteRenderer arrow;
    public Gradient gradient;

    public SpriteRenderer allPart;
    public float colorSpeed = 3f;

    // Update is called once per frame
    void Update()
    {
        float zAngle = this.transform.rotation.eulerAngles.z;
        if (zAngle > 180)
            zAngle -= 360;
        //if (zAngle != 0)
        {
            _rgbd.AddTorque(-zAngle * rotSpeed/* * Time.deltaTime*/, ForceMode2D.Impulse);
        }

        float lerp = Mathf.InverseLerp(-25, 25, -zAngle);

        Color col = gradient.Evaluate(lerp);
        col.a = arrow.color.a;
        arrow.color = col;
    }

    public void SetTransparency(float opacity)
    {
        Color col = allPart.color;
        col.a = opacity;
        StartCoroutine(ChangeColorTo(allPart, col));

        col = arrow.color;
        col.a = opacity;
        StartCoroutine(ChangeColorTo(arrow, col));
    }

    public IEnumerator ChangeColorTo(SpriteRenderer sR, Color endColor)
    {
        float lerp = 0;
        Color baseColor = sR.color;
        while (lerp < 1)
        {
            lerp += Time.deltaTime * colorSpeed;
                sR.color = Color.Lerp(baseColor, endColor, lerp);
            yield return new WaitForSeconds(0.01f);
        }

        sR.color = endColor;
    }

}
