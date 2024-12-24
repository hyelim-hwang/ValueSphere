using UnityEngine;

public class SSTouchPacket {
    //fields
    private readonly string mId;
    public string getId() {
        return this.mId;
    }
    private readonly Vector2 mPt;
    public Vector2 getPt() {
        return this.mPt;
    }

    //constructor
    public SSTouchPacket(string id, Vector2 pt) {
        this.mId = id;
        this.mPt = pt;
    }
}