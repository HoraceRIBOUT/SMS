﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloc : MonoBehaviour
{
    public static float epsilon = 0.05f;

    public Vector2 minxMaxX = new Vector2(-4,4);

    public Vector3 speed = Vector3.down;
    public float speedLR = 3f;
    public float rotationSpeed = 4f;
    public int questIndex = 0;

    public bool activeBloc = true;

    public float timerToWait = 0.5f;
    public float timerStopped = 1f;

    private Vector2 velocityMemory;

    public Rigidbody2D _rgbd;
    private Coroutine rotateRoutine = null;
    private Quaternion targetRotation;


    public List<SpriteRenderer> myBox;

    public bool freeze = false;

    public Color brown;
    public float colorSpeed = 1;

    public GameObject particuleDeat;


    public void Init(int indexOfTheQuest, Color col)
    {
        questIndex = indexOfTheQuest;
        timerStopped = timerToWait;
        _rgbd.gravityScale = 0;
        targetRotation = this.transform.rotation;

        foreach(SpriteRenderer sR in GetComponentsInChildren<SpriteRenderer>())
        {
            myBox.Add(sR);
            sR.color = col;
        }

    }

    bool goLeftPls = false;
    bool goRightPls = false;

    public void GoLeft()
    {
        if (activeBloc)
            goLeftPls = true;
    }

    public void GoRight()
    {
        if(activeBloc)
            goRightPls = true;
    }

    public void Rotate()
    {
        if (rotateRoutine != null) StopCoroutine(rotateRoutine);
        rotateRoutine = StartCoroutine(LaunchRotation(true));
    }

    // Update is called once per frame
    void Update()
    {
        if (freeze)
            return;


        if (activeBloc)
        {
            Vector3 currentSpeed = speed;
            if ((Input.GetKey(KeyCode.RightArrow) || goRightPls) && this.transform.position.x < minxMaxX.y)
            {
                currentSpeed += Vector3.right * speedLR;
                goRightPls = false;
            } 
            if ((Input.GetKey(KeyCode.LeftArrow) || goLeftPls) && this.transform.position.x > minxMaxX.x)
            {
                currentSpeed += Vector3.left * speedLR;
                goLeftPls = false;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                currentSpeed += speed;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                currentSpeed += speed * 6f;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Rotate();
            }


            this.transform.position += currentSpeed * Time.deltaTime;
        }
        else if(timerStopped > 0)
        {
            TestIfStop();
        }
    }

    public IEnumerator LaunchRotation(bool up)
    {
        float lerp = 0;
        Quaternion baseRotation = this.transform.rotation;
        targetRotation = Quaternion.Euler(0, 0, (up?90:-90) + targetRotation.eulerAngles.z);
        while (lerp < 1)
        {
            lerp += Time.deltaTime * rotationSpeed;
            float interpretedLerp = Mathf.Clamp01(lerp);
            this.transform.rotation = Quaternion.Lerp(baseRotation, targetRotation, interpretedLerp);

            yield return new WaitForSeconds(0.01f);
        }
        this.transform.rotation = targetRotation;
        rotateRoutine = null;
    }
    
    [ContextMenu("Set Brown")]
    public void SetBrown()
    {
        Color col = brown;
        col.a = myBox[0].color.a;
        StartCoroutine(ChangeColorTo(col));
    }

    public void SetTransparency (float opacity)
    {
        Color col = myBox[0].color;
        col.a = opacity;
        StartCoroutine(ChangeColorTo(col));
    }
    
    public IEnumerator ChangeColorTo(Color endColor)
    {
        float lerp = 0;
        Color baseColor = myBox[0].color;
        while (lerp < 1)
        {
            lerp += Time.deltaTime * colorSpeed;
            foreach (SpriteRenderer sR in myBox)
            {
                sR.color = Color.Lerp(baseColor, endColor, lerp);
            }
            yield return new WaitForSeconds(0.01f);
        }
        foreach (SpriteRenderer sR in myBox)
        {
            sR.color = endColor;
        }
    }


    public void TestIfStop()
    {
        if (timerStopped <= 0)
        {
            Spawner.instance.SpawnBloc(questIndex);
            return;
        }
        if (_rgbd.velocity.sqrMagnitude < epsilon * epsilon)
        {
            timerStopped -= Time.deltaTime;
        }
        else
        {
            timerStopped = timerToWait;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Bord bord = collision.gameObject.GetComponent<Bord>();
        if(bord != null)
        {
            if(timerStopped > 0)
                Spawner.instance.SpawnBloc(questIndex);

            KillMe();
            return;
        }

        if(activeBloc)
        {
//            Debug.Log("Hello ?");
            activeBloc = false;
            Spawner.instance.imfree = true;
           _rgbd.gravityScale = 1;

//            Debug.Log("Hello : " + timerStopped);
            if (timerStopped == 0)
            {
                Spawner.instance.SpawnBloc(questIndex);
            }
        }
    }


    public void StopAllMove()
    {
        velocityMemory = _rgbd.velocity;
        _rgbd.constraints = RigidbodyConstraints2D.FreezeAll;
        _rgbd.velocity = Vector2.zero;
        _rgbd.gravityScale = 0;
        freeze = true;
        activeBloc = false;
    }
    public void ResumeAllMove()
    {
        _rgbd.constraints = RigidbodyConstraints2D.None;
        _rgbd.velocity = velocityMemory;
        _rgbd.gravityScale = 1;
        freeze = false;
    }


    public void KillMe()
    {
        foreach (SpriteRenderer sR in myBox)
        {
            Instantiate(particuleDeat, sR.transform.position, Quaternion.identity, null);
        }

        Spawner.instance.allCurrentBloc.Remove(this);
        Destroy(this.gameObject);
    }

    //[ContextMenu("SpriteChange")]
    //public void ChangeAllSpriteTo()
    //{
    //    foreach(SpriteRenderer sR in GetComponentsInChildren<SpriteRenderer>())
    //    {
    //        sR.sprite = GameManager.instance.newSprite;
    //    }
    //}

}
