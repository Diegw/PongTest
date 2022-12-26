using UnityEngine;

[CreateAssetMenu(menuName = "Configuration/PowerUp/Float", fileName = "FloatModifierConfiguration")]
public class FloatModifierSetting : BasePowerUpSettings
{
    public float Modifier => _modifier;
    
    [SerializeField, Min(0f)] private float _modifier = 0.2f;
}
