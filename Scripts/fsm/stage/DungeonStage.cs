using UnityEngine;
using System.Collections;
using fsm;
using UnityEngine.SceneManagement;

public class DungeonStage : GameStage
{
    public enum EventType
    {
        UPDATEASSET = fsm.Event.USER_FIELD + 1,
        AWAKE,
        LOAD,
        RUN,
    }
    protected override void InitStateMachine()
    {
        base.InitStateMachine();
        fsm.State awake = new fsm.State("awake",stateMachine);
        awake.onEnter = EnterAwakeState;
        awake.onAction = UpdateAwakeState;
        awake.onExit = ExitAwakeState;

        fsm.State load = new fsm.State("load", stateMachine);
        load.onEnter = EnterLoadState;
        load.onAction = UpdateLoadState;
        load.onExit = ExitLoadState;

        fsm.State run = new fsm.State("run", stateMachine);
        run.onEnter = EnterRunState;
        run.onAction = UpdateRunState;
        run.onExit = ExitRunState;

        awake.Add<fsm.EventTransition>(load, (int)EventType.LOAD);

        load.Add<fsm.EventTransition>(run, (int)EventType.RUN);

        stateMachine.initState = awake;
    }

    protected void EnterAwakeState(State _from, State _to, fsm.Event _event)
    {
        stateMachine.Send((int)EventType.LOAD);
    }
    protected void UpdateAwakeState(State _from)
    {

    }
    protected void ExitAwakeState(State _from, State _to, fsm.Event _event)
    {

    }

    void LoadFinish(string error)
    {
        if (string.IsNullOrEmpty(error)) stateMachine.Send((int)EventType.RUN);
    }

    protected void EnterLoadState(State _from, State _to, fsm.Event _event)
    {
        SceneLoadMng.instance.LoadScene("level01_12", LoadFinish);
    }
    protected void UpdateLoadState(State _from)
    {

    }
    protected void ExitLoadState(State _from, State _to, fsm.Event _event)
    {

    }

    protected void EnterRunState(State _from, State _to, fsm.Event _event)
    {

    }
    protected void UpdateRunState(State _from)
    {

    }
    protected void ExitRunState(State _from, State _to, fsm.Event _event)
    {

    }
}
