using UnityEngine;

public abstract class BasePowerUpSettings : ScriptableObject
{
    public int ComponentId => _componentId;
    
    [SerializeField, Min(0)] protected int _componentId = 0;
}
