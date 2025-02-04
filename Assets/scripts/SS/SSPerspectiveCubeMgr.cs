using UnityEngine;
using SS.AppObject;
using SSAppObject;
using Unity.VisualScripting;

namespace SS {
    public class SSPerspectiveCubeMgr {
        // constants

        //fields
        private SSApp mSS = null;
        private SSPerspectiveCube mPerspectiveCube = null;
        public SSPerspectiveCube getPerspectiveCube() {
            return this.mPerspectiveCube;
        }
        public void setPerspectiveCube(SSPerspectiveCube cube) {
            this.mPerspectiveCube = cube;
        }

        //constructor
        public SSPerspectiveCubeMgr(SSApp ss) {
            this.mSS = ss;
            this.mPerspectiveCube = new SSPerspectiveCube();
        }

        //util functions
        public enum BlendMode {
            Opaque = 0,
            Cutout,
            Fade,
            Transparent
        }

        public void makeGridTransparent() {
            SSApp ss = this.mSS;
            SSGrid grid = ss.getSSPerspectiveCubeMgr().getPerspectiveCube().
                getGrid();
                grid.getGameObject().SetActive(false);
        }

        public void makeGridShow() {
            SSApp ss = this.mSS;
            SSGrid grid = ss.getSSPerspectiveCubeMgr().getPerspectiveCube().
                getGrid();
            grid.getGameObject().SetActive(true);
        }

        public void makeCubeTransparent() {
            SSApp ss = this.mSS;
            GameObject cube = ss.getSSPerspectiveCubeMgr().getPerspectiveCube().
                getCube();
            cube.SetActive(false);
        }

        public void makeCubeShow() {
            SSApp ss = this.mSS;
            GameObject cube = ss.getSSPerspectiveCubeMgr().getPerspectiveCube().
                getCube();
            changeRenderMode(cube.GetComponent<Renderer>().material,
                BlendMode.Opaque);
            cube.SetActive(true);
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
        public void cubeCollideChecker() {
            //perspective cube's world coordinate vertices.
            Vector3 POVController = new Vector3(-0.5f, 0.5f, -0.5f);
            Vector3 FOVController = new Vector3(-0.5f, -0.5f, -0.5f);
            //make a ray from the grid camera.
            Camera cam = this.mSS.getGridCameraPerson().getCamera();
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit)) {
                GameObject cube = this.mPerspectiveCube.getCube();
                Transform cubeTransform = cube.transform;
                Vector3 localHitPoint =
                cubeTransform.InverseTransformPoint(hit.point);
                if (Vector3.Distance(localHitPoint, POVController) < 0.1f) {
                    Debug.Log("Controlling POV");
                    return;
                } else if (
                    Vector3.Distance(localHitPoint, FOVController) < 0.1f) {
                    Debug.Log("Controlling FOV");
                    return;
                }
                Debug.Log("Controlling Rotation");
            }
        }
    }
}
