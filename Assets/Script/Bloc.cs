using System.Collections;
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

    public void Init(int indexOfTheQuest)
    {
        questIndex = indexOfTheQuest;
        timerStopped = timerToWait;
        _rgbd.gravityScale = 0;
        targetRotation = this.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (activeBloc)
        {
            Vector3 currentSpeed = speed;
            if (Input.GetKey(KeyCode.RightArrow) && this.transform.position.x < minxMaxX.y)
            {
                currentSpeed += Vector3.right * speedLR;
            }
            if (Input.GetKey(KeyCode.LeftArrow) && this.transform.position.x > minxMaxX.x)
            {
                currentSpeed += Vector3.left * speedLR;
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
                if (rotateRoutine != null) StopCoroutine(rotateRoutine);
                rotateRoutine = StartCoroutine( LaunchRotation(true));
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
        Bord bord = collision.gameObject.GetComponent<Bord>();
        if(bord != null)
        {
            if(timerStopped > 0)
                Spawner.instance.SpawnBloc();

            KillMe();
            return;
        }

        //if()
        {
//            Debug.Log("Hello ?");
            activeBloc = false;
            Spawner.instance.imfree = true;
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


    public void KillMe()
    {
        Destroy(this.gameObject);
    }


}
