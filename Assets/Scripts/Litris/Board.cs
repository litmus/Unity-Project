using System.Collections;
using UnityEngine;

public class Board : MonoBehaviour
{
  public Transform m_emptySprite;
  public int m_height = 30;
  public int m_width = 10;
  public int m_header = 8;

  public int m_linesCleared = 0;

  Transform[,] m_grid;

  SoundManager m_soundManager;

  void Awake() {
    m_grid = new Transform[m_width, m_height];
  }

  // Start is called before the first frame update
  void Start()
  {
    m_soundManager = GameObject.FindObjectOfType<SoundManager>();
    DrawGrid();
  }

  // Update is called once per frame
  void Update()
  {

  }

  bool IsWithinBoard(int x, int y) {
    return (x >= 0 && x < m_width && y >= 0);
  }

  public bool IsValidPosition(Shape shape)
  {
    foreach (Transform child in shape.transform) {
      Vector2 pos = Vectorf.Round(child.position);

      if (!IsWithinBoard((int) pos.x, (int) pos.y)) {
        return false;
      }

      if (IsOccupied((int) pos.x, (int) pos.y, shape))
        return false;
    }
    return true;
  }

  bool IsOccupied(int x, int y, Shape shape)
  {
    return (m_grid[x, y] != null && m_grid[x,y].parent != shape.transform);
  }

  void DrawGrid()
  {
    if (m_emptySprite) {
      for (int y = 0; y < m_height - m_header; y++) {
        for (int x = 0; x < m_width; x++) {
          Transform clone;
          clone = Instantiate(m_emptySprite, new Vector3(x, y, 2), Quaternion.identity) as Transform;
          clone.transform.parent = transform;
        }
      }
    } else {
      Debug.Log("Please assign the emptySprite object.");
    }
  }

  public void StoreShapeInGrid(Shape shape)
  {
    if (shape == null)
      return;

    foreach (Transform child in shape.transform) {
      Vector2 pos = Vectorf.Round(child.position);
      m_grid[(int) pos.x, (int) pos.y] = child;
    }
  }

  bool LineComplete(int y) {
    for (int x = 0; x < m_width; x++) {
      if (m_grid[x,y] == null) {
        return false;
      }
    }
    return true;
  }

  void ClearLine(int y) {
    for (int x = 0; x < m_width; x++) {
      if (m_grid[x,y] != null) {
        Destroy(m_grid[x,y].gameObject);
      }
      m_grid[x,y] = null;
    }

    if (m_soundManager.m_effectsEnabled)
      AudioSource.PlayClipAtPoint(m_soundManager.m_clearLineSound, Camera.main.transform.position, m_soundManager.m_effectsVolume);
  }

  void ShiftOneLineDown(int y) {
    for (int x = 0; x < m_width; x++) {
      if (m_grid[x,y] != null) {
        m_grid[x, y-1] = m_grid[x,y];
        m_grid[x,y] = null;
        m_grid[x, y-1].position += new Vector3(0, -1, 0);
      }
    }
  }

  void ShiftLinesDown(int startY) {
    for (int i = startY; i < m_height; i++) {
      ShiftOneLineDown(i);
    }
  }

  public void ClearAllLines() {
    m_linesCleared = 0;
    for (int y = 0; y < m_height; y++) {
      if (LineComplete(y)) {
        m_linesCleared++;
        ClearLine(y);
        ShiftLinesDown(y+1);
        y--;
      }
    }
  }

  public bool IsOverflowing(Shape shape) {
    foreach (Transform child in shape.transform) {
      if (child.transform.position.y >= (m_height - m_header - 1)) {
        return true;
      }
    }
    return false;
  }
}
