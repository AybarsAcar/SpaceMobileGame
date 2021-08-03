using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour
{
  [SerializeField] private GameObject[] asteroidPrefabs;

  [Tooltip("Seconds between asteroid spawns")] [SerializeField]
  private float spawnRate = 2f;

  [Tooltip("X_value is the lower, Y-value is the upper bound")] [SerializeField]
  private Vector2 forceRange;

  private Camera _mainCamera;

  private float _timer;

  private void Awake()
  {
    _mainCamera = Camera.main;
  }

  private void Update()
  {
    _timer -= Time.deltaTime;

    if (_timer <= 0)
    {
      // spawn a new asteroid
      SpawnAsteroid();
      
      // reset timer
      _timer += spawnRate;
    }
  }

  /// <summary>
  /// spawns a random asteroid
  /// and spawns from a random size 
  /// </summary>
  private void SpawnAsteroid()
  {
    // pick a random side of the screen
    var side = Random.Range(0, 4);

    var spawnPoint = Vector2.zero;
    var direction = Vector2.zero;

    switch (side)
    {
      case 0: // left side of the screen
        spawnPoint.x = 0;
        spawnPoint.y = Random.value;
        direction = new Vector2(1f, Random.Range(-1f, 1f));
        break;

      case 1: // right side of the screen
        spawnPoint.x = 1;
        spawnPoint.y = Random.value;
        direction = new Vector2(-1f, Random.Range(-1f, 1f));
        break;

      case 2: // Bottom side of the screen
        spawnPoint.x = Random.value;
        spawnPoint.y = 0;
        direction = new Vector2(Random.Range(-1f, 1f), 1f);
        break;

      case 3: // top side of the screen
        spawnPoint.x = Random.value;
        spawnPoint.y = 1;
        direction = new Vector2(Random.Range(-1f, 1f), -1f);
        break;
    }

    var worldSpawnPos = _mainCamera.ViewportToWorldPoint(spawnPoint);
    worldSpawnPos.z = 0;

    // pick a random asteroid and spawn
    var asteroidInstance = Instantiate(asteroidPrefabs[Random.Range(0, asteroidPrefabs.Length)], worldSpawnPos,
      Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

    // set the velocity
    var rb = asteroidInstance.GetComponent<Rigidbody>();
    rb.velocity = direction.normalized * Random.Range(forceRange.x, forceRange.y);
  }
}