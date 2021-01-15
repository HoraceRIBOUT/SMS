using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balance : MonoBehaviour
{
    public Rigidbody2D _rgbd;

    // Update is called once per frame
    void Update()
    {
        if(_rgbd.velocity.magnitude > Bloc.epsilon * Bloc.epsilon)
        {
            _rgbd.velocity *= Time.deltaTime;
        }
    }
}
