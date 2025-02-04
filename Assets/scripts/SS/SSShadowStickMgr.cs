using UnityEngine;
using SS.AppObject;
using SSAppObject;
using UnityEngine.UIElements;
using System.Collections.Generic;

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


        //fields
        private SSApp mSS = null;
        private SSStick mShadowStick= null;
        public SSStick getShadowStick() {
            return this.mShadowStick;
        }
        public void setShadowStick(SSStick stick) {
            this.mShadowStick = stick;
        }
        //position and rotation
        private Vector3 mPosition = Vector3.zero;
        public Vector3 getPosition() {
            return this.mPosition;
        }
        public void setPosition(Vector3 pos) {
            this.mPosition = pos;
        }
        private Quaternion mRotation = Quaternion.identity;
        public Quaternion getRotation() {
            return this.mRotation;
        }
        public void setRotation(Quaternion rot) {
            this.mRotation = rot;
        }
        //top height(longer one) and bottom height(shorter one)
        private float mTopHeight = float.NaN;
        public float getTopHeight() {
            return this.mTopHeight;
        }
        public void setTopHeight(float topHeight) {
            this.mTopHeight = topHeight;
        }
        private float mBottomHeight = float.NaN;
        public float getBottomHeight() {
            return this.mBottomHeight;
        }
        public void setBottomHeight(float bottomHeight) {
            this.mBottomHeight = bottomHeight;
        }
        private Plane mPlane = new Plane();
        public Plane getPlane() {
            return this.mPlane;
        }
        public void setPlane(Plane plane) {
            this.mPlane = plane;
        }
        private List<SSStick> mSticks = new List<SSStick>();
        public List<SSStick> getSticks() {
            return this.mSticks;
        }
        public void setSticks(List<SSStick> sticks) {
            this.mSticks = sticks;
        }


        //constructor
        public SSShadowStickMgr(SSApp ss) {
            this.mSS = ss;
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
            Vector2 baseOfStickInScreenPt = ss.getGridCameraPerson().
                getCamera().WorldToScreenPoint(stick.getBaseOfStick());
            Vector2 edgeTopInScreenPt = ss.getGridCameraPerson().
                getCamera().WorldToScreenPoint(stick.getGameObject().
                transform.TransformPoint(stick.getEdgeTop()));
            Vector2 edgeBottomInScreenPt = ss.getGridCameraPerson().
                getCamera().WorldToScreenPoint(stick.getGameObject().
                transform.TransformPoint(stick.getEdgeBottom()));
            float baseDistance =
                Vector2.Distance(touchedScreenPt, baseOfStickInScreenPt);
            float edgeTopDistance =
                Vector2.Distance(touchedScreenPt, edgeTopInScreenPt);
            float edgeBottomDistance =
                Vector2.Distance(touchedScreenPt, edgeBottomInScreenPt);
            if (baseDistance < SSShadowStickMgr.TRANSLTE_BOUNDARY_RADIUS) {
                return 0;
            } else if (
                    edgeTopDistance < SSShadowStickMgr.TRANSLTE_BOUNDARY_RADIUS) {
                return 1;
            } else if (
                edgeBottomDistance < SSShadowStickMgr.TRANSLTE_BOUNDARY_RADIUS) {
                return 2;
            } else {
                return 100;
            }

        }

        public void makeStickTransparent() {
            SSApp ss = this.mSS;
            SSStick stick = ss.getShadowStickMgr().getShadowStick();
            // changeRenderMode(
            //     stick.getVerticalStick().GetComponent<Renderer>().material,
            //     BlendMode.Transparent);
            // Color color =
            //     vs.getSphere().GetComponent<Renderer>().material.color;
            // color.a = 0.5f;
            // vs.getSphere().GetComponent<Renderer>().material.color = color;
        }

        public void makeStickShow() {
            // SSApp ss = this.mSS;
            // SSValueSphere vs = ss.getValueSphereMgr().getValueSphere();
            // changeRenderMode(
            //     vs.getSphere().GetComponent<Renderer>().material,
            //     BlendMode.Opaque);
            // Color color =
            // vs.getSphere().GetComponent<Renderer>().material.color;
            // color.a = 0.5f;
            // vs.getSphere().GetComponent<Renderer>().material.color = color;
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
