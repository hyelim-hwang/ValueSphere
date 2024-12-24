using System;
using X;

namespace SS.Cmd {
    public class SSCmdToUndo : XLoggableCmd {
        // fields
        private DateTime mCurTime;

        // private constructor
        private SSCmdToUndo(XApp app) : base(app) {}

        // static method to construct and execute this command
        public static bool execute(XApp app) {
            SSCmdToUndo cmd = new SSCmdToUndo(app);
            return cmd.execute();
        }

        protected override bool defineCmd() {
            SSApp ss = (SSApp)this.mApp;
            this.mCurTime = DateTime.Now;
            return ss.getSnapshotMgr().undo();
        }

        protected override XJson createLogData() {
            SSApp ss = (SSApp)this.mApp;
            XJson data = new XJson();
            data.addMember("remainingUndoCount", SSCmdToUndo.
                calcRemainingUndoCount(ss));
            data.addMember("remainingRedoCount", SSCmdToUndo.
                calcRemainingRedoCount(ss));
            return data;
        }

        // public methods
        public static int calcRemainingUndoCount(SSApp ss) {
            SSSnapshot snap = ss.getSnapshotMgr().getCurSnapshot();
            int remainingUndoCount = 0;
            while (snap.getPrevSnapshot() != null) {
                remainingUndoCount++;
                snap = snap.getPrevSnapshot();
            }
            return remainingUndoCount;
        }
        public static int calcRemainingRedoCount(SSApp ss) {
            SSSnapshot snap = ss.getSnapshotMgr().getCurSnapshot();
            int remainingRedoCount = 0;
            while (snap.getNextSnapshot() != null) {
                remainingRedoCount++;
                snap = snap.getNextSnapshot();
            }
            return remainingRedoCount;
        }
    }
}
