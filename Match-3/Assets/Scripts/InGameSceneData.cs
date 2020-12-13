﻿using Match3.Assets.Scripts.UnityComponents.UI.InGame;
using UnityEngine;

public class InGameSceneData : MonoBehaviour
{
    [SerializeField] private Camera _camera = null;
    [SerializeField] private SettingsView _settingsView = null;
    [SerializeField] private NavigationView _navigationView = null;
    [SerializeField] private ScoreView _scoreView = null;

    public Camera Camera => _camera;
    public SettingsView SettingsView => _settingsView;
    public NavigationView NavigationView => _navigationView;
    public ScoreView ScoreView => _scoreView;
}
