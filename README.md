# SmallFightSystem

一个简单的2d战斗系统。还在持续更新中。。。

本工程是基于状态机和事件系统实现的一个横板闯关游戏的战斗系统。

系统分为3大部分

1. 数据层：主要用于获取一些配置的数据，包括角色属性、关卡属性、技能属性、buff属性等等
2. 逻辑层：逻辑层主要用于整个战斗流程的逻辑计算，包括对象逻辑实例的创建、更新、维护以及利用事件系统对表现层发送事件执行相应的操作。逻辑层是整个系统的核心部分。
3. 表现层：表现层主要是根据逻辑层通知的状态更新、事件等创建相应的gameobject，播放动画。以及接受用户输入并通知逻辑层做出相应动作的模块。

![](https://typora-picture-back-up.oss-cn-hangzhou.aliyuncs.com/smallFightSystem.png)

## 数据创建

游戏数据配置利用的是unity自带的ScriptableObject，可以方便的创建多种不同数据

![](https://typora-picture-back-up.oss-cn-hangzhou.aliyuncs.com/datacreate1.png)

![](https://typora-picture-back-up.oss-cn-hangzhou.aliyuncs.com/datacreate2.png)

数据会根据需要利用Resources.Load加载到内存中供给游戏使用。

## 展示

**开始战斗：**

![](https://typora-picture-back-up.oss-cn-hangzhou.aliyuncs.com/kaishizhandou.png)

**角色移动：**

![](https://typora-picture-back-up.oss-cn-hangzhou.aliyuncs.com/movegif.gif)

**敌人索敌**

![](https://typora-picture-back-up.oss-cn-hangzhou.aliyuncs.com/autoMove.gif)

**释放技能**

![](https://typora-picture-back-up.oss-cn-hangzhou.aliyuncs.com/heal.gif)

![](https://typora-picture-back-up.oss-cn-hangzhou.aliyuncs.com/damage.gif)

**施加buff**

场地治疗buff

![](https://typora-picture-back-up.oss-cn-hangzhou.aliyuncs.com/healbuff.gif)

流血buff

![](https://typora-picture-back-up.oss-cn-hangzhou.aliyuncs.com/DAMAGEBUFF.gif)

更新ing。。。

