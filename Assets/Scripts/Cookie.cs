using UnityEngine;
public class Cookie : MonoBehaviour
{
    CookieCounter cookieCounter;
    ScoreManager scoreManager;
    private AudioManager audioManager;
    void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
        cookieCounter = FindFirstObjectByType<CookieCounter>();
        scoreManager = FindFirstObjectByType<ScoreManager>();
    }
    public void Collect()
    {
        audioManager.PlaySFX(audioManager.CollectCookie);
        cookieCounter.AddCookie();
        scoreManager.AddPoints(100);
        Destroy(transform.gameObject);
    }
}
