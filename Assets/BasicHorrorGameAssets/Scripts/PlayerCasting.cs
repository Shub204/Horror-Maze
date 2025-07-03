using UnityEngine;

public class PlayerCasting : MonoBehaviour
{
    public static float DistanceFromTarget;
    public float ToTarget;

    void Update()
    {
        RaycastHit Hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out Hit))
        {
            ToTarget = Hit.distance;
            DistanceFromTarget = ToTarget;
        }
        else
        {
            DistanceFromTarget = Mathf.Infinity; // If nothing is hit, set to a high number
        }
    }
}
