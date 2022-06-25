using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameConfigScriptableObject _gameConfig;
    [SerializeField] private Transform _characterOriginPosition;
    [SerializeField] private ThreatsManager _threatsManager;

    private Character _character;


    private void Awake()
    {
        _character = FindObjectOfType<Character>(true);
        _threatsManager.OnChacterDeath += ReturnCharacterToOrigin;
    }

    private void ReturnCharacterToOrigin()
    {
        StartCoroutine(WaitDelay());
        IEnumerator WaitDelay()
        {
            yield return new WaitForSeconds(_gameConfig.DeathTriggerDelay);
            _character.transform.position = _characterOriginPosition.position;
        }
    }

    private void OnDestroy()
    {
        _threatsManager.OnChacterDeath -= ReturnCharacterToOrigin;
    }
}
