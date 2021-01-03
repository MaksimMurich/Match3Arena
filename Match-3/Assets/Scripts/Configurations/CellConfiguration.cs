using Match3.Assets.Scripts.UnityComponents.UI.InGame;
using Match3.Components.Game;
using Match3.UnityComponents;
using System;
using UnityEngine;

namespace Match3.Configurations
{
    [Serializable]
    public class CellConfiguration
    {
        [SerializeField] private CellType _type;
        [Tooltip("weight of current cell, when choosing a random cell for spawn")]
        [SerializeField] private float _spawnWeight = 10;
        [SerializeField] private int _health = 50;
        [SerializeField] private int _demage = 50;
        [SerializeField] private CellView _view = null;
        [SerializeField] private CellRewardView _healthRewardView = null;
        [SerializeField] private CellRewardView _demageRewardView = null;

        private float _spawnRangeMin = -1;
        private float _spawnRangeMax = -1;

        public CellType Type => _type;
        public float Weight => _spawnWeight;
        public int Health => _health;
        public int Demage => _demage;
        public CellView ViewExample => _view;
        public CellRewardView HealthRewardView { get => _healthRewardView; }
        public CellRewardView DemageRewardView { get => _demageRewardView; }

        public void SetSpawnRange(float minValue, float maxValue)
        {
            _spawnRangeMin = minValue;
            _spawnRangeMax = maxValue;
        }

        public bool CheckInSpawnRabge(float value)
        {
            bool inRange = value >= _spawnRangeMin && value < _spawnRangeMax;
            return inRange;
        }
    }
}