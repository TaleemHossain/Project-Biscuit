using UnityEngine;

public class PowerUpCounter : MonoBehaviour
{
    int powerUpCount;
    [SerializeField] public int powerUpID;
    LevelManager levelManager;
    void Start()
    {
        powerUpCount = 0;
        levelManager = FindFirstObjectByType<LevelManager>();
    }
    public bool AddPowerUp(int num)
    {
        if (powerUpID == num)
        {
            powerUpCount++;
            UpdateText();
            return true;
        }
        return false;
    }
    public void UsePowerUp(int powerUpType)
    {
        if (powerUpCount == 0) return;
        if (powerUpCount > 0) powerUpCount--;
        UpdateText();
        if (powerUpType == 0)
            levelManager.EliminatePowerUp();
        else if (powerUpType == 1)
            levelManager.AddCookiePowerUp();
    }
    void UpdateText()
    {
        transform.GetComponent<TMPro.TextMeshProUGUI>().text = " X " + powerUpCount.ToString();
    }
}
