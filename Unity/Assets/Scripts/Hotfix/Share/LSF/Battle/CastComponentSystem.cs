using System.Numerics;
using BulletSharp;
using BulletSharp.Math;
using TrueSync;

namespace ET
{
    [EntitySystemOf(typeof(CastComponent))]
    [FriendOf(typeof(CastComponent))]
    [FriendOfAttribute(typeof(ET.Cast))]
    [FriendOfAttribute(typeof(ET.LSUnit))]
    public static partial class CastComponentSystem
    {
        [EntitySystem]
        private static void Awake(this CastComponent self)
        {
            
        }

        public static Cast Creat(this CastComponent self, int configId)
        {
            Cast cast = self.AddChild<Cast, int>(configId);
            self.Casts.Add(cast);
            return cast;
        }
        
        public static Cast Creat(this CastComponent self, int configId, long id)
        {
            Cast cast = self.AddChildWithId<Cast, int>(id, configId);
            self.Casts.Add(cast);
            return cast;
        }

        public static Cast Creat(this CastComponent self, int configId, TSVector offset)
        {
            Cast cast = self.AddChild<Cast, int, TSVector>(configId, offset);
            self.Casts.Add(cast);
            return cast;
        }

        public static void Remove(this CastComponent self, Cast cast)
        {
            self.Casts.Remove(cast);
            self.RemoveChild(cast.Id);
        }

        public static void Remove(this CastComponent self, long castId)
        {
            Cast cast = self.GetChild<Cast>(castId);
            if (cast == null) return;
            self.Remove(cast);
        }

        public static void Remove(this CastComponent self, LSUnit unit)
        {
            Cast cast = self.Find(unit);
            if (cast == null) return;
            self.Remove(cast);
        }

        public static Cast Find(this CastComponent self, LSUnit unit)
        {
            if (unit == null) Log.Error($"无法找到为空的技能");

            return self.GetChild<Cast>(unit.Id);
        }
    }
}