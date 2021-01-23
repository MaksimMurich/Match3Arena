using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Match3
{
    public class LobbyTopPannelView : MonoBehaviour {
        [SerializeField] private Text _coins = null;

        public Text Coins { get => _coins; }
    }
}
