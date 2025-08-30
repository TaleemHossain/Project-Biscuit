using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject biscuitPrefab;
    [SerializeField] GameObject CupParentPrefab;
    [SerializeField] int cupCount = 1;
    [SerializeField] int biscuitCount = 1;
    [SerializeField] float spacingX = 2f;
    [SerializeField] float spacingY = 2.5f;
    public List<GameObject> CupContainer;
    public List<GameObject> InGameBiscuitContainer;
    void Start()
    {
        transform.position = Vector3.zero;
        SpawnCup(cupCount);
        AddContent();
    }
    private void SpawnCup(int cupCount)
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
    private void AddContent()
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
                    biscuit.transform.localPosition = new Vector3(0f, -0.4f, 0f);
                    InGameBiscuitContainer.Add(biscuit);
                    break;
                }
            }
            indices.RemoveAt(randIndex);
        }
    }
    public void Proceed()
    {
        if (InGameBiscuitContainer.Count > 0) return;
        DestroyAll();
        CupContainer.Clear();
        cupCount++;
        if (cupCount > 20)
        {
            cupCount = 20;
        }
        SpawnCup(cupCount);
        AddContent();
    }
    void DestroyAll()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            if (transform.GetChild(i).CompareTag("Cup"))
                Destroy(transform.GetChild(i).gameObject);
            if (transform.GetChild(i).CompareTag("Biscuit"))
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
            CupContainer.Remove(cup);
            Destroy(cup);
        }
    }
}
