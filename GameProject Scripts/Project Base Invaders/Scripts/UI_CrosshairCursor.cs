using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CrosshairCursor : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject shopMenuUI;
    [SerializeField] private GameObject winGameUI;
    [SerializeField] private GameObject startGamePopup;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Cursor.visible = false;
    }
    
    void Update()
    {
        Vector2 mouseCursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mouseCursorPos;

        if(pauseMenuUI.activeInHierarchy || shopMenuUI.activeInHierarchy || winGameUI.activeInHierarchy || startGamePopup.activeInHierarchy)
        {
            Cursor.visible = true;
            spriteRenderer.enabled = false;
        }
        else
        {
            Cursor.visible = false;
            spriteRenderer.enabled = true;
        }
            
    }
}
