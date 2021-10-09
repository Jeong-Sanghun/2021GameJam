using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    AudioClip clickFalse;
    [SerializeField]
    AudioClip clickTrue;
    [SerializeField]
    AudioClip mainBGM;
    [SerializeField]
    AudioClip alertBGM;
    [SerializeField]
    AudioClip mosquito;
    [SerializeField]
    AudioClip siren;
    [SerializeField]
    AudioClip dishes;
    [SerializeField]
    AudioClip recycleBin;
    [SerializeField]
    AudioClip cook;
    [SerializeField]
    AudioClip toilet;
    [SerializeField]
    AudioClip[] clap;
    [SerializeField]
    AudioClip openWindow;
    [SerializeField]
    AudioClip closeWindow;
    [SerializeField]
    AudioClip coding;
    [SerializeField]
    AudioClip fly;
    [SerializeField]
    AudioClip toiletWash;

    [SerializeField]
    AudioSource clickAudio;

    [SerializeField]
    AudioSource clapAudio;
    [SerializeField]
    AudioSource bgmAudio;
    [SerializeField]
    AudioSource alertAudio;
    [SerializeField]
    AudioSource bugAudio;
    [SerializeField]
    AudioSource effectAudio;

    // Start is called before the first frame update
    void Start()
    {
        BGMChanger(false);
    }

    public void BGMChanger(bool isAlert)
    {
        if(isAlert == true)
        {
            if(bgmAudio.clip == alertBGM)
            {
                return;
            }
            SirenPlay();
            bgmAudio.clip = alertBGM;
            
        }
        else
        {
            if (bgmAudio.clip == mainBGM)
            {
                return;
            }
            bgmAudio.clip = mainBGM;
        }
        bgmAudio.Play();
    }

    public void ClickTruePlay()
    {
        clickAudio.clip = clickTrue;
        clickAudio.Play();
    }

    public void SirenPlay()
    {
        alertAudio.clip = siren;
        alertAudio.Play();
    }

    public void MosquiotoPlay()
    {
        bugAudio.clip = mosquito;
        bugAudio.Play();
    }

    public void FlyPlay()
    {
        bugAudio.clip = fly;
        bugAudio.Play();
    }

    public void ClapPlay()
    {
        clickAudio.clip = clap[Random.Range(0, clap.Length)];
        clickAudio.Play();
    }

    public void CookPlay()
    {
        effectAudio.clip = cook;
        effectAudio.Play();
    }

    public void DishesPlay()
    {
        effectAudio.clip = dishes;
        effectAudio.Play();
    }

    public void ToiletPlay()
    {
        effectAudio.clip = toilet;
        effectAudio.Play();
    }

    public void ToiletWashPlay()
    {
        effectAudio.clip = toiletWash;
        effectAudio.Play();
    }

    public void RecycleBinPlay()
    {
        effectAudio.clip = recycleBin;
        effectAudio.Play();
    }

    public void CodingPlay()
    {
        effectAudio.clip = coding;
        effectAudio.Play();
    }

    public void CloseWindowPlay()
    {
        effectAudio.clip = closeWindow;
        effectAudio.Play();
    }

    public void OpenWindowPlay()
    {
        effectAudio.clip = openWindow;
        effectAudio.Play();
    }


    public void StopEffectSound()
    {
        effectAudio.Stop();
    }



   

   
}
