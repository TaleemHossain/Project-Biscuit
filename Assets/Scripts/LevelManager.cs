using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    ScoreManager scoreManager;
    [SerializeField] GameObject biscuitPrefab;
    [SerializeField] GameObject CupParentPrefab;
    [SerializeField] List<GameObject> PowerUpsPrefab;
    [SerializeField] int cupCount = 1;
    [SerializeField] int maxCupCount = 15;
    [SerializeField] int biscuitCount = 1;
    [SerializeField] float spacingX = 2f;
    [SerializeField] float spacingY = 2.5f;
    [SerializeField] Vector3 generateCookieAt = new(0f, -4f, 0f);
    int maxPowerUpCountinLevel;
    int streak;
    public List<GameObject> CupContainer = new();
    public List<GameObject> InGameBiscuitContainer = new();
    public List<GameObject> PowerUpInGame = new();
    public GameObject ProceedButton;
    public GameObject gameOver;
    void Start()
    {
        scoreManager = FindFirstObjectByType<ScoreManager>();
        ProceedButton.SetActive(false);
        gameOver.SetActive(false);
        transform.position = Vector3.zero;
        SpawnCup(cupCount);
        AddContent();
        maxPowerUpCountinLevel = 0;
        streak = 0;
    }
    void Update()
    {
        if (InGameBiscuitContainer.Count == 0)
        {
            if (!ProceedButton.activeSelf) ProceedButton.SetActive(true);
        }
    }
    public void SpawnCup(int cupCount)
    {
        int rows = Mathf.CeilToInt((float)cupCount / 5f);
        int cols = Mathf.CeilToInt((float)cupCount / rows);

        float medianRow = (float)(rows + 1) / 2f;
        float medianCol = (float)(cols + 1) / 2f;

        for (int i = 0; i < cupCount; i++)
        {
            int row_num = i / cols + 1;
            int col_num = i % cols + 1;
            float posX = -(medianCol - col_num) * spacingX;
            float posY = -(row_num - medianRow) * spacingY;
            GameObject cup = Instantiate(CupParentPrefab, new Vector3(posX, posY, 0), Quaternion.identity, transform);
            cup.GetComponent<Cup>().cupNumber = i;
            CupContainer.Add(cup);
        }
    }
    public void AddContent()
    {
        int cupCount = CupContainer.Count;
        List<int> indices = new();
        for (int i = 0; i < cupCount; i++)
        {
            indices.Add(i);
        }
        for (int i = 0; i < indices.Count; i++)
        {
            int rand = Random.Range(i, indices.Count);
            int temp = indices[i];
            indices[i] = indices[rand];
            indices[rand] = temp;
        }
        List<int> biscuitIndices = new();
        for (int i = 0; i < biscuitCount; i++)
        {
            if (indices.Count == 0) break;
            int randIndex = Random.Range(0, indices.Count);
            biscuitIndices.Add(indices[randIndex]);
            foreach (GameObject cup in CupContainer)
            {
                if (cup.GetComponent<Cup>().cupNumber == indices[randIndex])
                {
                    cup.GetComponent<Cup>().cupContent = Cup.CupContent.Biscuit;
                    GameObject biscuit = Instantiate(biscuitPrefab, cup.transform);
                    biscuit.transform.localPosition = generateCookieAt;
                    InGameBiscuitContainer.Add(biscuit);
                    indices.RemoveAt(randIndex);
                    break;
                }
            }
        }
        for (int i = 0; i < maxPowerUpCountinLevel; i++)
        {
            if (indices.Count == 0) break;
            int randIndexForCup = Random.Range(0, indices.Count);
            int randIndexForPowerUp = Random.Range(0, PowerUpsPrefab.Count);
            foreach (GameObject cup in CupContainer)
            {
                if (cup.GetComponent<Cup>().cupNumber == indices[randIndexForCup])
                {
                    cup.GetComponent<Cup>().cupContent = Cup.CupContent.PowerUp;
                    GameObject PowerUp = Instantiate(PowerUpsPrefab[randIndexForPowerUp], cup.transform);
                    PowerUp.transform.localPosition = generateCookieAt;
                    PowerUpInGame.Add(PowerUp);
                    indices.RemoveAt(randIndexForCup);
                    break;
                }
            }
        }
    }
    public void Proceed()
    {
        if (InGameBiscuitContainer.Count > 0) return;
        DestroyAll();
        CupContainer.Clear();
        InGameBiscuitContainer.Clear();
        PowerUpInGame.Clear();
        cupCount++;
        if (cupCount > maxCupCount)
        {
            cupCount = maxCupCount;
        }
        maxPowerUpCountinLevel = (cupCount - 1) / 5;
        SpawnCup(cupCount);
        AddContent();
        ProceedButton.SetActive(false);
    }
    public void DestroyAll()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            if (transform.GetChild(i).CompareTag("Cup"))
                Destroy(transform.GetChild(i).gameObject);
            if (transform.GetChild(i).CompareTag("Biscuit"))
                Destroy(transform.GetChild(i).gameObject);
            if (transform.GetChild(i).CompareTag("PowerUp"))
                Destroy(transform.GetChild(i).gameObject);
        }
    }
    public void DestroyCup(int num)
    {
        GameObject cup = CupContainer.Find(c => c.GetComponent<Cup>().cupNumber == num);
        if (cup != null)
        {
            if (cup.GetComponent<Cup>().cupContent == Cup.CupContent.Biscuit)
            {
                streak++;
                GameObject biscuit = null;
                foreach (Transform child in cup.transform)
                {
                    if (child.CompareTag("Biscuit"))
                    {
                        biscuit = child.gameObject;
                        break;
                    }
                }
                if (biscuit != null)
                {
                    InGameBiscuitContainer.Remove(biscuit);
                    biscuit.transform.SetParent(transform);
                }
            }
            else if (cup.GetComponent<Cup>().cupContent == Cup.CupContent.PowerUp)
            {
                streak++;
                GameObject powerUp = null;
                foreach (Transform child in cup.transform)
                {
                    if (child.CompareTag("PowerUp"))
                    {
                        powerUp = child.gameObject;
                        break;
                    }
                }
                if (powerUp != null)
                {
                    PowerUpInGame.Remove(powerUp);
                    powerUp.transform.SetParent(transform);
                }
            }
            else
            {
                streak = 0;
            }
            CupContainer.Remove(cup);
            Destroy(cup);
            scoreManager.AddPoints(200 * streak);
        }
    }
    public void EliminatePowerUp()
    {
        if(CupContainer.Count == 0) return;
        int randIndex = Random.Range(0, CupContainer.Count);
        CupContainer[randIndex].GetComponent<Cup>().PowerUpReveal();   
    }
    public void AddCookiePowerUp()
    {
        if(CupContainer.Count == 0) return;
        List<int> indices = new();
        foreach (var cup in CupContainer)
        {
            if (cup.GetComponent<Cup>().cupContent == Cup.CupContent.None)
            {
                indices.Add(cup.GetComponent<Cup>().cupNumber);
            }
        }
        if (indices.Count == 0) return;
        int randIndex = Random.Range(0, indices.Count);
        foreach (var cup in CupContainer)
        {
            if (cup.GetComponent<Cup>().cupNumber == indices[randIndex])
            {
                cup.GetComponent<Cup>().cupContent = Cup.CupContent.Biscuit;
                GameObject biscuit = Instantiate(biscuitPrefab, cup.transform);
                biscuit.transform.localPosition = generateCookieAt;
                break;
            }
        }
    }
    public void GameOver()
    {
        gameOver.SetActive(true);
    }
    public void Retry()
    {
        SceneManager.LoadScene(0);
    }
}
