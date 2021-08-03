using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
  private GameOverHandler _gameOverHandler;

  private void Awake()
  {
    _gameOverHandler = FindObjectOfType<GameOverHandler>();
  }

  public void Crash()
  {
    // do not destroy the game object
    // we will make them watch an ad to respawn
    gameObject.SetActive(false);
    
    _gameOverHandler.EndGame();
  }
}