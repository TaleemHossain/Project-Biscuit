using UnityEngine;

public class Cup : MonoBehaviour
{
    LevelManager levelManager;
    Animator animator;
    CurrentCookie currentCookie;
    CookieCounter cookieCounter;
    public enum CupContent { None, Biscuit };
    public CupContent cupContent = CupContent.None;
    public int cupNumber;
    void Start()
    {
        currentCookie = FindFirstObjectByType<CurrentCookie>();
        cookieCounter = FindFirstObjectByType<CookieCounter>();
        animator = transform.GetComponent<Animator>();
        levelManager = FindFirstObjectByType<LevelManager>();
    }
    public void Reveal()
    {
        if(cookieCounter.GetCookieCount() == 0 && cupContent == CupContent.None && !currentCookie.CookieAvailable)
        {
            Debug.Log("No Biscuit Available");
            Debug.Log("Game Should Show End Game Screen");
            return;
        }
        if (animator.GetBool("Reveal")) return;
        animator.SetBool("Reveal", true);
        currentCookie.EatBite();
    }
    void Vanish()
    {
        levelManager.DestroyCup(cupNumber);
    }
}
