using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathFollower : MonoBehaviour
{
    Node[] PathNodes;
    public Node m_waypointNode;

    public GameObject Maily;
    public float MoveSpeed;
    float Timer;
    static Vector2 startPosition;
    static Vector3 CurrentPositionHolder;
    int CurrentNode = 0;

    bool m_onLastNode = false;
    public bool m_isStopped = false;

    public GameObject titleObject;
    public Text titleText;

    public AudioClip m_moveSound;
    public AudioClip m_gameSound;

    void CheckNode() {
      if (CurrentNode == PathNodes.Length) {
        m_onLastNode = true;
      } else {
        Timer = 0;
        startPosition = Maily.transform.position;
        CurrentPositionHolder = PathNodes[CurrentNode].transform.position;
      }
    }

    void Start() {
      PathNodes = GetComponentsInChildren<Node>();
      CurrentPositionHolder = PathNodes[0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
      if (!m_isStopped) {
        if (m_onLastNode) {
          CurrentNode = 0;
          CheckNode();
          m_onLastNode = false;
        } else {
          MoveForward();
        }
      }
    }

    public void MoveForward() {
      m_isStopped = false;
      titleText.text = "";
      Timer += Time.deltaTime * MoveSpeed;
      if (Maily.transform.position != CurrentPositionHolder) {
        Maily.transform.position = Vector3.Lerp(startPosition, CurrentPositionHolder, Timer);
      } else {
        if (CurrentNode <= PathNodes.Length - 1) {
          if (PathNodes[CurrentNode].m_isWaypoint) {
            m_waypointNode = PathNodes[CurrentNode];
            titleText.text = m_waypointNode.title;

            if (m_waypointNode.gameLaunchId != null && m_waypointNode.gameLaunchId != "") {
              AudioSource.PlayClipAtPoint(m_gameSound, Camera.main.transform.position, 1);
            }
            titleText.transform.position = Maily.transform.position;
            m_isStopped = true;
          }
          CurrentNode++;
          CheckNode();
        }
      }
    }
}
