<div align="center">   

<h1>PlayerLoop Runner</h1>
Addon for <a href="https://github.com/skelitheprogrammer/PlayerLoop-Customization-API">PlayerLoopCustomizationAPI</a>. Add <b>custom</b> or Use <b>predefined</b> interfaces to run your custom Loop System!
</div>

# Features

- Predefined interfaces to use it in project
- Tools for making custom PlayerLoop solution

# Installation

### Add via package manager

```
https://github.com/skelitheprogrammer/PlayerLoop-customization-API-Runner-Addon.git
```

### Add dependency in manifest.json
```
"com.skillitronic.playerloopcustomizationapi.addons.runner": "https://github.com/skelitheprogrammer/PlayerLoop-customization-API-Runner-Addon.git"
```

# Getting Started

## Implement custom interface
```c#
public interface ICustomTick()
{
    void CustomTick();
}
```
## Implement Loop Item
Inherit from [LoopItem](LoopItem.cs) class
```c#
public interface ICustomTick()
{
    void CustomTick();
}

public class CustomTickLoopItem : LoopItem
{
    private IEnumerable<ICustomTick> _entries;
    
    public CustomTickLoopItem(IEnumerable<ICustomTick> entries)
    {
        _entries = entries;
    }
    
    public override bool MoveNext()
    {
        if (Disposed)
        {
            return false;
        }

        OnMoveNext();

        return !Disposed;
    }
    
    protected override void OnMoveNext()
    {
        foreach (ILateTick tick in _items)
        {
            tick.LateTick();
        }
    }
}
```
> You also can inherit from [OneTimeLoopItem](Runtime/OneTimeLoopItem.cs) or [RepeatableLoopItem](Runtime/RepeatableLoopItem.cs) to get concrete implementation with one time or infintie uses

> You can implement fully custom LoopItem using [ILoopItem](ILoopItem.cs) interface
## Create class that will be invoked in your Composition root
```c#
public static class CustomTickInitializer 
{
    private static readonly LoopRunner _customTickLoopRunner;
    
    public static void Init(ILoopItem loopItem)
    {
        _customTickLoopRunner = new LoopRunner();
        _customTickLoopRunner.Dispatch(loopItem)
    }
}
```
> For better understanding check [Registrar](Implementation/Registrar.cs) class.
## Use PlayerLoopAPI to insert into PlayerLoop

```c#
public static class CustomTickInitializer 
{
    private struct CustomTickLoopName {}

    private static readonly LoopRunner _customTickLoopRunner;
    
    public static void Init(ILoopItem loopItem)
    {
        _customTickLoopRunner = new();
        _customTickLoopRunner.Dispatch(loopItem)
        
        PlayerLoopSystem customTickLoopSystem = new()
        {
            type = typeof(CustomTickLoopName),
            updateDelegate = _customTickLoopRunner.Run
        };
        
        ref PlayerLoopSystem copyLoop = ref PlayerLoopAPI.GetCustomPlayerLoop();
            
        copyLoop.GetLoopSystem<Update>().GetLoopSystem<Update.ScriptRunBehaviourUpdate>.InserAtBeginning(customTickLoopSystem);
        
    }
}
```

# Ready solution
If you don't want to create your own solution, but just want to use a specific implementation,
add `PLAYERLOOPAPI_RUNNER_IMPLEMENTATION` in [`Scripting define symbols`](https://docs.unity3d.com/Manual/CustomScriptingSymbols.html). 
From now on, all you need to do is to manage registration in your Composition Root class.

> If you don't want to manage registration, check out [As a plugin](#as-a-plugin) section.

# As a plugin
This addon also works as a plugin for [Reflex DI framework](https://github.com/gustavopsantos/Reflex#blazing-fast-minimal-but-complete-dependency-injection-library-for-unity) (using [ready solution](#ready-solution)). Look at [this package](https://github.com/skelitheprogrammer/Reflex-PlayerLoop-Runner-Plugin)
to understand how to start using it.
