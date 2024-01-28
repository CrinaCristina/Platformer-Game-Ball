using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinCollector : MonoBehaviour
{
    public static int coinsCollected = 0; // Static to keep track across all coin instances
    public int totalCoinsNeededToWin;
    public string winSceneName = "Win"; // Name of the 'You Won' scene

    private SphereController sphereController;

    void Start()
    {
        sphereController = GameObject.FindGameObjectWithTag("Player").GetComponent<SphereController>();
        if (sphereController == null)
        {
            Debug.LogError("SphereController not found on Player");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && sphereController != null)
        {
            coinsCollected++;
            sphereController.UpdateScore(coinsCollected); // Update score in SphereController
            CheckWinCondition();
            gameObject.SetActive(false); // Deactivate the coin
        }
    }

    private void CheckWinCondition()
    {
        if (coinsCollected >= totalCoinsNeededToWin)
        {
            SceneManager.LoadScene(winSceneName);
        }
    }
}
