using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour
{
  [SerializeField] private GameObject gameOverDisplay;
  [SerializeField] private AsteroidSpawner asteroidSpawner;

  [SerializeField] private TMP_Text gameOverText;

  [SerializeField] private Button playAgainButton;
  [SerializeField] private Button continueButton;
  [SerializeField] private Button returnToMenuButton;


  private ScoreHandler _scoreHandler;
  private GameObject _player;

  private void Awake()
  {
    _scoreHandler = FindObjectOfType<ScoreHandler>();
    _player = GameObject.FindGameObjectWithTag("Player");

    gameOverDisplay.SetActive(false);
  }

  private void Start()
  {
    playAgainButton.onClick.AddListener(PlayAgain);

    returnToMenuButton.onClick.AddListener(ReturnToMenu);

    continueButton.onClick.AddListener(Continue);
  }

  private void Continue()
  {
    AdManager.Instance.ShowAd(this);

    continueButton.interactable = false;
  }


  public void EndGame()
  {
    asteroidSpawner.enabled = false;

    gameOverDisplay.SetActive(true);

    // stop the score
    _scoreHandler.EndScoreTimer();

    gameOverText.text = $"Your score: {Mathf.FloorToInt(_scoreHandler.Score)}";
  }

  public void ContinueGame()
  {
    _scoreHandler.StartTimer();

    _player.transform.position = Vector3.zero;
    _player.SetActive(true);
    _player.GetComponent<Rigidbody>().velocity = Vector3.zero;

    asteroidSpawner.enabled = true;

    gameOverDisplay.SetActive(false);
  }

  private void PlayAgain()
  {
    SceneManager.LoadScene("Scene_Game");
  }

  private void ReturnToMenu()
  {
    SceneManager.LoadScene("Scene_MainMenu");
  }
}