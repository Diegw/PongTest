using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configuration/Ball", fileName = "BallConfiguration")]
public class BallSettings : BaseSettings
{
    public float InitialSpeed => _initialSpeed;
    public float InitialDelay => _initialDelay;
    public float SpeedIncrement => _speedIncrement;
    public Vector2 Size => _size;

    [SerializeField, Min(1)] private float _initialSpeed = 1.0f;
    [SerializeField, Min(0)] private float _initialDelay = 2.0f;
    [SerializeField, Min(0)] private float _speedIncrement = 0.1f;
    [SerializeField, Min(0.1f)] private Vector2 _size = new Vector2(0.2f, 0.2f);
}
