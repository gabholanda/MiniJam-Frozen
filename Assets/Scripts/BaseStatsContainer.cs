using UnityEngine;

[CreateAssetMenu(fileName = "New Base Stats", menuName = "SO/Base Stats")]
public class BaseStatsContainer : ScriptableObject
{
    public float Attack;
    public float MaxHealth;
    public float Speed;
    public float dashSpeed;
    public float dashCooldown;
}
