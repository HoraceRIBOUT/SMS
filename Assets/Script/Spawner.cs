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

    public void Start()
    {
        SpawnBloc();
    }


    public void SpawnBloc()
    {
        int index = Random.Range(0, blocPrefab.Count) ;

        Bloc bloc = Instantiate(blocPrefab[index], this.transform.position, Quaternion.identity, null).GetComponent<Bloc>();

        //bloc;
    }


}
