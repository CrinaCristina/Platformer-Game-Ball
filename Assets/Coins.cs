using UnityEngine;

public class Coin : MonoBehaviour
{
    // Optional: You can add a sound effect or animation for when the coin is collected.

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Collect();
        }
    }

   

   public  void Collect()
    {
        // Here you can add logic for sound or animation
        // For now, we'll just deactivate the coin
        gameObject.SetActive(false);

        // Optionally, notify the game manager or score manager that a coin was collected
        // GameManager.Instance.CoinCollected();
    }
}