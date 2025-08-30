using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpCounter powerUpCounter;
    [SerializeField] public int powerUpID;
    void Start()
    {
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
            Destroy(transform.gameObject);
    }
}
