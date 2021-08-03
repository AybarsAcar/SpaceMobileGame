using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
  [SerializeField] private Button startGameButton;

  private void Start()
  {
    startGameButton.onClick.AddListener(StartGame);
  }

  private void StartGame()
  {
    SceneManager.LoadScene("Scene_Game");
  }
}