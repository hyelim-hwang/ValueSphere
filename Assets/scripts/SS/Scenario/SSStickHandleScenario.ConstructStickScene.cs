using UnityEngine;
using X;
using UnityEngine.InputSystem;
using SS.Cmd;
using SS.Scenario;
using SS.AppObject;

namespace SS.Scenario {
    public partial class SSStickHandleScenario : XScenario {
        public class ConstructStickScene : SSScene {
            //singleton pattern
            private static ConstructStickScene mSingleton = null;
            public static ConstructStickScene getSingleton() {
                Debug.Assert(ConstructStickScene.mSingleton != null);
                return ConstructStickScene.mSingleton;
            }
            public static ConstructStickScene createSingleton(XScenario scenario) {
                Debug.Assert(ConstructStickScene.mSingleton == null);
                ConstructStickScene.mSingleton = new ConstructStickScene(scenario);
                return ConstructStickScene.mSingleton;
            }

            private ConstructStickScene(XScenario scenario) : base(scenario) {}

            //event handling methods
            public override void getReady() {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSShadowStickMgr SSMgr= ss.getShadowStickMgr();
                SSStickHandleScenario scenario =
                    (SSStickHandleScenario)this.mScenario;
                SSCameraPerson cam = ss.getGridCameraPerson();
            }

            public override void handleKeyDown(Key kc) {}

            public override void handleKeyUp(Key kc) {}

            public override void handlePenDown(Vector2 pt) {
            }

            public override void handlePenDrag(Vector2 pt) {}

            public override void handlePenUp(Vector2 pt) {
                SSApp ss = (SSApp)this.mScenario.getApp();
                SSCmdToConstructStick.execute(ss);
                SSCmdToCreateShadowTrace.execute(ss);
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
            }

            public override void handleTouchDrag() {
            }

            public override void handleTouchUp() {
            }

            public override void wrapUp() {}

            //util function
        }
    }
}