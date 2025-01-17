﻿using System.Collections.Generic;
using UnityEngine;

namespace X {
    public class XLogMgr {
        //field
        private List<XLog> mLogs = null;
        public List<XLog> getLogs() {
            return this.mLogs;
        }

        private bool mPrintOn = true;
        public bool getPrintOn() {
            return this.mPrintOn;
        }
        public void setPrintOn(bool isPrintOn) {
            this.mPrintOn = isPrintOn;
        }

        //constructor
        public XLogMgr() {
            this.mLogs = new List<XLog>();
        }

        //method
        public void addLog(XLog log) {
            this.mLogs.Add(log);
            if (this.mPrintOn) {
                Debug.Log(log);
            }
        }
    }
}

