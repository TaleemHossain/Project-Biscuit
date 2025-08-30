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
    public GameObject GameOver;
    void Start()
    {
        GameOver.SetActive(false);
        currentCookie = FindFirstObjectByType<CurrentCookie>();
        cookieCounter = FindFirstObjectByType<CookieCounter>();
        animator = transform.GetComponent<Animator>();
        levelManager = FindFirstObjectByType<LevelManager>();
    }
    public void Reveal()
    {
        if (cookieCounter.GetCookieCount() == 0 && cupContent == CupContent.None && !currentCookie.CookieAvailable)
        {
            Debug.Log("No Biscuit Available");
            GameOver.SetActive(true);
            Debug.Log("Game Should Show End Game Screen");
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
    void Vanish()
    {
        levelManager.DestroyCup(cupNumber);
    }
}