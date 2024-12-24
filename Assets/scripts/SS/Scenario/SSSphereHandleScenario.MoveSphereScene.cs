using UnityEngine;
using X;
using UnityEngine.InputSystem;
using SS.Cmd;
using SS.Scenario;
using SS.AppObject;

namespace SS.Scenario {
    public partial class SSSphereHandleScenario : XScenario {
        public class MoveSphereScene : SSScene {
            //singleton pattern
            private static MoveSphereScene mSingleton = null;
            public static MoveSphereScene getSingleton() {
                Debug.Assert(MoveSphereScene.mSingleton != null);
                return MoveSphereScene.mSingleton;
            }
            public static MoveSphereScene createSingleton(XScenario scenario) {
                Debug.Assert(MoveSphereScene.mSingleton == null);
                MoveSphereScene.mSingleton = new MoveSphereScene(scenario);
                return MoveSphereScene.mSingleton;
            }

            private MoveSphereScene(XScenario scenario) : base(scenario) {}

            //event handling methods
            public override void getReady() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSSphereHandleScenario scenario =
                    (SSSphereHandleScenario)this.mScenario;
                SSCameraPerson cam = ss.getPerspCameraPerson();
                if (ss.getTouchMarkMgr().wasTouchDownJustNow()) {
                    SSTouchMark tm =
                    ss.getTouchMarkMgr().getLastDownTouchMark();
                    scenario.getManipulatingTouchMarks().Add(tm);
                }

                //if the touch area is the highlight, move highlight.
                //take a screenshot
                if (isHighlight(cam)) {
                    XCmdToChangeScene.execute(ss,
                    SSSphereHandleScenario.HandleHighlightScene.getSingleton(),
                    this.mReturnScene);
                }

                //snapshot function
                bool isHighlight(SSCameraPerson cam) {
                    SSSphereHandleScenario scenario =
                        (SSSphereHandleScenario)SSSphereHandleScenario.getSingleton();
                    SSValueSphereMgr valueSphereMgr =
                        ((SSApp)scenario.getApp()).getValueSphereMgr();
                    SSApp ss = (SSApp)scenario.getApp();
                    SSValueStrokeMgr VSMgr = ss.getValueStrokeMgr();
                    SSTouchMark tm = scenario.getManipulatingTouchMarks()[0];
                    SSValueSphere vs = valueSphereMgr.getValueSphere();

                    //get previous light direction.
                    Vector3 lightPos =
                        ss.getLightSourceMgr().getLightSourcePos();
                    Vector3 sphereCenter = vs.getSphere().transform.position;
                    float sphereLightDist =
                        Vector3.Distance(lightPos, sphereCenter);

                    //assertion fail error
                    Vector3 curPt = tm.getRecentPt(0);
                    Ray curPtRay = cam.getCamera().ScreenPointToRay(curPt);
                    Ray curLightRay =
                        new Ray(lightPos, sphereCenter - lightPos);
                    if (RayIntersectsSphere(curPtRay,
                        vs.getSphere().transform.position,
                        vs.getRadius(), out Vector3 curPtIntersection1,
                        out Vector3 curPtIntersection2)) {
                        Vector3 curPtOnSphere = curPtIntersection1;
                        RayIntersectsSphere(curLightRay,
                        vs.getSphere().transform.position,
                        vs.getRadius(), out Vector3 lightIntersection1,
                        out Vector3 lightIntersection2);
                        //convert two world vectors to screen vectors.
                        Vector2 lightIntersection1InScreen = cam.getCamera().
                            WorldToScreenPoint(lightIntersection1);
                        Vector2  curPtIntersection1InScreen = cam.getCamera().
                            WorldToScreenPoint(curPtIntersection1);
                        float dist = Vector3.Distance(
                            lightIntersection1InScreen,
                            curPtIntersection1InScreen);
                        Debug.LogWarning(dist);
                        float sphereRadius = vs.getRadius();
                        //Debug.LogWarning(sphereRadius);
                        if (dist < sphereRadius * 320) {
                            return true;
                        } else {
                            return false;
                        }
                    } else {
                        // Debug.Log("No intersection");
                    }
                    return false;
                }
            }

            public override void handleKeyDown(Key kc) {}

            public override void handleKeyUp(Key kc) {}

            public override void handlePenDown(Vector2 pt) {
                // //enter equator modify scene by touch
                // SSApp ss = (SSApp)this.mScenario.getApp();
                // XCmdToChangeScene.execute(ss,
                //         SSSphereHandleScenario.MoveEquatorScene.getSingleton(),
                //         this.mReturnScene);
            }

            public override void handlePenDrag(Vector2 pt) {}

            public override void handlePenUp(Vector2 pt) {}

            public override void handleEraserDown(Vector2 pt) {
                //throw new System.NotImplementedException();
            }

            public override void handleEraserDrag(Vector2 pt) {
                //throw new System.NotImplementedException();
            }

            public override void handleEraserUp(Vector2 pt) {
                //throw new System.NotImplementedException();
            }

            public override void handleTouchDown() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                XCmdToChangeScene.execute(ss,
                    SSSphereHandleScenario.SphereMoveNScaleScene.getSingleton(),
                    this.mReturnScene);
            }

            public override void handleTouchDrag() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSCmdToMoveEquator.execute(ss);

            }

            public override void handleTouchUp() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSSphereHandleScenario scenario =
                    (SSSphereHandleScenario)this.mScenario;
                SSTouchMark tm = ss.getTouchMarkMgr().getLastUpTouchMark();
                if (scenario.getManipulatingTouchMarks().Contains(tm)) {
                    scenario.getManipulatingTouchMarks().Remove(tm);
                    XCmdToChangeScene.execute(ss,
                    SSDefaultScenario.ReadyScene.getSingleton(), null);
                }
            }

            public override void wrapUp() {}

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
        }
    }
}