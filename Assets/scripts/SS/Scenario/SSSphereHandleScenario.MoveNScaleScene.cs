using UnityEngine;
using X;
using UnityEngine.InputSystem;
using SS.Cmd;

namespace SS.Scenario {
    public partial class SSSphereHandleScenario : XScenario {
        public class SphereMoveNScaleScene : SSScene {
            //singleton pattern
            private static SphereMoveNScaleScene mSingleton = null;
            public static SphereMoveNScaleScene getSingleton() {
                Debug.Assert(SphereMoveNScaleScene.mSingleton != null);
                return SphereMoveNScaleScene.mSingleton;
            }
            public static SphereMoveNScaleScene createSingleton(
                XScenario scenario) {
                Debug.Assert(SphereMoveNScaleScene.mSingleton == null);
                SphereMoveNScaleScene.mSingleton =
                    new SphereMoveNScaleScene(scenario);
                return SphereMoveNScaleScene.mSingleton;
            }

            private SphereMoveNScaleScene(
                XScenario scenario) : base(scenario) {}

            //event handling methods
            public override void getReady() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSSphereHandleScenario scenario =
                    (SSSphereHandleScenario)this.mScenario;
                if (ss.getTouchMarkMgr().wasTouchDownJustNow()) {
                    SSTouchMark tm =
                    ss.getTouchMarkMgr().getLastDownTouchMark();
                    scenario.getManipulatingTouchMarks().Add(tm);
                }
                Debug.Assert(scenario.getManipulatingTouchMarks().Count >= 2);
            }

            public override void handleKeyDown(Key kc) {}

            public override void handleKeyUp(Key kc) {}

            public override void handlePenDown(Vector2 pt) {
                //enter equator modify scene by touch
                SSApp ss = (SSApp)this.mScenario.getApp();
                // XCmdToChangeScene.execute(ss,
                //     SSSphereHandleScenario.MoveEquatorScene.getSingleton(),
                //     null);
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
               Debug.LogWarning("another touch detected!!");
               SSSphereHandleScenario scenario =
                    (SSSphereHandleScenario)this.mScenario;
            }

            public override void handleTouchDrag() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSCmdToScaleValueSphere.execute(ss);
            }

            public override void handleTouchUp() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSSphereHandleScenario scenario =
                    (SSSphereHandleScenario)this.mScenario;
                SSTouchMark tm = ss.getTouchMarkMgr().getLastUpTouchMark();
                if (scenario.getManipulatingTouchMarks().Contains(tm)) {
                    scenario.getManipulatingTouchMarks().Remove(tm);
                    XCmdToChangeScene.execute(ss,
                        SSSphereHandleScenario.MoveSphereScene.getSingleton(),
                        null);
                } else {}
            }

            public override void wrapUp() {}
        }
    }
}