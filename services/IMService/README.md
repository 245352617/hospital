## 即时通讯微服务

### 一、Who：前言

​	在一次偶然的会议上谈起前端页面中数据即时刷新的场景，最终苏总与桂华确定了独立一个 **即时通讯微服务**（以下简称 *IMService*），并且作为其他微服务的基础设施使用。 其他微服务不与前端应用直接通过 WebSocket 通讯，而是通过 IMService 实现与前端的 WebSocket 通讯。如图所示：

![image-20211028180502740](https://gitee.com/lyzcren/images/raw/master/img/szYIJian/im/image-20211028180502740.png)

#### 独立的 SignalR 微服务的优点

* 1、减轻其他微服务的复杂程度

  其他微服务不需要引入 SignalR 或 WebSocket 等依赖，可简单地通过消息中间件实现全双工通讯功能。

* 2、可复用

  其他微服务只需要按协定的方式发布 MQ 消息，即可简单地实现即时通讯功能。并且当即时通讯功能需要重构时，不会对其他微服务造成影响。

* 3、高内聚

  当多个微服务各自实现自己的消息总线时，前端应用需要连接多条总线，在一定程度上增加了前端对总线消息的管理难度。

  使用独立的即时通讯微服务，可以让多个微服务在一条消息总线上与前端进行通讯，很大程度上可以缓解前端对消息总线的管理难度。极端情况下，一个前端应用只需要在最外层保持一个长连接（连接一条消息总线）即可监听应用内的所有后端推送消息。

#### 独立的 SignalR 微服务的限制

* 1、IMService 不应处理业务逻辑

  由于 IMService 已经独立成一个单独的模块且需要高度抽象以便提高可复用性，故而 IMService 不应也不能处理业务逻辑。简单来讲，以前我们可以像调用 WebAPI 一样调用一个 SignalR 方法并且得到一个业务查询/处理结果；而现在不能这样做了，因为要在一个 SignalR 方法返回里数据库查询结果，必然需要对其他微服务发起同步请求，这是不允许的。

#### IMService 需要实现的功能

* 实现双向通讯
* 实现广播消息推送
* 实现单客户端消息推送
* 实现单用户消息推送（向一个用户的登录多个客户端发送消息）

### 二、Why：IMService与其他服务的关系

* IMService 只关心什么时候（在什么情况下）向客户端发出消息，而不关心具体消息的内容跟业务逻辑。极大多数情况下 IMService 通过订阅 MQ 消息实现对客户端的消息推送，通过将客户端的 WebSocket 请求转发成 MQ 消息通知其他服务进行处理。

* 其他微服务不在乎 IMService 的存在，只需要在数据发生变化的时候发布一条 MQ 消息即可。

* IMService 与其他微服务间的通讯是异步的。

* 当 IMService 宕机时，不会对业务产生影响，只是即时通讯功能无法正常使用，其最常见的表现可能是页面无法自动刷新数据。

* 如何解决用户量级激增后的请求压力

  当广播消息到达客户端时，多个客户端会同时向服务器发起查询请求，按现有框架所有请求将直接打到数据库上，数据库服务器压力将瞬时暴增。

  > 以目前的业务体量和应用场景来讲，查询请求直接打到数据库也能在1秒内全部处理完。
  >
  > 但是我们不敢断言将来需不需要考虑缓存的问题，所以我们也提供相应的对策以供参考。

  * 单节点的情况下，可考虑使用 **Response Cache** （响应缓存）

    > 使用 Response Cache 可以将查询结果进行缓存，在请求上打上时间戳，同一个查询将只有第一次会进行数据库查询，后续请求直接返回缓存结果。可解决上述问题。
    >
    > 参考：[ASP.NET Core 中的响应缓存](https://docs.microsoft.com/en-us/aspnet/core/performance/caching/response?view=aspnetcore-5.0)

  * 分布式多节点的情况下，可考虑使用 API 网关缓存



### 三、How：加入该服务后如何实现即时消息

一般情况下，我们使用 IMService 只需要关注 Hub 及 Controller 即可。

#### 1、使用默认集线器

IMService 实现了一个默认的集线器（DefaultHub），DefaultHub 实现了广播消息、指定客户端消息、指定用户消息三个通用方法，其他微服务需要发送消息通知前端时，只需要发布默认集线器订阅的 MQ 消息即可。如：

``` csharp
// 发布广播的消息
await this._capPublisher.PublishAsync("im.default.send", new DefaultBroadcastEto("refreshTable", data));
// 指定客户端消息
await this._capPublisher.PublishAsync("im.default.send.client", new DefaultClientEto(connectionId, "refreshTable", data));
// 指定用户消息
await this._capPublisher.PublishAsync("im.default.send.user", new DefaultUserEto(userId, "refreshTable", data));
```

当 IMService 集成 JWT 验证后，IMService 的 UserID 与其他微服务的 UserID 是一致的。

#### 2、实现自己的集线器

当默认集线器无法满足要求时，可在 IMService 中实现自己的集线器，此时只需要：

1) 写一个继承自 `AbpHub` 的 `Hub`
2) 写一个继承自 `BaseHubController` 泛型类的 的 `Controller` （当然你也可以不用 Controller 而只是写一个简单类）
3) 实现自己的 MQ 消息跟 WebSocket 方法



### 四、结语

引入独立的 IMService 有利有弊，利弊的权衡也是见仁见智。我们希望实现简单的复用，并且在任何时候进行重构都不会对业务模块产生影响。但也因此设计产生了一些限制。最后，希望各位同事使用愉快，工作顺心~

