using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource SFX;
    public AudioClip BiteCookie;
    public AudioClip ClickButton;
    public AudioClip CollectPowerUp;
    public AudioClip UsePowerUp;
    public AudioClip GameOver;
    public AudioClip CollectCookie;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void PlaySFX(AudioClip aud)
    {
        SFX.PlayOneShot(aud);
    }
}
