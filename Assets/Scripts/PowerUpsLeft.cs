using Unity.VisualScripting;
using UnityEngine;

public class PowerUpsLeft : MonoBehaviour
{
    LevelManager levelManager;
    void Start()
    {
        levelManager = FindFirstObjectByType<LevelManager>();
    }
    void Update()
    {
        int numOfPowerUps = levelManager.PowerUpInGame.Count;
        if (numOfPowerUps > 1)
            transform.GetComponent<TMPro.TextMeshProUGUI>().text = "*You can still get " + numOfPowerUps.ToString() + " power-ups in this level";
        else if (numOfPowerUps == 1)
            transform.GetComponent<TMPro.TextMeshProUGUI>().text = "*You can still get " + numOfPowerUps.ToString() + " power-up in this level";
        else
            transform.GetComponent<TMPro.TextMeshProUGUI>().text = "*There are no power-ups to collect in this level";
    }
}
