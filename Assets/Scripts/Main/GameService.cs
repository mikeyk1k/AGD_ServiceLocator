using ServiceLocator.Events;
using ServiceLocator.Player;
using ServiceLocator.Sound;
using ServiceLocator.UI;
using ServiceLocator.Utilities;
using ServiceLocator.Map;
using ServiceLocator.Wave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService : GenericMonoSingleton<GameService>
{
    //UIService
    [SerializeField] private UIService uiService;
    public UIService UIService => uiService;

    //PlayerService
    public PlayerService PlayerService { get; private set; }
    [SerializeField] public PlayerScriptableObject playerScriptableObject;

    //SoundService
    public SoundService SoundService { get; private set; }
    [Header("SoundService")]
    [SerializeField] public SoundScriptableObject soundScriptableObject;
    [SerializeField] private AudioSource audioEffects;
    [SerializeField] private AudioSource backgroundMusic;

    //EventService
    public EventService EventService { get; private set; }

    //MapService
    public MapService MapService { get; private set; }
    [SerializeField] private MapScriptableObject mapScriptableObject;

    //WaveService
    public WaveService WaveService { get; private set; }
    [SerializeField] private WaveScriptableObject waveScriptableObject;
    private void Start()
    {
        PlayerService = new PlayerService(playerScriptableObject);
        SoundService = new SoundService(soundScriptableObject, audioEffects, backgroundMusic);
        EventService = new EventService();
        MapService = new MapService(mapScriptableObject);
        WaveService = new WaveService(waveScriptableObject);
    }
    private void Update()
    {
        PlayerService.Update();
    }
}
