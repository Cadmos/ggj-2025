using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Jobs;
using Unity.Burst;
using Unity.Mathematics;
using static Unity.Mathematics.math;

namespace GGJ
{

    public class SphereLooper : MonoBehaviour
 {
    [Header("Sphere Boundary")]
    [SerializeField] private float sphereRadius = 5f;
    [SerializeField] private float sampleMinDist = 1f;    // Radius of the sphere

    [Header("Bubble Motion")]
    [SerializeField] private float baseUpSpeed = 1f;          // Base upward speed
    [SerializeField] private float horizontalTurbulence = 0.2f; // Horizontal wiggle amplitude
    [SerializeField] private float verticalTurbulence = 0.1f;   // Additional vertical speed variation
    [SerializeField] private float noiseFrequency = 0.5f;       // Frequency for Perlin noise

    [Header("Reset to Bottom Inside Factor")]
    // How far inside the bottom of the sphere to reposition the bubble
    [Range(0f, 1f)]
    [SerializeField] private float bottomInsideFactor = 0.9f;

    [Header("Horizontal Reset Randomization")]
    [SerializeField] private bool randomizeHorizontalReset = true;
    [SerializeField] private float resetHorizontalRange = 0.2f; // The horizontal offset range

    [Header("Poisson Prewarm")]
    [SerializeField] private bool prewarm = true;
    [SerializeField] private uint prewarmSeed = 1234;    
    [SerializeField] private int poissonK = 30;     

    private Transform[] _children;
    private NativeArray<float3> _positions;
    private JobHandle _jobHandle;

    void Start()
    {
        // Gather ONLY direct children of this GameObject
        int childCount = transform.childCount;
        _children = new Transform[childCount];

        for (int i = 0; i < childCount; i++)
        {
            _children[i] = transform.GetChild(i);
        }

        // Initialize NativeArray for positions
        _positions = new NativeArray<float3>(_children.Length, Allocator.Persistent);

        if (prewarm)
        {
            // 1) Generate Poisson distribution of points in the sphere
            List<Vector3> poissonPoints =
                PoissonDisk3D.Generate3DPoissonPointsInSphere(
                    center: transform.position,
                    radius: sphereRadius,
                    sampleMinDist: sampleMinDist,
                    k: poissonK,
                    seed: prewarmSeed);

            // 2) We might get more or fewer points than childCount
            //    So let's randomly assign each child to a random Poisson point
            //    Or clamp if there aren't enough points
            for (int i = 0; i < _children.Length; i++)
            {
                // Wrap around if we have fewer Poisson points than children
                Vector3 chosen = poissonPoints[i % poissonPoints.Count];
                _positions[i] = new float3(chosen.x, chosen.y, chosen.z);
            }
        }
        else
        {
            // Use original positions
            for (int i = 0; i < _children.Length; i++)
            {
                Vector3 p = _children[i].position;
                _positions[i] = new float3(p.x, p.y, p.z);
            }
        }
    }

    void Update()
    {
        // Create the job
        var bubbleJob = new BubbleLoopJob
        {
            positions             = _positions,
            center                = transform.position,
            radius                = sphereRadius,
            baseUpSpeed           = baseUpSpeed,
            horizontalTurbulence  = horizontalTurbulence,
            verticalTurbulence    = verticalTurbulence,
            noiseFreq             = noiseFrequency,
            deltaTime             = Time.deltaTime,
            currentTime           = Time.time,
            bottomFactor          = bottomInsideFactor,
            randomizeHorizontal   = randomizeHorizontalReset,
            horizontalRange       = resetHorizontalRange
        };

        // Schedule the job across all children
        _jobHandle = bubbleJob.Schedule(_positions.Length, 64);

        // Complete the job so we can copy data back
        _jobHandle.Complete();

        // Apply the updated positions
        for (int i = 0; i < _children.Length; i++)
        {
            _children[i].position = _positions[i];
        }
    }

    void OnDestroy()
    {
        _jobHandle.Complete();
        if (_positions.IsCreated)
        {
            _positions.Dispose();
        }
    }

    [BurstCompile]
    private struct BubbleLoopJob : IJobParallelFor
    {
        // Positions for each child
        public NativeArray<float3> positions;

        // Sphere properties
        public float3 center;
        public float  radius;

        // Movement/turbulence properties
        public float baseUpSpeed;
        public float horizontalTurbulence;
        public float verticalTurbulence;
        public float noiseFreq;

        public float deltaTime;
        public float currentTime;

        // For repositioning inside the sphere’s bottom
        public float bottomFactor;

        // For optional random horizontal offset
        public bool  randomizeHorizontal;
        public float horizontalRange;

        public void Execute(int index)
        {
            float3 pos = positions[index];

            // Unique offset for each child based on the index
            float uniqueOffset = index * 13.37f;

            // Sample noise for horizontal and vertical offsets
            float hNoise = noise.cnoise(new float2(currentTime * noiseFreq, uniqueOffset));
            float vNoise = noise.cnoise(new float2(uniqueOffset, currentTime * noiseFreq));

            // Horizontal turbulence in X and Z
            pos.x += hNoise * horizontalTurbulence * deltaTime;
            pos.z += hNoise * horizontalTurbulence * deltaTime;

            // Vertical speed is base plus noise-based variation
            float totalUpSpeed = baseUpSpeed + vNoise * verticalTurbulence;
            pos.y += totalUpSpeed * deltaTime;

            // If the object goes outside the sphere, reposition it more inside at the bottom.
            if (distance(pos, center) > radius)
            {
                // Move bubble to Y just inside the bottom: center.y - radius * bottomFactor
                pos.y = center.y - radius * bottomFactor;

                // Optional: random horizontal offset to break up alignment
                if (randomizeHorizontal)
                {
                    // A simple pseudo-random approach (using sin/cos).
                    // For a better approach, use Unity.Mathematics.Random with a stored seed per instance.
                    float randX = sin(uniqueOffset + currentTime * 25.1234f) * 0.5f;
                    float randZ = cos(uniqueOffset + currentTime * 21.9876f) * 0.5f;
                    randX *= horizontalRange;
                    randZ *= horizontalRange;

                    pos.x = center.x + randX;
                    pos.z = center.z + randZ;
                }
            }

            positions[index] = pos;
        }
    }
}
}