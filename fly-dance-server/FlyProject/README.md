Hey!This is a Net Server!

先吃先吃 ->
🍕🍕🍕🍕🍕🍕🍕🍕🍕🍕
再然后 ->
开始飞翔的舞蹈 -> 🤸‍♂️🤸‍♂️🤸‍♂️🤸‍♂️🤸‍♂️🤸‍♂️🤸‍♂️🤸‍♂️

# 有关项目拆分
项目采用分布式-微服务模式进行开发

直接使用 Net Core 8.0 的WebAPI，当然有特殊需要的项目可以按需选择版本

Fly.System.Web各框架版本选型：
	-- Autofac 8.2.0，Autofac.Extensions.DependencyInjection 10.0
	-- SQLSugarCore 5.1.4.173
	-- AutoMapper
	-- DotNetCore.Cap 8.3.2 DotNetCore.CAP.Kafka8.3.2
	-- SkyAPM.Agent.AspNetCore 2.2.0 SkyAPM.Diagnostics.AspNetCore 2.2.0 SkyAPM.Utilities.DependencyInjectio 2.2.0
	-- RestSharp 112.1.0 RestSharp.Serializers.NewtonsoftJ 112.1.0 RestSharp.Serializers.Utf8Json 106.15.0
	-- StackExchange.Redis 2.8.24
	-- Consul 1.7.14.6
	-- MailKit 4.9.0
	-- StackExchange.Redis 2.8.24
	
# 服务部署方面



# 有关数据库
理想情况下将采用分布式数据库，更理想的情况将会使用MYSQL

在分布式情况下，必要时可以混合使用SQLServer和MySQL

分库规则：

