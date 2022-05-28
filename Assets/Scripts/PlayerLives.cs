using System;
using UnityEngine;

public class PlayerLives : MonoBehaviour
{
   [SerializeField] private int _maxLives = 3;
   [SerializeField] private GameObject _gameOverCanvas;

   private int _currentLives;
   private PlayerController _player;
   private GameManager _gameManager;

   private void Awake()
   {
      _player = GetComponent<PlayerController>();
      _gameManager = FindObjectOfType<GameManager>();
   }

   private void Start()
   {
      _currentLives = _maxLives;
      _gameOverCanvas.SetActive(false);
   }

   private void Update()
   {
      if (_currentLives <= 0)
      {
         _gameOverCanvas.SetActive(true);
         _gameManager.EndGame();
      }
   }

   public void DecreaseLives()
   {
      _currentLives--;
      _player.Kill();
   }
}
