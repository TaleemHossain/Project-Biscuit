using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Vector2 clickPos = Vector2.zero;
    void Start()
    {
        Cursor.SetCursor(cursorTexture, clickPos, CursorMode.Auto);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos += clickPos;
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.transform.parent != null && hit.collider.transform.parent.CompareTag("Cup"))
                {
                    hit.collider.GetComponentInParent<Cup>().Reveal();
                }
                else if (hit.collider.transform.CompareTag("Biscuit"))
                {
                    hit.collider.GetComponentInParent<Cookie>().Collect();
                }
                else if (hit.collider.transform.CompareTag("PowerUp"))
                {
                    hit.collider.GetComponentInParent<PowerUp>().Collect();
                }
            }
        }
    }
}