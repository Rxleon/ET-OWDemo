using System;
using System.Collections.Generic;
using System.IO;
using BulletSharp;
using BulletSharp.Math;

namespace ET
{
    [Code]
    [FriendOfAttribute(typeof(ET.B3WorldComponent))]
    public class CollisionInfoDispatcherComponent : Singleton<CollisionInfoDispatcherComponent>, ISingletonAwake
    {
        public readonly Dictionary<int, CollisionInfo> Infos = new();

        public void Awake()
        {
            string path = $"D:\\ColliderInfos.bytes";
            if (!File.Exists(path)) return;

            byte[] bytes = File.ReadAllBytes(path);
            List<CollisionInfo> infos = MemoryPackHelper.Deserialize(typeof(List<CollisionInfo>), bytes, 0, bytes.Length) as List<CollisionInfo>;

            foreach (CollisionInfo info in infos)
            {
                this.Infos.Add(info.Id, info);
            }
        }

        public CollisionObject Clone(int configId)
        {
            CollisionConfig config = CollisionConfigCategory.Instance.Get(configId);
            CollisionInfo info = this.Infos[configId];

            CollisionShape shape = null;
            switch (info.Tag)
            {
                case "Collision_Polygon":
                    shape = new ConvexHullShape(((PolygonInfo)info).Points);
                    ((ConvexHullShape)shape).InitializePolyhedralFeatures();
                    break;
                case "Collision_Mesh":
                    shape = new ConvexHullShape(((MeshInfo)info).Points);
                    ((ConvexHullShape)shape).InitializePolyhedralFeatures();
                    break;
                case "Collision_Cube":
                    shape = new BoxShape(((CubeInfo)info).Size);
                    break;
                case "Collision_Sphere":
                    shape = new SphereShape(((SphereInfo)info).R);
                    break;
                case "Collision_Capsule":
                    shape = new CapsuleShape(((CapsuleInfo)info).R, ((CapsuleInfo)info).Height);
                    break;
                default:
                    Log.Error($"未找到对{info.Tag}的处理");
                    break;
            }

            CollisionObject co;

            // 设置触发器
            if (config.IsTrigger == 1)
            {
                GhostObject go = new GhostObject();
                go.CollisionShape = shape;
                go.WorldTransform = Matrix.Translation(info.Position);
                go.CollisionFlags |= CollisionFlags.NoContactResponse;

                co = go;
            }
            else
            {
                DefaultMotionState motionState = new(Matrix.Translation(info.Position));
                Vector3 inertia = info.Mass == 0 ? Vector3.Zero : inertia = shape.CalculateLocalInertia(info.Mass);
                RigidBodyConstructionInfo rbInfo = new(info.Mass, motionState, shape, inertia);
                RigidBody body = new RigidBody(rbInfo);

                if (info.Mass == 0)
                {
                    body.CollisionFlags |= CollisionFlags.KinematicObject;
                    body.ActivationState = ActivationState.DisableDeactivation;
                }
                
                // 设置旋转约束(逆惯性张量)
                switch (config.InvInertiaDiagLocal)
                {
                    case "X":
                        body.InvInertiaDiagLocal = new Vector3(1, 0, 0);
                        break;
                    case "Y":
                        body.InvInertiaDiagLocal = new Vector3(0, 1, 0);
                        break;
                    case "Z":
                        body.InvInertiaDiagLocal = new Vector3(0, 0, 1);
                        break;
                }
                
                // 设置阻尼
                if (config.Damping > 0)
                {
                    body.SetDamping(config.Damping * 0.01f, 0);
                }

                co = body;
            }

            return co;
        }
    }
}