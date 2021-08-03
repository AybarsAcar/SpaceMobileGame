using System;
using UnityEngine;
using UnityEngine.Advertisements;

/// <summary>
/// Ad Manager
/// a Singleton class
/// </summary>
public class AdManager : MonoBehaviour, IUnityAdsListener
{
  [SerializeField] private bool testMode = true;

  public static AdManager Instance;

  private GameOverHandler _gameOverHandler;

#if UNITY_ANDROID
  private readonly string _gameId = "4245685";
#elif UNITY_IOS
  private readonly string _gameId = "4245684";
#endif

  private void Awake()
  {
    if (Instance != null && Instance != this)
    {
      Destroy(gameObject);
    }
    else
    {
      Instance = this;
      DontDestroyOnLoad(gameObject);

      Advertisement.AddListener(this);
      Advertisement.Initialize(_gameId, testMode);
    }
  }

  public void ShowAd(GameOverHandler gameOverHandler)
  {
    _gameOverHandler = gameOverHandler;

    Advertisement.Show("rewardedVideo");
  }

  public void OnUnityAdsReady(string placementId)
  {
    Debug.Log("Unity Ads Ready");
  }

  public void OnUnityAdsDidError(string message)
  {
    Debug.LogError($"Unity Ads Error: {message}");
  }

  public void OnUnityAdsDidStart(string placementId)
  {
    Debug.Log("Unity Ads Started");
  }

  public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
  {
    switch (showResult)
    {
      case ShowResult.Finished:
        _gameOverHandler.ContinueGame();
        break;
      case ShowResult.Skipped:
        break;
      case ShowResult.Failed:
        Debug.LogWarning("Ad Failed");
        break;
    }
  }
}