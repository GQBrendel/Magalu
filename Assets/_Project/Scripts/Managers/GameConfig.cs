using UnityEngine;

public class GameConfig : MonoBehaviour
{
    public static GameConfig Instance;

    [SerializeField] private GameConfigScriptableObject _gameConfig;

    public float DeathTriggerDelay { get { return _gameConfig.DeathTriggerDelay; } }
    public float TimeToTurnOn { get { return _gameConfig.TimeToTurnOn; } }
    public float TimeToTurnOff { get { return _gameConfig.TimeToTurnOff; } }
    public float PodDelay { get { return _gameConfig.PodDelay; } }
    public float IconMovementTime { get { return _gameConfig.IconMovementTime; } }
    public float IconFadeTime { get { return _gameConfig.IconFadeTime; } }

    public float BlendingLightTime { get { return _gameConfig.BlendingLightTime; } }

    public float HeadCost { get { return _gameConfig.HeadCost; } }
    public float DorsoCost { get { return _gameConfig.DorsoCost; } }
    public float WheelCost { get { return _gameConfig.WheelCost; } }
    public float RefillRate { get { return _gameConfig.RefillRate; } }
    public float OverHeatDelay { get { return _gameConfig.OverHeatDelay; } }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            return;
        }

        Destroy(gameObject);
    }
}
