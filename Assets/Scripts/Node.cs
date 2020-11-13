using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Node : MonoBehaviour
{
  public bool m_isWaypoint = false;
  public string title;
  public string gameLaunchId;

  public void LaunchGame() {
    if (gameLaunchId != null) {
      SceneManager.LoadScene(gameLaunchId);
    }
  }
}
