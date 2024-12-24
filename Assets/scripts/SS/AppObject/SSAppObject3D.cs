namespace SS.AppObject
{
    public abstract class SSAppObject3D : SSAppObject
    {
        public SSAppObject3D (string name) : base (name)
        {
            this.mGameObject.layer = 0;
        }
    }
}