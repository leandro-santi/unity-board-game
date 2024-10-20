using UnityEngine;

public class DiceFaceReader : MonoBehaviour
{
    [SerializeField] private Transform[] faceTransforms;
    [SerializeField] private int[] faceValues;

    public int GetTopFaceValue()
    {
        float maxDot = -1f;
        int topFaceIndex = -1;

        for (int i = 0; i < faceTransforms.Length; i++)
        {
            float dot = Vector3.Dot(faceTransforms[i].up, Vector3.up);
            if (dot > maxDot)
            {
                maxDot = dot;
                topFaceIndex = i;
            }
        }

        return topFaceIndex >= 0 ? faceValues[topFaceIndex] : -1;
    }
}