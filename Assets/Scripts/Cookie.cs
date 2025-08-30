using UnityEngine;

public class Cookie : MonoBehaviour
{
    CookieCounter cookieCounter;
    void Start()
    {
        cookieCounter = FindFirstObjectByType<CookieCounter>();
    }
    public void Collect()
    {
        cookieCounter.AddCookie();
        Destroy(transform.gameObject);
    }
}
