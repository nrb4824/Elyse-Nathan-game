using Newtonsoft.Json.Bson;
using UnityEngine;

public class SentryEnemySettings : MonoBehaviour
{   
    public static SentryEnemySettings Instance { get; private set;}

    [SerializeField] private float sentrySpeed = 2f;
    public static float SentrySpeed => Instance.sentrySpeed;

    [SerializeField] private float aggroRadius = 4f;
    public static float AggroRadius => Instance.aggroRadius;

    [SerializeField] private float attackRange = 3f;
    public static float AttackRange => Instance.attackRange;

    [SerializeField] private GameObject sentryProjectilePrefab;
    public static GameObject SentryProjectilePrefab => Instance.sentryProjectilePrefab;

    [SerializeField] private float attackSpeed = 2.0f;
    public static float AttackSpeed => Instance.attackSpeed;

    [SerializeField] private float health = 50.0f;
    public static float Health => Instance.health;

    [SerializeField] private float damage = 10.0f;
    public static float Damage => Instance.damage;

    [Range(0, 360)]
    [SerializeField] private float angle = 60.0f;
    public static float Angle => Instance.angle;

    [SerializeField] private LayerMask targetMask;
    public static LayerMask TargetMask => Instance.targetMask;


    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
    }
}
