using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject biscuitPrefab;
    [SerializeField] GameObject CupParentPrefab;
    [SerializeField] int cupCount = 1;
    [SerializeField] float spacingX = 2f;
    [SerializeField] float spacingY = 2.5f;
    public List<GameObject> CupContainer;
    void Start()
    {
        transform.position = Vector3.zero;
        SpawnCup(cupCount);
    }
    void Update()
    {

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
            CupContainer.Add(cup);
        }
    }
    public void Proceed()
    {
        DestroyAllCups();
        CupContainer.Clear();
        cupCount++;
        if (cupCount > 20)
        {
            cupCount = 20;
        }
        SpawnCup(cupCount);
    }
    void DestroyAllCups()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            if(transform.GetChild(i).CompareTag("Cup"))
                Destroy(transform.GetChild(i).gameObject);
        }
    }
}
