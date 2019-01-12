﻿using System;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using WorkflowCore.Models;
using WorkflowCore.Providers.Redis.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static WorkflowOptions UseRedisQueues(this WorkflowOptions options, string connectionString, string prefix)
        {
            options.UseQueueProvider(sp => new RedisQueueProvider(connectionString, prefix, sp.GetService<ILoggerFactory>()));
            return options;
        }

        public static WorkflowOptions UseRedisLocking(this WorkflowOptions options, string connectionString, string keyName)
        {
            options.UseDistributedLockManager(sp => new RedisLockProvider(connectionString, keyName, sp.GetService<ILoggerFactory>()));
            return options;
        }

        public static WorkflowOptions UseRedisEventHub(this WorkflowOptions options, string connectionString, string channel)
        {
            options.UseEventHub(sp => new RedisLifeCycleEventHub(connectionString, channel, sp.GetService<ILoggerFactory>()));
            return options;
        }
    }
}
