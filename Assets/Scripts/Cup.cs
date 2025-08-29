using UnityEngine;

public class Cup : MonoBehaviour
{
    LevelManager levelManager;
    Animator animator;
    public enum CupContent{None, Biscuit};
    public CupContent cupContent = CupContent.None;
    public int cupNumber;
    void Start()
    {
        animator = transform.GetComponent<Animator>();
        levelManager = FindFirstObjectByType<LevelManager>();
    }

    void Update()
    {

    }
    public void Reveal()
    {
        if (animator.GetBool("Reveal")) return;
        animator.SetBool("Reveal", true);
    }
    void Vanish()
    {
        levelManager.DestroyCup(cupNumber);
    }
}
