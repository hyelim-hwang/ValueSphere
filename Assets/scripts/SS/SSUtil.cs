using UnityEngine;
using SS.AppObject;

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

        //util functions
        public enum BlendMode {
            Opaque = 0,
            Cutout,
            Fade,
            Transparent
        }

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

        public static bool hits(Camera cam, SSAppGeom3D appGeom3D,
            Vector2 touchedPt) {
            Ray ray = cam.ScreenPointToRay(touchedPt);
            RaycastHit hit;
            Collider collider = appGeom3D.getCollider();
            if (collider.Raycast(ray, out hit, Mathf.Infinity)) {
                return true;
            } else {
                return false;
            }
        }

        // 교점 계산 함수
        public static bool FindLineIntersection(Vector3 p1, Vector3 d1,
            Vector3 p2, Vector3 d2, out Vector3 intersection) {
            intersection = Vector3.zero;

            Vector3 crossDir = Vector3.Cross(d1, d2);
            float denominator = crossDir.sqrMagnitude;

            // 평행 여부 확인
            if (denominator < Mathf.Epsilon) {
                Debug.Log("Lines are parallel or collinear.");
                return false;
            }

            Vector3 deltaP = p2 - p1;
            float t =
                Vector3.Dot(Vector3.Cross(deltaP, d2), crossDir) / denominator;
            float s =
                Vector3.Dot(Vector3.Cross(deltaP, d1), crossDir) / denominator;

            // 교점 계산
            intersection = p1 + t * d1;

            // 확인: 두 값이 근사하면 교점이 있다고 봄
            Vector3 pointOnL2 = p2 + s * d2;
            if (Vector3.Distance(intersection, pointOnL2) < 0.01f) {
                return true;
            }
            return false;
        }

        public static float DistanceFromPointToLine(Vector2 lineStart,
            Vector2 lineEnd, Vector2 point) {
            Vector2 lineDirection = (lineEnd - lineStart).normalized;
            Vector2 pointToLineStart = point - lineStart;

            float dotProduct = Vector2.Dot(pointToLineStart, lineDirection);
            Vector2 projection = lineStart + lineDirection * dotProduct;
            Debug.LogWarning(Vector2.Distance(projection, point));

            return Vector2.Distance(projection, point);
        }

        public static void changeRenderMode(Material standardShaderMaterial,
            BlendMode blendMode) {
            switch (blendMode) {
                case BlendMode.Opaque:
                    standardShaderMaterial.SetFloat("_Mode", 0.0f);
                    standardShaderMaterial.SetOverrideTag(
                        "RenderType", "Opaque");
                    standardShaderMaterial.SetInt("_SrcBlend",
                        (int)UnityEngine.Rendering.BlendMode.One);
                    standardShaderMaterial.SetInt("_DstBlend",
                        (int)UnityEngine.Rendering.BlendMode.Zero);
                    standardShaderMaterial.SetInt("_ZWrite", 1);
                    standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                    standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                    standardShaderMaterial.DisableKeyword(
                        "_ALPHAPREMULTIPLY_ON");
                    standardShaderMaterial.renderQueue = -1;
                    break;
                case BlendMode.Cutout:
                    standardShaderMaterial.SetFloat("_Mode", 1.0f);
                    standardShaderMaterial.SetOverrideTag("RenderType",
                        "Opaque");
                    standardShaderMaterial.SetInt("_SrcBlend",
                        (int)UnityEngine.Rendering.BlendMode.One);
                    standardShaderMaterial.SetInt("_DstBlend",
                        (int)UnityEngine.Rendering.BlendMode.Zero);
                    standardShaderMaterial.SetInt("_ZWrite", 1);
                    standardShaderMaterial.EnableKeyword("_ALPHATEST_ON");
                    standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                    standardShaderMaterial.DisableKeyword(
                        "_ALPHAPREMULTIPLY_ON");
                    standardShaderMaterial.renderQueue = 2450;
                    break;
                case BlendMode.Fade:
                    standardShaderMaterial.SetFloat("_Mode", 2.0f);
                    standardShaderMaterial.SetOverrideTag("RenderType",
                        "Transparent");
                    standardShaderMaterial.SetInt("_SrcBlend",
                        (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    standardShaderMaterial.SetInt("_DstBlend",
                        (int)UnityEngine.Rendering.BlendMode.
                        OneMinusSrcAlpha);
                    standardShaderMaterial.SetInt("_ZWrite", 0);
                    standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                    standardShaderMaterial.EnableKeyword("_ALPHABLEND_ON");
                    standardShaderMaterial.DisableKeyword(
                        "_ALPHAPREMULTIPLY_ON");
                    standardShaderMaterial.renderQueue = 3000;
                    break;
                case BlendMode.Transparent:
                    standardShaderMaterial.SetFloat("_Mode", 3.0f);
                    standardShaderMaterial.SetOverrideTag("RenderType",
                        "Transparent");
                    standardShaderMaterial.SetInt("_SrcBlend",
                        (int)UnityEngine.Rendering.BlendMode.One);
                    standardShaderMaterial.SetInt("_DstBlend",
                        (int)UnityEngine.Rendering.BlendMode.
                        OneMinusSrcAlpha);
                    standardShaderMaterial.SetInt("_ZWrite", 0);
                    standardShaderMaterial.DisableKeyword("_ALPHATEST_ON");
                    standardShaderMaterial.DisableKeyword("_ALPHABLEND_ON");
                    standardShaderMaterial.EnableKeyword(
                        "_ALPHAPREMULTIPLY_ON");
                    standardShaderMaterial.renderQueue = 3000;
                    break;
            }
        }
    }
}