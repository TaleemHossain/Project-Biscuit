using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PowerUpCounter : MonoBehaviour
{
    ScoreManager scoreManager;
    int powerUpCount;
    [SerializeField] public int powerUpID;
    LevelManager levelManager;
    void Start()
    {
        scoreManager = FindFirstObjectByType<ScoreManager>();
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
        if (powerUpCount > 0)
        {
            scoreManager.AddPoints(-25);
            powerUpCount--;
        }
        UpdateText();
        if (powerUpType == 0)
            levelManager.EliminatePowerUp();
        else if (powerUpType == 1)
            levelManager.AddCookiePowerUp();
    }
    void UpdateText()
    {
        transform.GetComponent<TMPro.TextMeshProUGUI>().text = "X " + powerUpCount.ToString();
    }
}
