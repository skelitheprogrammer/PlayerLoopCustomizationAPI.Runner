using PlayerLoopCustomizationAPI.Runtime;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;

namespace PlayerLoopCustomizationAPI.Addons.Runner.Implementation
{
    public enum PlayerLoopTiming
    {
        INITIALIZATION = 0,
        POST_INITIALIZATION = 1,

        START = 2,
        POST_START = 3,

        FIXED_TICK = 4,
        POST_FIXED_TICK = 5,

        TICK = 6,
        POST_TICK = 7,

        LATE_TICK = 8,
        POST_LATE_TICK = 9
    }

    public static class Registrar
    {
        private struct PlayerLoopAPIInitialize
        {
        }

        private struct PlayerLoopAPIPostInitialize
        {
        }

        private struct PlayerLoopAPIStart
        {
        }

        private struct PlayerLoopAPIPostStart
        {
        }

        private struct PlayerLoopAPIFixedTick
        {
        }

        private struct PlayerLoopAPIPostFixedTick
        {
        }


        private struct PlayerLoopAPITick
        {
        }

        private struct PlayerLoopAPIPostTick
        {
        }

        private struct PlayerLoopAPILateTick
        {
        }

        private struct PlayerLoopAPIPostLateTick
        {
        }
        
        private static readonly LoopRunner[] _runners = new LoopRunner[10];

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Init()
        {
            for (int index = 0; index < _runners.Length; index++)
            {
                _runners[index] = new LoopRunner();
            }

            PlayerLoopSystem playerLoopAPIInitSystem = new()
            {
                type = typeof(PlayerLoopAPIInitialize),
                updateDelegate = _runners[(int) PlayerLoopTiming.INITIALIZATION].Run
            };

            PlayerLoopSystem playerLoopAPIPostInitSystem = new()
            {
                type = typeof(PlayerLoopAPIPostInitialize),
                updateDelegate = _runners[(int) PlayerLoopTiming.POST_INITIALIZATION].Run
            };

            PlayerLoopSystem playerLoopAPIStartSystem = new()
            {
                type = typeof(PlayerLoopAPIStart),
                updateDelegate = _runners[(int) PlayerLoopTiming.START].Run
            };

            PlayerLoopSystem playerLoopAPIPostStartSystem = new()
            {
                type = typeof(PlayerLoopAPIPostStart),
                updateDelegate = _runners[(int) PlayerLoopTiming.POST_START].Run
            };

            PlayerLoopSystem playerLoopAPIFixedTickSystem = new()
            {
                type = typeof(PlayerLoopAPIFixedTick),
                updateDelegate = _runners[(int) PlayerLoopTiming.FIXED_TICK].Run
            };

            PlayerLoopSystem playerLoopAPIPostFixedTickSystem = new()
            {
                type = typeof(PlayerLoopAPIPostFixedTick),
                updateDelegate = _runners[(int) PlayerLoopTiming.POST_FIXED_TICK].Run
            };

            PlayerLoopSystem playerLoopAPITickSystem = new()
            {
                type = typeof(PlayerLoopAPITick),
                updateDelegate = _runners[(int) PlayerLoopTiming.TICK].Run
            };

            PlayerLoopSystem playerLoopAPIPostTickSystem = new()
            {
                type = typeof(PlayerLoopAPIPostTick),
                updateDelegate = _runners[(int) PlayerLoopTiming.POST_TICK].Run
            };

            PlayerLoopSystem playerLoopAPILateTickSystem = new()
            {
                type = typeof(PlayerLoopAPILateTick),
                updateDelegate = _runners[(int) PlayerLoopTiming.LATE_TICK].Run
            };

            PlayerLoopSystem playerLoopAPIPostLateTickSystem = new()
            {
                type = typeof(PlayerLoopAPIPostLateTick),
                updateDelegate = _runners[(int) PlayerLoopTiming.POST_LATE_TICK].Run
            };

            ref PlayerLoopSystem copyLoop = ref PlayerLoopAPI.GetCustomPlayerLoop();
            
            copyLoop.GetLoopSystem<Initialization>().WrapSystems(playerLoopAPIInitSystem, playerLoopAPIPostInitSystem);
            copyLoop.GetLoopSystem<EarlyUpdate>().WrapSystemsAt<EarlyUpdate.ScriptRunDelayedStartupFrame>(playerLoopAPIStartSystem, playerLoopAPIPostStartSystem);
            copyLoop.GetLoopSystem<FixedUpdate>().WrapSystemsAt<FixedUpdate.ScriptRunBehaviourFixedUpdate>(playerLoopAPIFixedTickSystem, playerLoopAPIPostFixedTickSystem);
            copyLoop.GetLoopSystem<Update>().WrapSystemsAt<Update.ScriptRunBehaviourUpdate>(playerLoopAPITickSystem, playerLoopAPIPostTickSystem);
            copyLoop.GetLoopSystem<PreLateUpdate>().WrapSystemsAt<PreLateUpdate.ScriptRunBehaviourLateUpdate>(playerLoopAPILateTickSystem, playerLoopAPIPostLateTickSystem);
        }

        public static void Dispatch(PlayerLoopTiming timing, ILoopItem item)
        {
            _runners[(int) timing].Dispatch(item);
        }
    }
}