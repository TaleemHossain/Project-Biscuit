using UnityEngine;

public class Cup : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
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
        Destroy(gameObject);
    }
}
