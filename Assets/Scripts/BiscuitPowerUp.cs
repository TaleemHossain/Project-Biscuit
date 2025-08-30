using UnityEngine;

public class BiscuitPowerUp : MonoBehaviour
{
    public LevelManager levelManager;

    // Update is called once per frame
    public void Activate()
    {
        levelManager.DestroyAll();
        levelManager.SpawnCup(levelManager.CupCount/2);
        levelManager.AddContent();
    }
}
