using UnityEngine;
public class Cup : MonoBehaviour
{
    LevelManager levelManager;
    Animator animator;
    CurrentCookie currentCookie;
    CookieCounter cookieCounter;
    public enum CupContent { None, Biscuit, PowerUp };
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
        if (cookieCounter.GetCookieCount() == 0 && cupContent == CupContent.None && !currentCookie.CookieAvailable)
        {
            levelManager.GameOver();
        }
        else if (cookieCounter.GetCookieCount() == 0 && cupContent == CupContent.Biscuit && !currentCookie.CookieAvailable)
        {
            if (animator.GetBool("Reveal")) return;
            animator.SetBool("Reveal", true);
            cookieCounter.UseOnLoanCookieBite();
        }
        else
        {
            if (animator.GetBool("Reveal")) return;
            animator.SetBool("Reveal", true);
            currentCookie.EatBite();
        }
    }
    public void PowerUpReveal()
    {
        if (animator.GetBool("Reveal")) return;
        animator.SetBool("Reveal", true);
    }
    void Vanish()
    {
        levelManager.DestroyCup(cupNumber);
    }
}