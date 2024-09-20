基础配置
---
**1. 启用服务发现**

- 目前仅支持Consul服务发现
- App.ConsulEnabled： true-启动服务发现，false-禁用服务发现
- Consul.ConsulUrl：Consul提供的URL
- Consul.ServiceUrl: 本服务的URL

**2. 启动APM探针**

- 启动APM探针用于链路跟踪和日志采集，目前仅支持SkyWalking APM  
- App.SkyWalkingEnabled： true-启动APM探针，false-禁用APM探针