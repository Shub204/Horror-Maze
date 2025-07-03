using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    // List to store all 9 chest GameObjects
    public List<GameObject> chests;

    // Struct to store position and rotation together
    [System.Serializable]
    public struct ChestTransform
    {
        public Vector3 position;
        public Quaternion rotation;
    }

    // List to store the original positions and rotations
    private List<ChestTransform> originalTransforms = new List<ChestTransform>();

    void Start()
    {
        // Store the original positions and rotations of all chests
        foreach (GameObject chest in chests)
        {
            ChestTransform ct = new ChestTransform
            {
                position = chest.transform.position,
                rotation = chest.transform.rotation
            };
            originalTransforms.Add(ct);
        }

        // Shuffle positions and rotations once at game start
        ShuffleTransforms();
    }

    void ShuffleTransforms()
    {
        // Create a copy to shuffle
        List<ChestTransform> transformsToAssign = new List<ChestTransform>(originalTransforms);

        // Fisher-Yates Shuffle algorithm
        for (int i = 0; i < transformsToAssign.Count; i++)
        {
            int randomIndex = Random.Range(i, transformsToAssign.Count);

            // Swap transforms
            ChestTransform temp = transformsToAssign[i];
            transformsToAssign[i] = transformsToAssign[randomIndex];
            transformsToAssign[randomIndex] = temp;
        }

        // Assign shuffled positions and rotations to the chests
        for (int i = 0; i < chests.Count; i++)
        {
            chests[i].transform.position = transformsToAssign[i].position;
            chests[i].transform.rotation = transformsToAssign[i].rotation;
        }
    }
}
