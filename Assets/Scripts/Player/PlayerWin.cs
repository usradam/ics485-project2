using UnityEngine;

public class PlayerWin : MonoBehaviour
{
    private UIManager uiManager;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GameEnd")
        {
            // Disable player movement to prevent further input
            if (playerMovement != null)
                playerMovement.enabled = false;

            // Show win screen
            uiManager.Win();
        }
    }
}
