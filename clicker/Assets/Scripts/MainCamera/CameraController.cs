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
        //Asignamos delegados a Eventos de Burbuja
        Bubble.Instance.OnBubblePressed += OnBubblePressedDelegate;
        Bubble.Instance.OnBubbleTakeDamage += OnBubbleTakeDamageDelegate;
    }

    //---------------------------------------------------------------------------
    #region CameraAnimations

    private void OnBubblePressedDelegate()
    {
        mAnimator.Play("clickZoom");
    }

    private void OnBubbleTakeDamageDelegate()
    {
        mAnimator.Play("DamageShake");
    }

    #endregion
    //---------------------------------------------------------------------------

    // Update is called once per frame
    void Update()
    {
        
    }
}