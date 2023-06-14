using Newtonsoft.Json.Bson;
using UnityEngine;

public class SentryEnemySettings : MonoBehaviour
{   
    public static SentryEnemySettings Instance { get; private set;}

    [SerializeField] private float aggroRadius = 4f;
    public static float AggroRadius => Instance.aggroRadius;

    [SerializeField] private float attackSpeed = 2.0f;
    public static float AttackSpeed => Instance.attackSpeed;

    [SerializeField] private float health = 50.0f;
    public static float Health => Instance.health;

    [Range(0, 360)]
    [SerializeField] private float angle = 60.0f;
    public static float Angle => Instance.angle;

    [SerializeField] private float rotateSpeed = 2f;
    public static float RotateSpeed => Instance.rotateSpeed;

    [SerializeField] private LayerMask targetMask;
    public static LayerMask TargetMask => Instance.targetMask;


    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
    }
}
