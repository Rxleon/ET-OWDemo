using UnityEngine.SceneManagement;

namespace ET.Client
{
    [Event(SceneType.LockStepFrame)]
    public class LSFSceneChange_LoadAsset: AEvent<Scene, LSFSceneChange>
    {
        protected override async ETTask Run(Scene clientScene, LSFSceneChange args)
        {
            Room room = clientScene.GetComponent<Room>();
            ResourcesLoaderComponent resourcesLoaderComponent = room.AddComponent<ResourcesLoaderComponent>();
            room.AddComponent<UIComponent>();
            
            // 加载场景资源
            await resourcesLoaderComponent.LoadSceneAsync($"Assets/Bundles/Scenes/{room.Name}.unity", LoadSceneMode.Single);
        }
    }
}