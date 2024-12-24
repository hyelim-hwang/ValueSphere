using UnityEngine;
using X;
using SS.Scenario;
using SS.AppObject;

namespace SS.Cmd {
    public class SSCmdToMoveEquator : XLoggableCmd {
        //private constructor
        private SSCmdToMoveEquator(XApp app) : base(app) {}

        //static method to construct and execute this command
        public static bool execute(XApp app) {
            SSCmdToMoveEquator cmd = new SSCmdToMoveEquator(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            SSApp app = (SSApp)this.mApp;
            SSSphereHandleScenario scenario =
                (SSSphereHandleScenario)SSSphereHandleScenario.getSingleton();
            SSValueSphere vs = app.getValueSphereMgr().getValueSphere();
            Camera cam = ((SSApp)this.mApp).getPerspCameraPerson().getCamera();

            //rotation needs to be modified by touchmark.
            SSTouchMark tm = scenario.getManipulatingTouchMarks()[0];
            Vector2 prevPt = tm.getRecentPt(1);
            Vector2 curPt = tm.getRecentPt(0);
            if (tm.getPts().Count == 1) {
                return true;
            }

            //get the collision point with collider.
            Ray prevPtRay = cam.ScreenPointToRay(prevPt);
            Ray curPtRay = cam.ScreenPointToRay(curPt);
            if (RayIntersectsSphere(prevPtRay,
            vs.getSphere().transform.position,
            vs.getRadius(), out Vector3 intersection1,
            out Vector3 intersection2)) {
            } else {
                Debug.Log("No intersection");
            }
            if (RayIntersectsSphere(curPtRay, vs.getSphere().transform.position,
                vs.getRadius(), out Vector3 intersection3,
                out Vector3 intersection4)) {
            } else {
                Debug.Log("No intersection");
            }

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

            Vector3 prevDir = intersection1 - vs.getSphere().transform.position;
            Vector3 curDir = intersection3 - vs.getSphere().transform.position;
            Quaternion rot = Quaternion.FromToRotation(prevDir, curDir);
            vs.setRot(rot * vs.getRot());
            return true;
        }

        protected override XJson createLogData() {
            XJson data = new XJson();
            data.addMember("moveEquator", this.GetType().Name);
            return data;
        }
    }
}