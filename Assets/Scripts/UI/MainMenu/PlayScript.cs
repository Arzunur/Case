using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayScript : MonoBehaviour
{
    [Header("UI Panel")]
    public GameObject mainMenuPanel; 

    [Header("Character")]
    public PlayerController characterMovement;

    [Header("Ball Thrower")]
    public BallThrower ballThrower; 

    public void Play()
    {
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(false);

        if (characterMovement != null)
            characterMovement.StartMoving();

        if (ballThrower != null)
        {
          //  Debug.Log("▶️ Oyun başladı, top atma aktif!");
            ballThrower.EnableShooting(true);
        }

        PlayerInput playerInput = FindObjectOfType<PlayerInput>();
        if (playerInput != null)
            playerInput.EnableInput(true);
    }

}
