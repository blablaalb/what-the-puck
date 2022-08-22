using UnityEngine;
using System;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private Checkpoint _nextCheckpoint;

    [SerializeField]
    private Transform _transactionpoint;
    
    public float puckSize;
    public bool isFinishPoint;

    public Checkpoint NextCheckpoint => _nextCheckpoint;
    public Vector3 Position => transform.position;
    public Vector3 CameraLocation { get; private set; }
    public Vector3 TransactionLocation { get; private set; }
    public Quaternion CameraRotaion { get; private set; }
    public bool Reached { get; private set; }

    public static event Action<Checkpoint, Puck> CheckpointHit;

    internal void Awake()
    {
        Camera camera = GetComponentInChildren<Camera>();
        CameraLocation = camera.transform.position;
        CameraRotaion = camera.transform.rotation;
        if (_transactionpoint != null)
            TransactionLocation = _transactionpoint.position;
    }

    internal void Start()
    {
        RealPuck.CheckpointReached += OnCheckpointReached;
    }

    internal void OnDestroy()
    {
        CheckpointHit = null;
        RealPuck.CheckpointReached -= OnCheckpointReached;
    }

    internal void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Puck>() is Puck puck)
        {
            CheckpointHit?.Invoke(this, puck);
        }
    }

    private void OnCheckpointReached(Checkpoint checkpoint, RealPuck realPuck)
    {
        Reached = true;
    }
}