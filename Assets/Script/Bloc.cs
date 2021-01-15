using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloc : MonoBehaviour
{
    public static float epsilon = 0.05f;

    public Vector3 speed = Vector3.down;
    public float speedLR = 3f;
    public int questIndex = 0;

    public bool activeBloc = true;

    public float timerToWait = 0.5f;
    public float timerStopped = 1f;

    private Vector2 velocityMemory;

    public Rigidbody2D _rgbd;

    public void Init(int indexOfTheQuest)
    {
        questIndex = indexOfTheQuest;
        timerStopped = timerToWait;
        _rgbd.gravityScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (activeBloc)
        {
            Vector3 currentSpeed = speed;
            if (Input.GetKey(KeyCode.RightArrow))
            {
                currentSpeed += Vector3.right * speedLR;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                currentSpeed += Vector3.left * speedLR;
            }


            this.transform.position += currentSpeed * Time.deltaTime;
        }
        else if(timerStopped > 0)
        {
            TestIfStop();
        }
    }

    public void TestIfStop()
    {
        if (_rgbd.velocity.sqrMagnitude < epsilon * epsilon)
        {
            timerStopped -= Time.deltaTime;
            if(timerStopped < 0)
            {
                Spawner.instance.SpawnBloc();
            }
        }
        else
        {
            timerStopped = timerToWait;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //if()
        {
            Debug.Log("Hello ?");
            activeBloc = false;
            _rgbd.gravityScale = 1;
        }
    }


    public void StopAllMove()
    {
        velocityMemory = _rgbd.velocity;
        _rgbd.constraints = RigidbodyConstraints2D.FreezeAll;   
    }
    public void ResumerAllMove()
    {
        _rgbd.constraints = RigidbodyConstraints2D.None;
        _rgbd.velocity = velocityMemory;
    }



}
