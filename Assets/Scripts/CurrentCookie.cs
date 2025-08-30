using UnityEngine.UI;
using UnityEngine;

public class CurrentCookie : MonoBehaviour
{
    CookieCounter cookieCounter;
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
    void Start()
    {
        currentBites = 0;
        cookieCounter = FindFirstObjectByType<CookieCounter>();
        image = transform.GetComponent<Image>();
    }
    public void EatBite()
    {
        currentBites++;
        UpdateImage(currentBites);
        if (currentBites == maxBites)
        {
            cookieCounter.SetCurrentCookie();
        }
        if (currentBites > maxBites)
        {
            Debug.Log("You shouldn't be here");
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