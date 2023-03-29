using Newtonsoft.Json.Bson;
using UnityEngine;

public class EnemySettings : MonoBehaviour
{   
    public static EnemySettings Instance { get; private set;}

    [SerializeField] private float droneSpeed = 2f;
    public static float DroneSpeed => Instance.droneSpeed;

    [SerializeField] private float aggroRadius = 4f;
    public static float AggroRadius => Instance.aggroRadius;

    [SerializeField] private float attackRange = 3f;
    public static float AttackRange => Instance.attackRange;

    [SerializeField] private GameObject droneProjectilePrefab;
    public static GameObject DroneProjectilePrefab => Instance.droneProjectilePrefab;

    [SerializeField] private float attackSpeed = 2.0f;
    public static float AttackSpeed => Instance.attackSpeed;

    [SerializeField] private float health = 50.0f;
    public static float Health => Instance.health;

    [SerializeField] private float damage = 10.0f;
    public static float Damage => Instance.damage;


    private void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
    }
}
