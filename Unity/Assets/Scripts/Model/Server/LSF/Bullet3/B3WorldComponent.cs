using System.Collections.Generic;
using BulletSharp;

namespace ET.Server
{
    [ComponentOf(typeof(LSWorld))]
    public class B3WorldComponent : LSEntity, IAwake, ILSUpdate, IDestroy
    {
        public DynamicsWorld World;
        
        public Dictionary<CollisionObject, ACollisionCallback> Callbacks = new();
        public Queue<(CollisionObject, ACollisionCallback)> WaitToAdds = new();
        public Queue<CollisionObject> WaitToRemoves = new();

        // 所有有回调的刚体的接触物理列表
        public List<(CollisionObject, CollisionObject, PersistentManifold)> NowCollisionInfos = new();
        // 上一帧 所有有回调的刚体的接触物理列表
        public List<(CollisionObject, CollisionObject, PersistentManifold)> LastCollisionInfos = new();

        public static implicit operator CollisionWorld(B3WorldComponent worldComponent)
        {
            return worldComponent.World;
        }
    }
}