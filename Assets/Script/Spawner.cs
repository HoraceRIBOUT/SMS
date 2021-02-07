using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public static Spawner instance = null;

    public void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public List<GameObject> blocPrefab = new List<GameObject>();
    public bool imfree = true;
    public bool onOverClock = false;
    public Balance balance;

    public List<Color> availableColor;


    [Header("Current")]
    public Bloc lastBloc = null;
    public List<Bloc> allCurrentBloc = new List<Bloc>();

    [Header("Changing focus")]
    public Darkener darkener;
    public float opacityForBalanceAndBox = 0.1f;

    public void Start()
    {
        //SpawnBloc();
        SetTransparent();
    }

    public void StopAllBloc()
    {
        Debug.Log("Stop all move");
        Bloc blocMain = null;
        foreach(Bloc bloc in allCurrentBloc)
        {
            if (bloc.activeBloc)
                blocMain = bloc;
            else
                bloc.StopAllMove();
        }
        if(blocMain != null)
            blocMain.KillMe();

        balance.StopAllMove();
    }

    public void ResumeAllBloc()
    {
        Debug.Log("Resume all move");
        foreach (Bloc bloc in allCurrentBloc)
        {
            bloc.ResumeAllMove();
        }
        balance.ResumeAllMove();
    }


    public void ActiveBlocGoLeft()
    {
        Debug.Log("Go left");
        leftInput = true;
    }

    public void ActiveBlocGoRight()
    {
        Debug.Log("Go right");
        rightInput = true;
    }

    public void DeactiveBlocGoLeft()
    {
        Debug.Log("Stop left");
        leftInput = false;
    }

    public void DeactiveBlocGoRight()
    {
        Debug.Log("Stop right");
        rightInput = false;
    }


    public void ActiveRotate()
    {
        if (lastBloc != null)
            lastBloc.Rotate();
    }

    bool rightInput = false;
    bool leftInput = false;
    public void Update()
    {
        if (rightInput)
        {
            if (lastBloc != null)
                lastBloc.GoRight();
        }
        if (leftInput)
        {
            if (lastBloc != null)
                lastBloc.GoLeft();
        }
    }

    public IEnumerator cleanUpWait(int questNumber)
    {
        yield return new WaitForSeconds(.5f);
        onOverClock = false;
        SpawnBloc(questNumber);
    }

    public void SpawnBloc(int questNumber)
    {
        if (!imfree)
            return;
        if (onOverClock)
            return;

        if (allCurrentBloc.Count > 25)
        {
            onOverClock = true;
            CleanUp();
            StartCoroutine(cleanUpWait(questNumber));
            return;
        }

        int index = Random.Range(0, blocPrefab.Count) ;

        Bloc bloc = Instantiate(blocPrefab[index], this.transform.position, Quaternion.identity, null).GetComponent<Bloc>();


        Color randColo = availableColor[index];//availableColor[Random.Range(0, availableColor.Count)];
        bloc.Init(questNumber, randColo);

        //bloc;
        imfree = false;

        allCurrentBloc.Add(bloc);
        
        lastBloc = bloc;
    }

    public void CleanUp()
    {
        for (int i = allCurrentBloc.Count - 1; i > 5 ; i--)
        {
            allCurrentBloc[i].KillMe();
        }
    }

    public void ClearBloc(int questNumber)
    {
        List<Bloc> toDestroy = new List<Bloc>();
        foreach (Bloc bloc in allCurrentBloc)
        {
            if(bloc.questIndex == questNumber)
            {
                bloc.SetBrown();
            }
            else
            {
                toDestroy.Add(bloc);
            }
        }
        foreach (Bloc bloc in toDestroy)
        {
            bloc.KillMe();
        }

    }


    [ContextMenu("Transparent")]
    public void SetTransparent()
    {
        foreach(Bloc bl in allCurrentBloc)
        {
            bl.SetTransparency(opacityForBalanceAndBox);
        }
        balance.SetTransparency(opacityForBalanceAndBox);

        darkener.Deactivate();
    }

    [ContextMenu("Opaque")]
    public void SetOpaque()
    {
        foreach (Bloc bl in allCurrentBloc)
        {
            bl.SetTransparency(1);
        }
        balance.SetTransparency(1);

        darkener.Activate();

       
    }

    public bool GetBalanceResult()
    {
        float zAngle = balance.transform.rotation.eulerAngles.z;
        if (zAngle > 180)
            zAngle -= 360;

        return zAngle > 0;
    }

}
