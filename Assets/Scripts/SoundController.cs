/* (c) Irina Astafeva, 2023 */

using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource Background;
    public AudioSource Eat;
    public AudioSource Lose;
    public AudioSource Pop;
    public AudioSource Win;


    private void Awake()
    {
        Background.Play();
    }

    public void SetSoundMute(bool mute)
    {
        Background.mute = mute;
        Eat.mute = mute;
        Lose.mute = mute;
        Pop.mute = mute;
        Win.mute = mute;
    }

    public void PlayEatSound()
    {
        Eat.Play();
    }

    public void PlayPopSound()
    {
        Pop.Play();
    }

    public void PlayLoseSound()
    {
        Background.Stop();
        Lose.Play();
    }

    public void PlayWinSound()
    {
        Background.Stop();
        Win.Play();
    }

}
