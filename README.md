# ET-OWDemo

## 运行步骤

 1.需要安装: unity2022.3.15f1, Rider, 梯子
 
 2.以管理员权限打开UnityHub, 打开(Open), 选中'ET/Unity'文件夹

 3.Unity菜单/Edit/Preferences/External Tools，点击下拉框'External ScriptEditor'选择Rider，Generate .csproj files 
 for全部不要勾选

 4.Unity菜单/Window/Package Manager/HybridCLR/Update

 5.Unity菜单/HybridCLR/Installer

 6.'Assets/Resources/GlobalConfig', AppType选择'LockStepFrame', CodeMode选择'ClientServer', EPlayMode选择'HostPlayMode'

 7.Unity菜单/ET/Compile(或按快捷键F6)进行编译

 8.打开ET.sln，编译整个ET.sln，注意第一次要翻墙(翻墙后如果还有报错解决不了可以尝试先用VS打开ET.sln编译一次后再回到Rider重新编译一次)

 9.Unity菜单/HybridCLR/Generate/All

10.Unity菜单/HybridCLR/CopyAotDlls

11.Unity菜单/YooAsset/AssetBundle Builder

   ①BuildPipeline : 'ScriptableBuildPipeline'

   ②BuildMode : 'IncrementalBuild'

   ③CopyBuildinFileOption : 'ClearAndCopyAll'

   ④点击'Click Build'

12.可以在ET/Unity/Bundles里找到打包好的文件, 如果你想删除这些, 最好把DefultPackage里面内容也删了

13.~~开启资源服务器 (http://地址:端口/CDN/PC)~~ // 需要手动取消掉"Init"中"await ResourcesComponent.Update()"注释

   ①如果你的服务器只能在本地访问, 运行游戏时最好别开梯子

   ②在ResourcesComponent.GetHostServerURL()中更改资源服务器网址

   ③把YooAsset最新打包的资源放到资源服务器里面

14.打开Assets/Bundles/Scenes/Map1, 点击Unity菜单->Tools->Generate Map Infos

15.打开Assets/Scene/Collider, 点击Unity菜单/Tools/Generate Collider Infos

16.打开Assets/Scenes/Init, 点击Play即可运行

## 打包过程

 1.Assets/Resources/GlobalConfig, CodeMode选择'Client'

 2.Unity菜单/ET/Compile(或按快捷键F6)进行编译

 3.重复'运行过程' 10-16步

 4.Unity菜单/ET/BuildTool, 点击'BuildPackage'

## 开发时运行过程

 1.注释掉ResourcesComponent.Update()

 2.Assets/Resources/GlobalConfig, CodeMode选择"ClientServer", EPlayMode选择'Editor Simulate Mode'

 3.如果没执行过"运行过程" 15 16 步, 执行一遍
 
 4.以后每次更改代码后, 点击Unity菜单/ET/Compile(或按快捷键F6)进行编译

## ColliderScene使用方法
 1.目前分为Polyhedron Mesh Cube Sphere

 2.创建一个GameObject, 为它添加相应标签

   ①Polyhedron碰撞体需要LineRenderer LineRendererHelper(可选)

   ②Mesh碰撞体需要MeshFilter

   ③Cube碰撞体需要BoxCollider

   ④Sphere碰撞体需要SphereCollider

 3.如果GameObject上没RigidBody组件或组件中mass<0.1, 认为质量为0(静态物体)

 4.GameObject位置表示碰撞体偏移

 5.GameObject名字要和RigidBodyConfig中Id对应

 6.点击Unity菜单/Tools/Generate Collider Infos 会在D://ColliderInfos.bytes生成文件