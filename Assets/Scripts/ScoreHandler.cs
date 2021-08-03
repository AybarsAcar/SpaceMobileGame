using System;
using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
  [SerializeField] private TMP_Text scoreText;
  [SerializeField] private float scoreMultiplier = 1f;

  private float _score;
  public float Score => _score;

  private bool _isCounting = true;

  private void Update()
  {
    if (!_isCounting) return; 

    _score += Time.deltaTime * scoreMultiplier;

    scoreText.text = Mathf.FloorToInt(_score).ToString();
  }

  public void EndScoreTimer()
  {
    _isCounting = false;
    scoreText.text = string.Empty;
  }

  public void StartTimer()
  {
    _isCounting = true;
  }
}