using UnityEngine;
using X;
using SS.Cmd;
using UnityEngine.InputSystem;
using SS.AppObject;

namespace SS.Scenario {
    public partial class SSDrawScenario : XScenario {
        public class DrawScene : SSScene {
            //singleton pattern
            private static DrawScene mSingleton = null;
            public static DrawScene getSingleton() {
                Debug.Assert(DrawScene.mSingleton != null);
                return DrawScene.mSingleton;
            }
            public static DrawScene createSingleton(XScenario scenario) {
                Debug.Assert(DrawScene.mSingleton == null);
                DrawScene.mSingleton = new DrawScene(scenario);
                return DrawScene.mSingleton;
            }

            private DrawScene(XScenario scenario) : base(scenario) {}

            //event handling methods
            public override void getReady() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSCmdToCreateCurPtCurve2D.execute(ss);
                SSValueSphere vs = ss.getValueSphereMgr().getValueSphere();
                SSValueSphereMgr valueSphereMgr = ss.getValueSphereMgr();
                //make sphere transparent while drawing strokes.
                if (vs != null) {
                    valueSphereMgr.makeSphereTransparent();
                }
            }

            public override void handleKeyDown(Key kc) {}

            public override void handleKeyUp(Key kc) {}

            public override void handlePenDown(Vector2 pt) {}

            public override void handlePenDrag(Vector2 pt) {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSCmdToUpdateCurPtCurve2D.execute(ss);
            }

            public override void handlePenUp(Vector2 pt) {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSCmdToAddCurValueStrokeToValueStroke.execute(ss);
                SSCmdToTakeSnapshot.execute(ss);
                SSValueSphere vs = ss.getValueSphereMgr().getValueSphere();
                SSValueSphereMgr valueSphereMgr = ss.getValueSphereMgr();
                if (vs != null) {
                    valueSphereMgr.makeSphereShow();
                }
                XCmdToChangeScene.execute(ss, this.mReturnScene, null);
            }

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
                //throw new System.NotImplementedException();
            }

            public override void handleTouchDrag() {
                //throw new System.NotImplementedException();
            }

            public override void handleTouchUp() {
                //throw new System.NotImplementedException();
            }

            public override void wrapUp() {}
        }
    }
}