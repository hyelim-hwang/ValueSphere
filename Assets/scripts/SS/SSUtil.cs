using UnityEngine;

namespace SS {
    public static class SSUtil {
        //constants
        public static readonly Vector2 VECTOR2_NAN = new Vector2(float.NaN,
            float.NaN);
        public static readonly Vector3 VECTOR3_NAN = new Vector3(float.NaN,
            float.NaN, float.NaN);
        public static readonly Quaternion QUATERNION_NAN =
            new Quaternion(float.NaN, float.NaN, float.NaN, float.NaN);
        private static readonly System.Random random = new System.Random();
        private static readonly char[] ID_CHARS =
            "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ_abcdefghijklmnopqrstuvwxyz-".
            ToCharArray();
        private static readonly int ID_LENGTH = 21;

        //methods
        public static void createDebugSphere(Vector3 pt) {
            GameObject debugSphere = GameObject.CreatePrimitive(
                PrimitiveType.Sphere);
            debugSphere.name = "DebugSphere";
            debugSphere.transform.position = pt;
            debugSphere.transform.localScale = 0.05f * Vector3.one;
            debugSphere.GetComponent<MeshRenderer>().material.color = Color.red;
        }

        public static string createId() {
            char[] idChars = new char[ID_LENGTH];
            for (int i = 0; i < ID_LENGTH; i++) {
                idChars[i] = ID_CHARS[random.Next(0, ID_CHARS.Length)];
            }
            return new string(idChars);
        }

        public static Vector2 vector3To2(Vector3 v3) {
            Vector2 v2 = new Vector2(v3.x, v3.y);
            return v2;
        }

        public static Vector3 vector2To3(Vector2 v2, float z) {
            Vector3 v3 = new Vector3(v2.x, v2.y, z);
            return v2;
        }
    }
}