using UnityEngine.UI;
using UnityEngine;

public class CurrentCookie : MonoBehaviour
{
    CookieCounter cookieCounter;
    ScoreManager scoreManager;
    [SerializeField] Sprite FullBiscuit;
    [SerializeField] Sprite Bite1Biscuit;
    [SerializeField] Sprite Bite2Biscuit;
    [SerializeField] Sprite Bite3Biscuit;
    [SerializeField] Sprite Bite4Biscuit;
    [SerializeField] Sprite NoBiscuit;
    [SerializeField] public int maxBites = 4;
    public int currentBites;
    public bool CookieAvailable = false;
    Image image;
    private AudioManager audioManager;
    void Start()
    {
        currentBites = 0;
        audioManager = FindFirstObjectByType<AudioManager>();
        cookieCounter = FindFirstObjectByType<CookieCounter>();
        scoreManager = FindFirstObjectByType<ScoreManager>();
        image = transform.GetComponent<Image>();
    }
    public void EatBite()
    {
        audioManager.PlaySFX(audioManager.BiteCookie);
        scoreManager.AddPoints(-50);
        currentBites++;
        UpdateImage(currentBites);
        if (currentBites >= maxBites)
        {
            cookieCounter.SetCurrentCookie();
        }
    }
    public void UpdateImage(int num)
    {
        if (num == 0)
        {
            image.sprite = FullBiscuit;
            CookieAvailable = true;
        }
        else if (num == maxBites - 1)
        {
            image.sprite = Bite4Biscuit;
        }
        else if (num == 1)
        {
            image.sprite = Bite1Biscuit;
        }
        else if (num == 2)
        {
            image.sprite = Bite2Biscuit;
        }
        else if (num == 3)
        {
            image.sprite = Bite3Biscuit;
        }
        else
        {
            image.sprite = NoBiscuit;
            CookieAvailable = false;
        }
    }
}