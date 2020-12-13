using System;
using UnityEngine;

namespace Match3.Configurations
{
    [Serializable]
    public class ArenaConfig
    {
        [SerializeField] private string _name = "arena";
        [SerializeField] private float _bet = 100;

        public string Name { get => _name; }
        public float Bet { get => _bet; }
    }
}
