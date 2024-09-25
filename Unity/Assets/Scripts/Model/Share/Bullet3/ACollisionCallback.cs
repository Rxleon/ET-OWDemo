using System;
using System.Collections.Generic;
using BulletSharp;

namespace ET
{
    public class CollisionCallbackAttribute : BaseAttribute
    {
        
    }
    
    [CollisionCallback]
    public abstract class ACollisionCallback : HandlerObject
    {
        public abstract void CollisionCallbackEnter(CollisionObject self, CollisionObject other, PersistentManifold persistentManifold);
        public abstract void CollisionCallbackStay(CollisionObject self, CollisionObject other, PersistentManifold persistentManifold);
        public abstract void CollisionCallbackExit(CollisionObject self, CollisionObject other, PersistentManifold persistentManifold);

        public abstract void CollisionTestStart(CollisionObject self);
        public abstract void CollisionTestFinish(CollisionObject self);
    }
}