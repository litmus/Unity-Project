using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{

  public PathFollower m_startingPath;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    if (m_startingPath.m_isStopped) {
      if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") > 0) {
        m_startingPath.MoveForward();
        AudioSource.PlayClipAtPoint(m_startingPath.m_moveSound, Camera.main.transform.position, 1);
      }

      if (Input.GetButton("Submit")) {
        m_startingPath.m_waypointNode.LaunchGame();
      }

      if (Input.GetButton("Cancel")) {
        Application.LoadLevel(0);
      }
    }
  }
}
