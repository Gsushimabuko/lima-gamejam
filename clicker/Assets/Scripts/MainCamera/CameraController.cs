using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Animator mAnimator;

    void Awake()
    {
        mAnimator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<Bubble>().OnBubblePressed += OnBubblePressedDelegate;
    }

    private void OnBubblePressedDelegate()
    {
        mAnimator.Play("clickZoom");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
