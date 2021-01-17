﻿using System.Collections;
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
        foreach(Bloc bloc in allCurrentBloc)
        {
            bloc.StopAllMove();
        }
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
        if (lastBloc != null)
            lastBloc.GoLeft();
    }

    public void ActiveBlocGoRight()
    {
        if (lastBloc != null)
            lastBloc.GoRight();
    }


    public void SpawnBloc()
    {
        if (!imfree)
            return;
        int index = Random.Range(0, blocPrefab.Count) ;

        Bloc bloc = Instantiate(blocPrefab[index], this.transform.position, Quaternion.identity, null).GetComponent<Bloc>();


        Color randColo = availableColor[Random.Range(0, availableColor.Count)];
        bloc.Init(1, randColo);

        //bloc;
        imfree = false;

        allCurrentBloc.Add(bloc);
        lastBloc = bloc;
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
