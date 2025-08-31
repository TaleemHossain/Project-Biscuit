using UnityEngine;

public class PowerUp : MonoBehaviour
{
    ScoreManager scoreManager;
    public PowerUpCounter powerUpCounter;
    [SerializeField] public int powerUpID;
    private AudioManager audioManager;
    void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
        scoreManager = FindFirstObjectByType<ScoreManager>();
        PowerUpCounter[] powerUpCounters = FindObjectsByType<PowerUpCounter>(FindObjectsSortMode.None);
        foreach (var counter in powerUpCounters)
        {
            if (powerUpID == counter.GetComponent<PowerUpCounter>().powerUpID)
            {
                powerUpCounter = counter;
                break;
            }
        }
    }
    public void Collect()
    {
        if (powerUpCounter.AddPowerUp(powerUpID))
        {
            audioManager.PlaySFX(audioManager.CollectPowerUp);
            scoreManager.AddPoints(50);
            Destroy(transform.gameObject);
        }
    }
}
