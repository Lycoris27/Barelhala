using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class PlayerWalkingChange : MonoBehaviour
{
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        PlayerInputManagerScript.onWalkingPerformed += ModifyAnim;

    }
    private void OnDisable()
    {
        PlayerInputManagerScript.onWalkingPerformed -= ModifyAnim;
    }

    private void ModifyAnim(bool b)
    {
        animator.SetBool("isWalking", b);
    }
}
