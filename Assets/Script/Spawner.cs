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
    public Balance balance;

    public List<Color> availableColor;


    [Header("Current")]
    public List<Bloc> allCurrentBloc = new List<Bloc>();

    [Header("Changing focus")]
    public Darkener darkener;

    public void Start()
    {
        SpawnBloc();
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
    }


    [ContextMenu("Transparent")]
    public void SetTransparent()
    {
        foreach(Bloc bl in allCurrentBloc)
        {
            bl.SetTransparency(0.25f);
        }
        balance.SetTransparency(0.25f);

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

}
