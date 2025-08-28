using UnityEngine;
using ServiceLocator.Utilities;
using ServiceLocator.Events;
using ServiceLocator.Map;
using ServiceLocator.Wave;
using ServiceLocator.Sound;
using ServiceLocator.Player;
using ServiceLocator.UI;

namespace ServiceLocator.Main
{
    public class GameService : GenericMonoSingleton<GameService>
    {
        // Services:
        private EventService EventService;
        private MapService MapService;
        private WaveService WaveService;
        private SoundService SoundService;
        private PlayerService PlayerService;

        [SerializeField] private UIService uiService;
        public UIService UIService => uiService;


        // Scriptable Objects:
        [SerializeField] private MapScriptableObject mapScriptableObject;
        [SerializeField] private WaveScriptableObject waveScriptableObject;
        [SerializeField] private SoundScriptableObject soundScriptableObject;
        [SerializeField] private PlayerScriptableObject playerScriptableObject;

        // Scene Referneces:
        [SerializeField] private AudioSource SFXSource;
        [SerializeField] private AudioSource BGSource;

        private void Start()
        {
            CreateServices();

            InjectDependencies();
        }

        private void CreateServices()
        {
            EventService = new EventService();
            MapService = new MapService(mapScriptableObject);
            WaveService = new WaveService(waveScriptableObject);
            SoundService = new SoundService(soundScriptableObject, SFXSource, BGSource);
            PlayerService = new PlayerService(playerScriptableObject);
        }

        private void InjectDependencies()
        {
            PlayerService.Init(uiService, MapService, SoundService);
            WaveService.Init(uiService, SoundService, MapService, EventService, PlayerService);
            MapService.Init(EventService);
            uiService.Init(EventService, WaveService, PlayerService);
        }

        private void Update()
        {
            PlayerService.Update();
        }
    }
}