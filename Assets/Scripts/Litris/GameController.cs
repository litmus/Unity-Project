using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

  Board m_gameBoard;

  ShapeSpawner m_shapeSpawner;

  Shape m_currentShape;

  PlayerInput m_playerInput;

  float m_dropInterval = 0.25f;
  float m_dropIntervalModifier;

  float m_timeToDrop;

  bool m_gameOver = false;
  public bool m_paused = true;

  public bool m_displayingTitle = false;

  public GameObject m_gameOverPanel;
  public GameObject m_pausePanel;
  public GameObject m_titlePanel;

  SoundManager m_soundManager;

  ScoreManager m_scoreManager;

  // Start is called before the first frame update
  void Start()
  {
    m_gameBoard    = GameObject.FindWithTag("Board").GetComponent<Board>();
    m_shapeSpawner = GameObject.FindWithTag("ShapeSpawner").GetComponent<ShapeSpawner>();
    m_playerInput  = GameObject.FindWithTag("PlayerInput").GetComponent<PlayerInput>();
    m_soundManager = GameObject.FindObjectOfType<SoundManager>();
    m_scoreManager = GameObject.FindObjectOfType<ScoreManager>();

    if (m_currentShape == null) {
      m_currentShape = m_shapeSpawner.SpawnShape();
    }

    m_shapeSpawner.transform.position = Vectorf.Round(m_shapeSpawner.transform.position);

    if (m_gameOverPanel) {
      m_gameOverPanel.SetActive(false);
    }

    if (m_pausePanel) {
      m_pausePanel.SetActive(false);
    }

    m_dropIntervalModifier = m_dropInterval;
    showTitleScreen();
  }

  // Update is called once per frame
  void Update()
  {
    if (m_gameOver) {
      return;
    }

    m_playerInput.handlePlayerInput(m_currentShape);

    if (Time.time > m_timeToDrop) {
      m_timeToDrop = Time.time + m_dropIntervalModifier;

      if (m_currentShape) {
        m_currentShape.MoveDown();

        if (!m_gameBoard.IsValidPosition (m_currentShape)) {
          if (m_gameBoard.IsOverflowing(m_currentShape)) {
            m_currentShape.MoveUp();
            EndGame();
          } else {
            m_currentShape.MoveUp();
            m_gameBoard.StoreShapeInGrid(m_currentShape);

            if (m_soundManager.m_effectsEnabled)
             AudioSource.PlayClipAtPoint(m_soundManager.m_dropSound, Camera.main.transform.position, m_soundManager.m_effectsVolume);

            m_gameBoard.ClearAllLines();

            if (m_gameBoard.m_linesCleared > 0) {
              m_scoreManager.CountLineScore(m_gameBoard.m_linesCleared);

              if (m_scoreManager.m_levelUp) {
                if (m_soundManager.m_effectsEnabled)
                  AudioSource.PlayClipAtPoint(m_soundManager.m_nextLevelSound, Camera.main.transform.position, m_soundManager.m_effectsVolume);

                m_dropIntervalModifier = Mathf.Clamp(m_dropInterval - (((float) m_scoreManager.m_level - 1) * 0.05f), 0.05f, 1f);
                m_scoreManager.m_levelUp = false;
              }
            }

            if (m_shapeSpawner) {
              m_currentShape = m_shapeSpawner.SpawnShape();
            }
          }
        }
      }
    }
  }

  void showTitleScreen() {
    Time.timeScale = 0;
    m_displayingTitle = true;
  }

  public void StartGame() {
    Time.timeScale = 1;
    m_displayingTitle = false;
    m_titlePanel.SetActive(false);
    m_soundManager.StartGameBackgroundMusic();
  }

  void EndGame() {
    m_gameOver = true;
    m_gameOverPanel.SetActive(true);
    m_soundManager.StopBackgroundMusic();
    if (m_soundManager.m_effectsEnabled)
     AudioSource.PlayClipAtPoint(m_soundManager.m_gameOverSound, Camera.main.transform.position, m_soundManager.m_effectsVolume);
  }

  public void RestartGame() {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

  public void ExitGame() {
    Application.LoadLevel(1);
  }

  public void TogglePause() {
    if (m_gameOver) {
      return;
    }

    m_paused = !m_paused;

    if (m_pausePanel) {
      m_pausePanel.SetActive(m_paused);

      if (m_soundManager) {
        m_soundManager.m_musicSource.volume = (m_paused) ? m_soundManager.m_musicVolume * 0.25f : m_soundManager.m_musicVolume;
      }

      Time.timeScale = (m_paused) ? 0 : 1;
    }
  }
}
