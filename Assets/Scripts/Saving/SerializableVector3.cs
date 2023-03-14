using UnityEngine;

namespace RPG.Saving
{
    [System.Serializable]
    public class SerializableVector3
    {
        float x, y, z;

        public SerializableVector3(Vector3 _vector)
        {
            x = _vector.x;
            y = _vector.y;
            z = _vector.z;
        }

        public Vector3 ToVector()
        {
            return new Vector3(x, y, z);
        }
    }
}