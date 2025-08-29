using TMPro;
using UnityEngine;

public class CookieCounter : MonoBehaviour
{
    CurrentCookie currentCookie;
    TextMeshProUGUI textMeshPro;
    int cookieCount = 0;
    void Start()
    {
        currentCookie = FindFirstObjectByType<CurrentCookie>();
        textMeshPro = transform.GetComponent<TextMeshProUGUI>();
        AddCookie();
    }
    public int GetCookieCount()
    {
        return cookieCount;
    }
    public void AddCookie()
    {
        cookieCount++;
        UpdateText(cookieCount);
        if (!currentCookie.CookieAvailable)
        {
            SetCurrentCookie();
        }
    }
    public void UpdateText(int num)
    { 
        textMeshPro.text = "and " + num.ToString() + " biscuits left";
    }
    public void SetCurrentCookie()
    {
        if (cookieCount > 0)
        {
            cookieCount--;
            currentCookie.currentBites = 0;
            currentCookie.UpdateImage(currentCookie.currentBites);
            UpdateText(cookieCount);
        }
        else
        {
            Debug.Log("No Biscuit Available");
        }
    }
}
