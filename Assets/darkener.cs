using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darkener : MonoBehaviour
{
    public bool active = true;

    public Animator _animator;

    public void Activate()
    {
        if (active)
            return;

        _animator.SetBool("Active", true);

        active = true;
    }

    public void Deactivate()
    {
        if (!active)
            return;

        _animator.SetBool("Active", false);

        active = false;
    }
}
