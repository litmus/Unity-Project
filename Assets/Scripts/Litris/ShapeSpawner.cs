﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeSpawner : MonoBehaviour
{

  public Shape[] m_allShapes;

  Shape GetRandomShape()
  {
    int i = Random.Range(0,m_allShapes.Length);
    if (m_allShapes[i])
    {
      return m_allShapes[i];
    }
    else
    {
      Debug.LogWarning("WARNING! Invalid shape in spawner!");
      return null;
    }

  }

  public Shape SpawnShape()
  {
    Shape shape = null;
    shape = Instantiate(GetRandomShape(), transform.position, Quaternion.identity) as Shape;

    if (shape)
    {
      return shape;
    }
    else
    {
      Debug.LogWarning("WARNING! Invalid shape in spawner!");
      return null;
    }
  }
}
