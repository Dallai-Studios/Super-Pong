using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DallaiStudios.SuperPong.Types;
using Newtonsoft.Json;
using UnityEngine;
using Object = UnityEngine.Object;

namespace DallaiStudios.SuperPong.Managers
{
    public static class DependencyInjection
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Bootstrap()
        {
            Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load(GameDependencies.GAME_MANAGER)));
            Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load(GameDependencies.SFX_MANAGER)));
        }
    }
}