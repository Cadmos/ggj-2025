using UnityEngine;
using System.Collections.Generic;
using Unity.Mathematics;

/// <summary>
/// Static utility class for 3D Poisson disk sampling inside a sphere.
/// </summary>
public static class PoissonDisk3D
{
    /// <summary>
    /// Generates a 3D Poisson disk distribution of points inside a sphere.
    /// </summary>
    /// <param name="center">Sphere center in world space</param>
    /// <param name="radius">Sphere radius</param>
    /// <param name="sampleMinDist">Minimum distance between points</param>
    /// <param name="k">Max attempts per active sample (commonly 30)</param>
    /// <param name="seed">Random seed for reproducibility</param>
    /// <returns>A list of Vector3 positions inside the sphere</returns>
    public static List<Vector3> Generate3DPoissonPointsInSphere(
        Vector3 center,
        float radius,
        float sampleMinDist,
        int k = 30,
        uint seed = 1234)
    {
        var rng = new Unity.Mathematics.Random(seed);

        // Cell size based on the minimum distance and sqrt(3) for 3D
        float cellSize = sampleMinDist / math.sqrt(3f);

        // Figure out how many cells we need in each dimension to cover the sphere bounding box
        // The bounding box for the sphere extends from (center - radius) to (center + radius)
        // We'll store the min and max for indexing the grid
        Vector3 bbMin = center - Vector3.one * radius;
        Vector3 bbMax = center + Vector3.one * radius;

        // Convert bounding box to grid dimensions
        int gridSizeX = Mathf.CeilToInt((bbMax.x - bbMin.x) / cellSize);
        int gridSizeY = Mathf.CeilToInt((bbMax.y - bbMin.y) / cellSize);
        int gridSizeZ = Mathf.CeilToInt((bbMax.z - bbMin.z) / cellSize);

        // This 3D array or dictionary holds indices of points. -1 = empty
        // Using a single dimensional array to store the 3D data
        int[,,] grid = new int[gridSizeX, gridSizeY, gridSizeZ];
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                for (int z = 0; z < gridSizeZ; z++)
                {
                    grid[x, y, z] = -1;
                }
            }
        }

        var points = new List<Vector3>();      // Final points
        var activeList = new List<int>();      // Indices into points that are active

        // Helper function to compute grid coords for a position
        System.Func<Vector3, int3> GetGridCoords = (Vector3 pos) =>
        {
            int gx = (int)((pos.x - bbMin.x) / cellSize);
            int gy = (int)((pos.y - bbMin.y) / cellSize);
            int gz = (int)((pos.z - bbMin.z) / cellSize);
            return new int3(gx, gy, gz);
        };

        // 1) Generate the first point at random inside the sphere
        //    We'll do a simple approach: repeatedly pick a random point in the bounding box
        //    until it's inside the sphere.
        Vector3 firstPoint;
        while(true)
        {
            float rx = rng.NextFloat(bbMin.x, bbMax.x);
            float ry = rng.NextFloat(bbMin.y, bbMax.y);
            float rz = rng.NextFloat(bbMin.z, bbMax.z);
            Vector3 candidate = new Vector3(rx, ry, rz);

            if(Vector3.Distance(candidate, center) <= radius)
            {
                firstPoint = candidate;
                break;
            }
        }

        // Place the first point
        points.Add(firstPoint);
        activeList.Add(0);

        int3 coords = GetGridCoords(firstPoint);
        grid[coords.x, coords.y, coords.z] = 0;

        // 2) Start the main loop
        while (activeList.Count > 0)
        {
            // Pick a random active point index
            int randIndex = rng.NextInt(0, activeList.Count);
            int currentPointIndex = activeList[randIndex];
            Vector3 currentPoint = points[currentPointIndex];

            bool foundNewPoint = false;

            // Generate up to k new candidate points around the current point
            for (int i = 0; i < k; i++)
            {
                // Random radius between [sampleMinDist, 2 * sampleMinDist]
                float randRadius = rng.NextFloat(sampleMinDist, 2f * sampleMinDist);

                // Pick a random direction in 3D
                // NextFloat3Direction() gives a random point on the unit sphere
                float3 dir = rng.NextFloat3Direction();
                Vector3 candidate = currentPoint + (Vector3)dir * randRadius;

                // Check if candidate is inside the main sphere
                if (Vector3.Distance(candidate, center) > radius)
                {
                    continue; // outside sphere, skip
                }

                // Get candidate's cell coords
                int3 cGrid = GetGridCoords(candidate);
                // Ensure cGrid is within [0..gridSize-1]
                if (!InBounds(cGrid, gridSizeX, gridSizeY, gridSizeZ))
                {
                    continue; // out of grid bounds, skip
                }

                // Check neighboring cells to ensure min distance
                bool tooClose = false;

                // We'll find the range of cells we need to check
                int minGX = math.max(0, cGrid.x - 2);
                int maxGX = math.min(gridSizeX - 1, cGrid.x + 2);
                int minGY = math.max(0, cGrid.y - 2);
                int maxGY = math.min(gridSizeY - 1, cGrid.y + 2);
                int minGZ = math.max(0, cGrid.z - 2);
                int maxGZ = math.min(gridSizeZ - 1, cGrid.z + 2);

                for (int gx = minGX; gx <= maxGX && !tooClose; gx++)
                {
                    for (int gy = minGY; gy <= maxGY && !tooClose; gy++)
                    {
                        for (int gz = minGZ; gz <= maxGZ && !tooClose; gz++)
                        {
                            int idx = grid[gx, gy, gz];
                            if (idx != -1)
                            {
                                Vector3 existingPoint = points[idx];
                                float dist = Vector3.Distance(candidate, existingPoint);
                                if (dist < sampleMinDist)
                                {
                                    tooClose = true; // not valid
                                }
                            }
                        }
                    }
                }

                if (tooClose) 
                    continue; // candidate fails the distance check

                // If we reach here, candidate is valid!
                points.Add(candidate);
                int newIndex = points.Count - 1;
                activeList.Add(newIndex);
                grid[cGrid.x, cGrid.y, cGrid.z] = newIndex;

                foundNewPoint = true;
                break; // stop generating more for this point, move to next active
            }

            // If no point found around current active, remove it from the list
            if (!foundNewPoint)
            {
                activeList.RemoveAt(randIndex);
            }
        }

        // Done! 'points' now contains a Poisson-distributed set of points in the sphere
        return points;
    }

    /// <summary>
    /// Helper to check if cell coords are in the grid bounds
    /// </summary>
    static bool InBounds(int3 cGrid, int sizeX, int sizeY, int sizeZ)
    {
        return (cGrid.x >= 0 && cGrid.x < sizeX &&
                cGrid.y >= 0 && cGrid.y < sizeY &&
                cGrid.z >= 0 && cGrid.z < sizeZ);
    }
}
