using UnityEngine;
using X;
using SS.Scenario;
using SS.AppObject;
using SSAppObject;
using System.Collections.Generic;

namespace SS.Cmd {
    public class SSCmdToConstructStick : XLoggableCmd {
        //fields

        //private constructor
        private SSCmdToConstructStick(XApp app) : base(app) {}

        //static method to construct and execute this command
        public static bool execute(XApp app) {
            SSCmdToConstructStick cmd = new SSCmdToConstructStick(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            SSApp ss = (SSApp)this.mApp;
            SSStickHandleScenario scenario =
                (SSStickHandleScenario)SSStickHandleScenario.getSingleton();
            SSStick stick = ((SSApp)scenario.getApp()).getShadowStickMgr().
                getShadowStick();
            List<SSStick> sticks = ((SSApp)scenario.getApp()).getShadowStickMgr().
                getSticks();
            //create new stick to the position
            Vector3 globalPosOfStick = stick.getGameObject().transform.position;
            SSStick constructedStick = new SSStick("constuctedStick",
            ss.getShadowStickMgr().getPlane(), stick.getLightVector());

            constructedStick.setEdgeTop(stick.getEdgeTop());
            constructedStick.setEdgeBottom(stick.getEdgeBottom());
            constructedStick.updateWidgetWithChangedPoints();

            //make constructed stick more transparent.
            makeStickTransparent(constructedStick);
            constructedStick.getGameObject().transform.position =
                globalPosOfStick;

            //shadowTrace
            Vector3 globalShadowTop =
                this.transformLocalToWorldByBOS(constructedStick.getShadowTop());
            Vector3 globalShadowBottom =
                this.transformLocalToWorldByBOS(constructedStick.getShadowBottom());

            SSShadowTrace shadowTrace =
                new SSShadowTrace("trace", globalShadowTop, globalShadowBottom);
            constructedStick.setShadowTrace(shadowTrace);
            constructedStick.addChild(constructedStick.getShadowTrace());
            sticks.Add(constructedStick);

            return true;
        }

        protected override XJson createLogData() {
            XJson data = new XJson();
            data.addMember("cmdToConstructStick", this.GetType().Name);
            return data;
        }

        private void makeStickTransparent(SSStick stick) {
            SSAppTrapezoid3D face = stick.getFace();
            SSAppPolyline3D topLightDir = stick.getTopLightDirection();
            SSAppPolyline3D bottomLightDir = stick.getBottomLightDirection();
            SSAppPolyline3D verticalStick = stick.getVerticalStick();
            SSAppPolyline3D shadowDir = stick.getShadowDirection();
            Material faceMat = face.getGameObject().
                GetComponent<MeshRenderer>().material;
            faceMat.SetFloat("_Mode", 3);
            faceMat.SetInt("_SrcBlend",
                (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            faceMat.SetInt("_DstBlend",
                (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            faceMat.SetInt("_ZWrite", 0);
            faceMat.DisableKeyword("_ALPHATEST_ON");
            faceMat.EnableKeyword("_ALPHABLEND_ON");
            faceMat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            faceMat.renderQueue = 3000;
            // Alpha 값 적용
            Color color = faceMat.color;
            color.a =  0.1f;
            faceMat.color = color;
            makeLineTransparent(topLightDir);
            makeLineTransparent(bottomLightDir);
            makeLineTransparent(verticalStick);
            makeLineTransparent(shadowDir);
        }
        private void makeLineTransparent(SSAppPolyline3D line) {
            Material faceMat = line.getGameObject().
                GetComponent<LineRenderer>().material;
            LineRenderer lineRenderer =
                line.getGameObject().GetComponent<LineRenderer>();
            // faceMat.SetFloat("_Mode", 3);
            // faceMat.SetInt("_SrcBlend",
            //     (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            // faceMat.SetInt("_DstBlend",
            //     (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            // faceMat.SetInt("_ZWrite", 0);
            // faceMat.DisableKeyword("_ALPHATEST_ON");
            // faceMat.EnableKeyword("_ALPHABLEND_ON");
            // faceMat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            faceMat.renderQueue = 3000;
            // Alpha 값 적용
            Color color = faceMat.color;
            color.a =  0.5f;
            Color startColor = lineRenderer.startColor;
            startColor.a = 0.5f; // Alpha 값 적용
            lineRenderer.startColor = startColor;
            Color endColor = lineRenderer.endColor;
            endColor.a = 0.5f; // Alpha 값 적용
            lineRenderer.endColor = endColor;
        }
        public Vector3 transformLocalToWorldByBOS(Vector3 local) {
            Vector3 baseOfStick =
                ((SSApp)this.mApp).getShadowStickMgr().getShadowStick().getBaseOfStick();
            return local + baseOfStick;
        }
    }
}