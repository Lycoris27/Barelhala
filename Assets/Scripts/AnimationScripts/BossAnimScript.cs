using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class BossAnimScript : MonoBehaviour
{
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void ModifyAnim(string paramName,bool b)
    {
        animator.SetBool(paramName, b);
    }
}
