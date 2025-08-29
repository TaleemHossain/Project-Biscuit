using UnityEngine.UI;
using UnityEngine;

public class CurrentCookie : MonoBehaviour
{
    CookieCounter cookieCounter;
    [SerializeField] Sprite FullBiscuit;
    [SerializeField] Sprite PartialBiscuit;
    [SerializeField] Sprite LastBiteBiscuit;
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
            Debug.Log("No Biscuit is Available Now");
        }
    }
    public void UpdateImage(int num)
    {
        if (num == maxBites)
        {
            image.sprite = NoBiscuit;
            CookieAvailable = false;
        }
        else if (num == 0)
        {
            image.sprite = FullBiscuit;
            CookieAvailable = true;
        }
        else if (num == maxBites - 1)
        {
            image.sprite = LastBiteBiscuit;
        }
        else
        {
            image.sprite = PartialBiscuit;
        }
    }
}