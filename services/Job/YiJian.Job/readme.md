## `YiJian.Job` 项目说明

> `YiJian.Job` 项目是独立的定时调度模块，主要负责项目的作业调度问题



1. Hangfire 调度作业
2. RabbitMQ 处理调度异步事件，当发起调度的时候通过RabbitMQ发起
3. httpClient 处理调度同步调度，当发起调度的时候通过httpClient发起

