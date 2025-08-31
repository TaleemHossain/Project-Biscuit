using UnityEngine;
public class Cookie : MonoBehaviour
{
    CookieCounter cookieCounter;
    ScoreManager scoreManager;
    void Start()
    {
        cookieCounter = FindFirstObjectByType<CookieCounter>();
        scoreManager = FindFirstObjectByType<ScoreManager>();
    }
    public void Collect()
    {
        cookieCounter.AddCookie();
        scoreManager.AddPoints(100);
        Destroy(transform.gameObject);
    }
}
