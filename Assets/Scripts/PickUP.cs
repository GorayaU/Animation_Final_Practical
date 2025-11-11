using System;
using UnityEngine;

public class PickUP : MonoBehaviour
{
    private int WeaponCount = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            Debug.Log("Weapon Collected!");
            Destroy(other.gameObject);
            WeaponCount += 1;
        }
    }

    private void Update()
    {
        if (WeaponCount >= 2)
        {
            Debug.Log("Game Over! You collected all weapons.");
            Application.Quit();
        }
    }
}
