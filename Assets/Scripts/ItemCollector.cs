using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemCollector : MonoBehaviour
{

  private int cherries = 0;

[SerializeField] private Text cherriesText;

  private void OnTriggerEnter2D (Collider2D collision) {
    if (collision.gameObject.CompareTag("Cherry")) {
      Destroy(collision.gameObject);
      cherries++;
      cherriesText.text = "Cherries: " + cherries;
    }

  }

  void Update() {
    if (cherries==8) {
      LoadLevel();
    }
  }

  public void LoadLevel() {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
  }
}
