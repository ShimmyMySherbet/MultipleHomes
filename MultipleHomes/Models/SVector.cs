using UnityEngine;

namespace ShimmyMySherbet.MultipleHomes.Models
{
    public struct SVector
    {
        public static readonly SVector Nil = new SVector(0f, 0f, 0f);

        public float X;
        public float Y;
        public float Z;

        public SVector(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public SVector(Vector3 vector)
        {
            X = vector.x;
            Y = vector.y;
            Z = vector.z;
        }

        public Vector3 ToVector3() => new Vector3(X, Y, Z);
    }
}