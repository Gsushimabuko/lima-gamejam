using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    private Animator mAnimator;

    int currentSlide;

    //--------------------------------------------

    void Awake()
    {
        mAnimator = GetComponent<Animator>();

        //Inicia en el Slide 1
        currentSlide = 1;
    }

    //--------------------------------------------

    public void Continue()
    {
        switch (currentSlide)
        {
            case 1:
                mAnimator.Play("Slide2");
                currentSlide = 2;
                break;

            case 2:
                mAnimator.Play("Slide3");
                currentSlide = 3;
                break;

            case 3:
                mAnimator.Play("Slide4");
                currentSlide = 4;
                break;

            case 4:
                mAnimator.Play("FadeIn");
                break;
        }
    }

    public void GoToTuTorial()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
