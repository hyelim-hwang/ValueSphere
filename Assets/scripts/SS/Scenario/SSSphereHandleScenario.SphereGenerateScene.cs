using UnityEngine;
using X;
using UnityEngine.InputSystem;
using SS.AppObject;
using SS.Cmd;

namespace SS.Scenario {
    public partial class SSSphereHandleScenario : XScenario {
        public class SphereGenerateScene : SSScene {
            //singleton pattern
            private static SphereGenerateScene mSingleton = null;
            public static SphereGenerateScene getSingleton() {
                Debug.Assert(SphereGenerateScene.mSingleton != null);
                return SphereGenerateScene.mSingleton;
            }
            public static SphereGenerateScene createSingleton(
                XScenario scenario) {
                Debug.Assert(SphereGenerateScene.mSingleton == null);
                SphereGenerateScene.mSingleton =
                    new SphereGenerateScene(scenario);
                return SphereGenerateScene.mSingleton;
            }

            private SphereGenerateScene(XScenario scenario) : base(scenario) {}

            //event handling methods
            public override void getReady() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSSphereHandleScenario scenario =
                    (SSSphereHandleScenario)this.mScenario;
                SSValueSphere vs =
                    ((SSApp)scenario.getApp()).getValueSphereMgr().
                getValueSphere();
                if (ss.getTouchMarkMgr().wasTouchDownJustNow()) {
                    SSTouchMark tm =
                        ss.getTouchMarkMgr().getLastDownTouchMark();
                    scenario.getManipulatingTouchMarks().Add(tm);
                }
                ((SSApp)scenario.getApp()).getValueSphereMgr().setValueSphere(
                    new SSValueSphere("valueSphere", 0f, Color.gray));
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
                //throw new System.NotImplementedException();
            }

            public override void handleTouchDrag() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSCmdToGenerateSphere.execute(ss);
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
                    SSSphereHandleScenario.SphereGenerateReadyScene.
                    getSingleton(),
                    null);
            }

            public override void wrapUp() {}
        }
    }
}