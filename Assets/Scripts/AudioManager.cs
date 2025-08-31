using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource SFX;
    [SerializeField] AudioSource Music;
    public AudioClip Background;
    public AudioClip BiteCookie;
    public AudioClip ClickButton;
    public AudioClip CollectPowerUp;
    public AudioClip UsePowerUp;
    public AudioClip GameOver;
    public AudioClip CollectCookie;
    private void Start()
    {
        Music.clip = Background;
        Music.Play();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void PlaySFX(AudioClip aud)
    {
        SFX.PlayOneShot(aud);
    }
}
