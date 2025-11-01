using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FailUI : MonoBehaviour
{
    private Animator animator;
    public Image FailMask;
    private Animator Manimator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        Manimator=FailMask.GetComponent<Animator>();
        Hide();
    }
    public void Hide() 
    {
        animator.enabled = false;
        FailMask.gameObject.SetActive(false);
        Manimator.enabled = false;
    }
    public void Show() 
    {
    animator.enabled = true;
    FailMask.gameObject.SetActive(true);
    Manimator.enabled = true;
    }
}
