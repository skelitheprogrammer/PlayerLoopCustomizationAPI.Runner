using PlayerLoopCustomizationAPI.Utils;
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

            PlayerLoopSystem playerLoopAPIInitSystem = PlayerLoopUtils.CreateSystem<PlayerLoopAPIInitialize>(_runners[(int) PlayerLoopTiming.INITIALIZATION].Run);
            PlayerLoopSystem playerLoopAPIPostInitSystem = PlayerLoopUtils.CreateSystem<PlayerLoopAPIPostInitialize>(_runners[(int) PlayerLoopTiming.POST_INITIALIZATION].Run);
            PlayerLoopSystem playerLoopAPIStartSystem = PlayerLoopUtils.CreateSystem<PlayerLoopAPIStart>(_runners[(int) PlayerLoopTiming.START].Run);
            PlayerLoopSystem playerLoopAPIPostStartSystem = PlayerLoopUtils.CreateSystem<PlayerLoopAPIPostStart>(_runners[(int) PlayerLoopTiming.POST_START].Run);
            PlayerLoopSystem playerLoopAPIFixedTickSystem = PlayerLoopUtils.CreateSystem<PlayerLoopAPIFixedTick>(_runners[(int) PlayerLoopTiming.FIXED_TICK].Run);
            PlayerLoopSystem playerLoopAPIPostFixedTickSystem = PlayerLoopUtils.CreateSystem<PlayerLoopAPIPostFixedTick>(_runners[(int) PlayerLoopTiming.POST_FIXED_TICK].Run);
            PlayerLoopSystem playerLoopAPITickSystem = PlayerLoopUtils.CreateSystem<PlayerLoopAPITick>(_runners[(int) PlayerLoopTiming.TICK].Run);
            PlayerLoopSystem playerLoopAPIPostTickSystem = PlayerLoopUtils.CreateSystem<PlayerLoopAPIPostTick>(_runners[(int) PlayerLoopTiming.POST_TICK].Run);
            PlayerLoopSystem playerLoopAPILateTickSystem = PlayerLoopUtils.CreateSystem<PlayerLoopAPILateTick>(_runners[(int) PlayerLoopTiming.LATE_TICK].Run);
            PlayerLoopSystem playerLoopAPIPostLateTickSystem = PlayerLoopUtils.CreateSystem<PlayerLoopAPIPostLateTick>(_runners[(int) PlayerLoopTiming.POST_LATE_TICK].Run);

            PlayerLoopAPI.WrapInside(ref playerLoopAPIInitSystem, ref playerLoopAPIPostInitSystem, typeof(Initialization));
            PlayerLoopAPI.WrapAround(ref playerLoopAPIStartSystem, ref playerLoopAPIPostStartSystem, typeof(EarlyUpdate.ScriptRunDelayedStartupFrame));
            PlayerLoopAPI.WrapAround(ref playerLoopAPIFixedTickSystem, ref playerLoopAPIPostFixedTickSystem, typeof(FixedUpdate.ScriptRunBehaviourFixedUpdate));
            PlayerLoopAPI.WrapAround(ref playerLoopAPITickSystem, ref playerLoopAPIPostTickSystem, typeof(Update.ScriptRunBehaviourUpdate));
            PlayerLoopAPI.WrapAround(ref playerLoopAPILateTickSystem, ref playerLoopAPIPostLateTickSystem, typeof(PreLateUpdate.ScriptRunBehaviourLateUpdate));
        }

        public static void Dispatch(PlayerLoopTiming timing, ILoopItem item)
        {
            _runners[(int) timing].Dispatch(item);
        }
    }
}