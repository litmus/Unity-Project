using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

  Board m_gameBoard;

  GameController m_gameController;

  float m_timeToNextKey;

  float m_keyRepeatRate = 0.1f;

  SoundManager m_soundManager;

  void Start() {
    m_timeToNextKey = Time.time;
    m_gameBoard = GameObject.FindWithTag("Board").GetComponent<Board>();
    m_soundManager = GameObject.FindObjectOfType<SoundManager>();
    m_gameController = GameObject.FindObjectOfType<GameController>();
  }

  public void handlePlayerInput(Shape currentShape) {
    if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") > 0 && Time.time > m_timeToNextKey) {
      currentShape.MoveRight();
      m_timeToNextKey = Time.time + m_keyRepeatRate;

      if (m_gameBoard.IsValidPosition(currentShape)) {
        // nothin'
        if (m_soundManager.m_effectsEnabled)
          AudioSource.PlayClipAtPoint(m_soundManager.m_moveSound, Camera.main.transform.position, m_soundManager.m_effectsVolume/4);
      } else {
        currentShape.MoveLeft();
      }
    }

    if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") < 0  && Time.time > m_timeToNextKey) {
      currentShape.MoveLeft();
      m_timeToNextKey = Time.time + m_keyRepeatRate;

      if (m_gameBoard.IsValidPosition(currentShape)) {
        // nothin'
        if (m_soundManager.m_effectsEnabled)
          AudioSource.PlayClipAtPoint(m_soundManager.m_moveSound, Camera.main.transform.position, m_soundManager.m_effectsVolume/4);
      } else {
        currentShape.MoveRight();
      }
    }

    if (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") > 0) {
      currentShape.RotateRight();

      if (!m_gameBoard.IsValidPosition(currentShape)) {
        currentShape.RotateLeft();
      } else {
        if (m_soundManager.m_effectsEnabled)
          AudioSource.PlayClipAtPoint(m_soundManager.m_moveSound, Camera.main.transform.position, m_soundManager.m_effectsVolume/4);
      }
    }

    if (Input.GetButton("Vertical") && Input.GetAxisRaw("Vertical") < 0  && Time.time > m_timeToNextKey) {
      currentShape.MoveDown();
      m_timeToNextKey = Time.time + m_keyRepeatRate;

      if (m_gameBoard.IsValidPosition(currentShape)) {
        // nothin'
        if (m_soundManager.m_effectsEnabled)
          AudioSource.PlayClipAtPoint(m_soundManager.m_moveSound, Camera.main.transform.position, m_soundManager.m_effectsVolume/4);
      } else {
        currentShape.MoveUp();
      }
    }

    if (Input.GetButton("Submit")) {
      if (m_gameController.m_displayingTitle) {
        m_gameController.StartGame();
      }
    }
  }
}
