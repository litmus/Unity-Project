using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    int m_score = 0;
    int m_lines = 0;
    public int m_level = 1;

    public int m_linesPerLevel = 5;

    public Text m_linesText;
    public Text m_levelText;
    public Text m_scoreText;

    public bool m_levelUp = false;

    public void CountLineScore(int numLines) {
      switch(numLines) {
        case 1:
          m_score += 100 * m_level;
          break;
        case 2:
          m_score += 300 * m_level;
          break;

        case 3:
          m_score += 500 * m_level;
          break;
        case 4:
          m_score += 800 * m_level;
          break;
      }

      m_lines -= numLines;

      if (m_lines <= 0)
        BumpLevel();

      UpdateUIText();
    }

    public void Reset() {
      m_level = 1;
      m_lines = m_linesPerLevel * m_level;
      UpdateUIText();
    }

    void UpdateUIText() {
      // add 0s to front of total score
      string numStr = m_score.ToString();
      while (numStr.Length < 5) {
        numStr = "0" + numStr;
      }

      m_linesText.text = m_lines.ToString();
      m_levelText.text = m_level.ToString();
      m_scoreText.text = numStr;
    }

    public void BumpLevel() {
      m_level++;
      m_lines = m_linesPerLevel * m_level;
      m_levelUp = true;
    }

    // Start is called before the first frame update
    void Start()
    {
      Reset();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
