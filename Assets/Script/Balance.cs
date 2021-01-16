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
        arrow.color = gradient.Evaluate(lerp);
    }
}
