using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    int score;
    void Start()
    {
        score = 0;
        UpdateText();
    }
    public void AddPoints(int num)
    {
        score += num;
        UpdateText();
    }
    void UpdateText()
    {
        transform.GetComponent<TMPro.TextMeshProUGUI>().text = "Score: " + score.ToString();
    }
}
