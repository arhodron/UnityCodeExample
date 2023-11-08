using System;
using UnityEngine;
using Plugins.UnityECS;
using Plugins.Extensions;

namespace Game.ECS
{
    public sealed class SimpleEntityMono : EntityMono, IMonoPool
    {
        int IMonoPool.UID { get; set; }
    }
}
