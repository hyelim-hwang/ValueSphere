using System.Collections.Generic;
using UnityEngine;
using X;
using SS.Scenario;
using SS.AppObject;

namespace SS.Cmd {
    public class SSCmdToHandlePassiveHighlight : XLoggableCmd {
        //fields

        //private constructor
        private SSCmdToHandlePassiveHighlight(XApp app) : base(app) {}

        //static method to construct and execute this command
        public static bool execute(XApp app) {
            SSCmdToHandlePassiveHighlight cmd =
            new SSCmdToHandlePassiveHighlight(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            SSSphereHandleScenario scenario =
                (SSSphereHandleScenario)SSSphereHandleScenario.getSingleton();
            SSValueSphereMgr valueSphereMgr =
            ((SSApp)scenario.getApp()).getValueSphereMgr();
            SSApp ss = (SSApp)scenario.getApp();
            SSValueStrokeMgr VSMgr = ss.getValueStrokeMgr();
            SSTouchMark tm = scenario.getManipulatingTouchMarks()[0];
            SSValueSphere vs = valueSphereMgr.getValueSphere();
            Camera cam = ((SSApp)this.mApp).getPerspCameraPerson().getCamera();

            //get previous light direction.
            Vector3 lightPos = ss.getLightSourceMgr().getLightSourcePos();
            Vector3 sphereCenter = vs.getSphere().transform.position;
            float sphereLightDist = Vector3.Distance(lightPos, sphereCenter);

            //assertion fail error
            Vector3 curPt = tm.getRecentPt(0);
            Ray curPtRay = cam.ScreenPointToRay(curPt);
            if (RayIntersectsSphere(curPtRay, vs.getSphere().transform.position,
                vs.getRadius(), out Vector3 intersection1,
                out Vector3 intersection2)) {
            } else {
                // Debug.Log("No intersection");
            }

            //update the light direction.
            Vector3 curPtOnSphere = intersection1;
            Vector3 curPtLightDir = (curPtOnSphere - sphereCenter).normalized;
            Vector3 newLightPos =
            sphereCenter + (curPtLightDir * sphereLightDist);
            ss.getLightSourceMgr().setLightSourcePos(newLightPos);
            ss.getLightSourceMgr().setDirection(newLightPos);

            //turn off the red line, cursor, and sketch.
            ss.getValueSphereMgr().getValueSphere().
            getEquator().getGameObject().SetActive(false);
            ss.getValueSphereMgr().getValueSphere().
            getPole().getGameObject().SetActive(false);
            // ss.getCanvas().SetActive(false);

            //take screenshot.
            SSPerspCameraPerson cp = new SSPerspCameraPerson();
            Camera tempCam = cp.getCamera();
            WaitForEndOfFrame frameEnd = new WaitForEndOfFrame();
            //yield return frameEnd;
            RenderTexture currentRT = RenderTexture.active;
            RenderTexture.active = tempCam.targetTexture;
            RenderTexture tex =
            new RenderTexture(Screen.width, Screen.height, 24);
            Texture2D screenshot =
            new Texture2D(Screen.width, Screen.height);
            tempCam.targetTexture = tex;
            screenshot.ReadPixels(
                new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            screenshot.Apply();
            RenderTexture.active = currentRT;

            //refresh ValueStrokes.
            List<SSValueStroke> VSs = VSMgr.getValueStrokes();
            foreach (SSValueStroke valueStroke in VSs) {
                valueStroke.setValueFromCoordinate(screenshot, cp);
            }

            //destroy screenshot.
            cp.getCameraRig().destroyGameObject();
            Object.Destroy(screenshot);

            //turn on the red line, cursor, and sketch.
            ss.getValueSphereMgr().getValueSphere().getEquator().
            getGameObject().SetActive(true);
            ss.getValueSphereMgr().getValueSphere().getPole().getGameObject().
                SetActive(true);
            // ss.getCanvas().SetActive(true);
            // foreach (SSCursor2D tc in ss.getCursorMgr().getTouchCursors()) {
            //     tc.getGameObject().SetActive(true);
            // }

            //util function
            bool RayIntersectsSphere(Ray ray, Vector3 sphereCenter,
                float sphereRadius, out Vector3 intersection1,
                out Vector3 intersection2) {
                intersection1 = Vector3.zero;
                intersection2 = Vector3.zero;

                Vector3 originToCenter = ray.origin - sphereCenter;
                float a = Vector3.Dot(ray.direction, ray.direction);
                float b = 2f * Vector3.Dot(originToCenter, ray.direction);
                float c = Vector3.Dot(originToCenter, originToCenter) -
                sphereRadius * sphereRadius;

                float discriminant = b * b - 4f * a * c;

                if (discriminant < 0) {
                    // No intersection
                    return false;
                }

                float sqrtDiscriminant = Mathf.Sqrt(discriminant);
                float t1 = (-b - sqrtDiscriminant) / (2f * a);
                float t2 = (-b + sqrtDiscriminant) / (2f * a);

                intersection1 = ray.origin + t1 * ray.direction;
                intersection2 = ray.origin + t2 * ray.direction;

                return true;
            }

            return true;
        }

        protected override XJson createLogData() {
            XJson data = new XJson();
            data.addMember("handlePassiveHighlight", this.GetType().Name);
            return data;
        }
    }
}