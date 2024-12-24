using UnityEngine;
using X;
using UnityEngine.InputSystem;
using SS.AppObject;

namespace SS.Scenario {
    public partial class SSSphereHandleScenario: XScenario {
        public class SphereGenerateReadyScene : SSScene {
            //singleton pattern
            private static SphereGenerateReadyScene mSingleton = null;
            public static SphereGenerateReadyScene getSingleton() {
                Debug.Assert(SphereGenerateReadyScene.mSingleton != null);
                return SphereGenerateReadyScene.mSingleton;
            }
            public static SphereGenerateReadyScene createSingleton(
                XScenario scenario) {
                Debug.Assert(SphereGenerateReadyScene.mSingleton == null);
                SphereGenerateReadyScene.mSingleton =
                    new SphereGenerateReadyScene(scenario);
                return SphereGenerateReadyScene.mSingleton;
            }

            private SphereGenerateReadyScene(
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
            }

            public override void handleKeyDown(Key kc) {}

            public override void handleKeyUp(Key kc) {}

            public override void handlePenDown(Vector2 pt) {}

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
                SSSphereHandleScenario scenario =
                    (SSSphereHandleScenario)this.mScenario;
                SSValueSphere vs =
                    ((SSApp)scenario.getApp()).getValueSphereMgr().
                    getValueSphere();
                if (vs == null) {
                    XCmdToChangeScene.execute(ss,
                        SSSphereHandleScenario.SphereGenerateScene.
                        getSingleton(), this);
                } else {
                    XCmdToChangeScene.execute(ss,
                        SSSphereHandleScenario.SphereMoveNScaleScene.
                        getSingleton(),
                        this);
                }
            }

            public override void handleTouchDrag() {
                //throw new System.NotImplementedException();
            }

            public override void handleTouchUp() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSSphereHandleScenario scenario =
                    (SSSphereHandleScenario)this.mScenario;
                SSTouchMark tm = ss.getTouchMarkMgr().getLastUpTouchMark();
                if (scenario.getManipulatingTouchMarks().Contains(tm)) {
                    scenario.getManipulatingTouchMarks().Remove(tm);
                }
                XCmdToChangeScene.execute(ss,
                    SSDefaultScenario.ReadyScene.getSingleton(),
                    null);
            }

            public override void wrapUp() {}
        }
    }
}