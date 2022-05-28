using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool GameOver { get; private set; }

    public void EndGame()
    {
        GameOver = true;
    }
}
