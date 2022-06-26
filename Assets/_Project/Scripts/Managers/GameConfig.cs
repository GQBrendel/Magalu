using UnityEngine;

public class GameConfig : MonoBehaviour
{
    public static GameConfig Instance;

    [SerializeField] private GameConfigScriptableObject _gameConfig;

    public float DeathTriggerDelay { get { return _gameConfig.DeathTriggerDelay; } }
    public float TimeToTurnOn { get { return _gameConfig.TimeToTurnOn; } }
    public float TimeToTurnOff { get { return _gameConfig.TimeToTurnOff; } }

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
