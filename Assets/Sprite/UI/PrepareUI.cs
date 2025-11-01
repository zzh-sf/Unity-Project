using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepareUI : MonoBehaviour
{
    private Animator animator;
    private Action onComplete;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.enabled = false;
    }
    
    public void Show(Action onComplete) 
    { 
        this.onComplete = onComplete; 
        animator.enabled = true;
    }
    
    void OnShowComplete() 
    {
        onComplete?.Invoke();
    }
}