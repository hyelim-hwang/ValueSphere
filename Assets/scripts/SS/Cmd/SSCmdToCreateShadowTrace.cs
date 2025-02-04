using UnityEngine;
using X;
using SS.Scenario;
using SS.AppObject;
using SSAppObject;
using System.Collections.Generic;

namespace SS.Cmd {
    public class SSCmdToCreateShadowTrace : XLoggableCmd {
        //fields

        //private constructor
        private SSCmdToCreateShadowTrace(XApp app) : base(app) {}

        //static method to construct and execute this command
        public static bool execute(XApp app) {
            SSCmdToCreateShadowTrace cmd = new SSCmdToCreateShadowTrace(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            SSApp ss = (SSApp)this.mApp;
            SSStickHandleScenario scenario =
                (SSStickHandleScenario)SSStickHandleScenario.getSingleton();
            SSStick stick = ((SSApp)scenario.getApp()).getShadowStickMgr().
                getShadowStick();
            SSShadowStickMgr SSMgr = ss.getShadowStickMgr();
            SSShadowTraceMgr STMgr = ss.getShadowTraceMgr();
            List<SSShadowTrace> traces = STMgr.getShadowTraces();

            //create new shadowTrace.
            Vector3 globalShadowTop =
                STMgr.transformLocalToWorldByBOS(stick.getShadowTop());
            Vector3 globalShadowBottom =
                STMgr.transformLocalToWorldByBOS(stick.getShadowBottom());

            SSShadowTrace shadowTrace =
                new SSShadowTrace("trace", globalShadowTop, globalShadowBottom);

            //add shadowTrace to shadowTrace list.
            traces.Add(shadowTrace);
            Debug.LogWarning("traceCount" + traces.Count);
            if (traces.Count > 1) {
                //STMgr.createShadowFacesByShadowTraces();
            }
            return true;
        }

        protected override XJson createLogData() {
            XJson data = new XJson();
            data.addMember("createShadowTrace", this.GetType().Name);
            return data;
        }
    }
}