using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillMe : MonoBehaviour
{
    public float timer = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroy", timer);
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }
}
