using UnityEngine;
using SS.AppObject;
using SSAppObject;
using UnityEngine.UIElements;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Data.Common;

namespace SS {
    public class SSShadowStickMgr {
        // constants
        public static readonly int DEFAULT_NUM_CURVE_SEGS = 20;
        public static readonly float DEFAULT_TOP_HEIGHT = 0.5f;
        public static readonly float DEFAULT_BOTTOM_HEIGHT = 0.3f;
        public static readonly Quaternion DEFAULT_ANGLE = Quaternion.identity;
        public static readonly Color DEFAULT_COLOR = Color.gray;
        public static readonly float DEFAULT_WIDTH = 0.01f;
        public static readonly Vector3 DEFAULT_POS = Vector3.zero;
        public static readonly float TRANSLTE_BOUNDARY_RADIUS = 30;
        public static readonly float SHADOW_BOUNDARY_RADIUS = 100;
        public static readonly float LIGHT_SOURCE_RADIUS = 0.1f;
        public static readonly Vector3 CYLINDER_LIGHT_DIR =
            new Vector3(-0.8f, -1f, 0.4f);
        public static readonly Vector3 CUBE_LIGHT_DIR =
            new Vector3(-0.6f, -1f, -0.8f);
        public static readonly Vector3 FLOATING_CYLINDER_LIGHT_DIR =
            new Vector3(0.6f, -1f, -0.8f);
        public static readonly Vector3 CUBE_FLOOR_DIR =
            new Vector3(0.6f, -1f, 0.8f);
        public static readonly Vector3 CONE_FLOOR_DIR =
            new Vector3(-0.7f, -1f, -1.3f);
        public static readonly Vector3 DEFAULT_LIGHT_POS =
            new Vector3(-500f, 500f, -500f);


        //fields
        private SSApp mSS = null;
        //stick
        private SSStick mShadowStick= null;
        public SSStick getShadowStick() {
            return this.mShadowStick;
        }
        public void setShadowStick(SSStick stick) {
            this.mShadowStick = stick;
        }
        private List<SSStick> mSticks = new List<SSStick>();
        public List<SSStick> getSticks() {
            return this.mSticks;
        }
        public void setSticks(List<SSStick> sticks) {
            this.mSticks = sticks;
        }
        private List<int> mManipulatingSticks = new List<int>();
        public List<int> getManipulatingSticks() {
            return this.mManipulatingSticks;
        }
        public void setManipulatingSticks(List<int> manipulatingSticks) {
            this.mManipulatingSticks = manipulatingSticks;
        }
        public int mIndexFlag = 0;
        GameObject mLightSourceCirlce = null;

        //light direction & vector
        private Vector3 mLightPosition = Vector3.zero;
        public Vector3 getLightPosition() {
            return this.mLightPosition;
        }
        public void setLightPosition(Vector3 lightPos) {
            this.mLightPosition = lightPos;
            //need to calculate light direction again.
            //need to be in global pos.
            Vector3 dir = this.mShadowStick.calcStickComponentPositionInWorld(
                this.mShadowStick.getEdgeTop())+ - lightPos;
            this.mShadowStick.setLightDirection(dir);
            this.setLightDirection(dir);
            foreach(SSStick stick in this.mSticks) {
                Vector3 lightDir =
                    stick.calcStickComponentPositionInWorld(stick.getEdgeTop()) -
                    lightPos;
                stick.setLightDirection(lightDir.normalized);
                stick.makeStickTransparent(stick);
            }
            //draw the light source on the canvas.
            this.mLightSourceCirlce.transform.position = lightPos;

        }
        public void updateLightDirectionByLightPosition() {
            //this.mShadowStick.setLightDirection();
        }
        private Vector3 mLightDirection = Vector3.zero;
        public Vector3 getLightDirection() {
            return this.mLightDirection;
        }
        public void setLightDirection(Vector3 dir) {
            this.mLightDirection = dir;
            this.mShadowStick.setLightDirection(dir);
            foreach(SSStick stick in this.mSticks) {
                stick.setLightDirection(dir);
                stick.makeStickTransparent(stick);
            }
        }
        //plane
        private Plane mPlane = new Plane();
        public Plane getPlane() {
            return this.mPlane;
        }
        public void setPlane(Plane plane) {
            this.mPlane = plane;
        }

        //constructor
        public SSShadowStickMgr(SSApp ss) {
            this.mSS = ss;
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            this.setPlane(plane);
            SSStick stick =
                new SSStick("stick", plane, SSShadowStickMgr.CUBE_LIGHT_DIR);
            this.setShadowStick(stick);
            //hide the stick first.
            this.mLightSourceCirlce =
                GameObject.CreatePrimitive(PrimitiveType.Sphere);
            this.mLightSourceCirlce.transform.localScale = 0.1f * Vector3.one;
            this.mLightSourceCirlce.GetComponent<MeshRenderer>().
                material.color = Color.yellow;
            this.mLightSourceCirlce.layer = 3;
            this.setLightPosition(SSShadowStickMgr.DEFAULT_LIGHT_POS);
            stick.getGameObject().SetActive(false);
            // this.mLightDirection = SSShadowStickMgr.CONE_FLOOR_DIR;
        }

        //util functions
        public enum BlendMode {
            Opaque = 0,
            Cutout,
            Fade,
            Transparent
        }

        public int stickColliderChecker(Vector3 touchedScreenPt) {
            SSApp ss = (SSApp)this.mSS;
            SSStick stick = this.getShadowStick();
            Camera cam = ss.getGridCameraPerson().getCamera();
            Vector2 baseOfStickInScreenPt =
                cam.WorldToScreenPoint(stick.getBaseOfStick());
            Vector2 edgeTopInScreenPt =
                cam.WorldToScreenPoint(stick.getGameObject().
                transform.TransformPoint(stick.getEdgeTop()));
            Vector2 edgeBottomInScreenPt =
                cam.WorldToScreenPoint(stick.getGameObject().
                transform.TransformPoint(stick.getEdgeBottom()));
            Vector3 shadowMiddle =
                (stick.getShadowBottom() + stick.getShadowTop()) / 2;
            Vector2 shadowMiddleInScreenPt =
                cam.WorldToScreenPoint(stick.getGameObject().
                transform.TransformPoint(shadowMiddle));
            float baseDistance =
                Vector2.Distance(touchedScreenPt, baseOfStickInScreenPt);
            float edgeTopDistance =
                Vector2.Distance(touchedScreenPt, edgeTopInScreenPt);
            float edgeBottomDistance =
                Vector2.Distance(touchedScreenPt, edgeBottomInScreenPt);
            float shadowDistance =
                Vector2.Distance(touchedScreenPt, shadowMiddleInScreenPt);
            bool isRayCollidedLightPlane =
                SSUtil.hits(cam, stick.getFace(), touchedScreenPt);
            bool isRayCollidedConstructedStickShadow = false;
            Vector2 constructedStickShadowMiddleInScreenPt = Vector2.zero;
            List<SSStick> sticks = this.getSticks();
            int flag = 0;
            foreach(SSStick constructedStick in sticks) {
                Vector3 constructedStickShadowMiddle =
                    (constructedStick.getShadowBottom() +
                    constructedStick.getShadowTop()) / 2;
                constructedStickShadowMiddleInScreenPt =
                    cam.WorldToScreenPoint(constructedStick.getGameObject().
                    transform.TransformPoint(constructedStickShadowMiddle));
                float constructedStickShadowDistance =
                    Vector2.Distance(touchedScreenPt,
                    constructedStickShadowMiddleInScreenPt);
                if (constructedStickShadowDistance <
                    SSShadowStickMgr.SHADOW_BOUNDARY_RADIUS) {
                        //add cur stick in manipulating sticks.
                        this.mManipulatingSticks.Add(flag);
                    return 3;
                }
                flag++;
            }

            if (baseDistance < SSShadowStickMgr.TRANSLTE_BOUNDARY_RADIUS) {
                return 0;
            } else if (
                edgeTopDistance < SSShadowStickMgr.TRANSLTE_BOUNDARY_RADIUS) {
                return 1;
            } else if (
                edgeBottomDistance < SSShadowStickMgr.TRANSLTE_BOUNDARY_RADIUS) {
                return 2;
            // } else if (
            //     shadowDistance < SSShadowStickMgr.SHADOW_BOUNDARY_RADIUS) {
            //     return 3;
            } else if (isRayCollidedLightPlane) {
                return 4;
            } else {
                return 5;
            }
        }

        public static Vector3 calculateLocalLightPosByConstructedSticks(
            SSStick stick1, SSStick stick2, Vector3 shadowTop1,
            Vector3 shadowTop2) {
            Vector3 edgeTop1 = stick1.getEdgeTop() +
                stick1.getGameObject().transform.position;
            Vector3 reversedLightDir1 =
                (edgeTop1 - shadowTop1).normalized;
            Vector3 edgeTop2 = stick2.getEdgeTop() +
                stick2.getGameObject().transform.position;
            Vector3 reversedLightDir2 =
                (edgeTop2 - shadowTop2).normalized;
            SSUtil.FindLineIntersection(edgeTop1, reversedLightDir1, edgeTop2,
                reversedLightDir2, out Vector3 intersection);
            //intersection masking
            if (intersection.magnitude > 1000f) {
                intersection = 1000f * intersection.normalized;
            } else if (intersection == Vector3.positiveInfinity ||
                intersection == Vector3.positiveInfinity) {
                intersection = 1000f * intersection.normalized;
            }
            // Debug.LogError("edgeTop1" + edgeTop1);
            // Debug.LogError("edgeTop2" + edgeTop2);
            // Debug.LogError("reversedLightDir1" + reversedLightDir1);
            // Debug.LogError("reversedLightDir2" + reversedLightDir2);

            return intersection;

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
