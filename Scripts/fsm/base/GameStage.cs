//==============================
////作者：邓成
////日期：2015/5/25
////用途：游戏平台基类
//=============================


using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class GameStage : FSMBase
{
    public enum EventType
    {
        Run = fsm.Event.USER_FIELD + 1,
        Over,
        Reset,
        Restart,
        Resume,
        Pause,
        USER_FIELD,

    }

    [System.NonSerialized]
    public string sceneName = "Unknown";


    public bool deinited { get { return deinited_; } }
    public string stageName = "Default Stage";
    protected bool deinited_ = false;
    protected fsm.State mainLoop = null;

    public virtual void PreGuiUpdate()
    {
    }

    public virtual void GuiUpdate()
    {
    }


    protected override void InitStateMachine()
    {
        fsm.State start = new fsm.State("start", stateMachine);
        start.onEnter += EnterStartState;
        start.onExit += ExitStartState;
        start.onAction += UpdateStartState;

        mainLoop = new fsm.State("mainLoop", stateMachine);
        mainLoop.onEnter += EnterMainLoopState;
        mainLoop.onExit += ExitMainLoopState;
        mainLoop.onAction += UpdateMainLoopState;

        fsm.State pause = new fsm.State("pause", stateMachine);
        pause.onEnter += EnterPauseState;
        pause.onExit += ExitPauseState;
        pause.onAction += UpdatePauseState;

        fsm.State over = new fsm.State("over", stateMachine);
        over.onEnter += EnterOverState;
        over.onExit += ExitOverState;
        over.onAction += UpdateOverState;

        fsm.State restart = new fsm.State("restart", stateMachine);
        restart.onEnter += EnterRestartState;
        restart.onExit += ExitRestartState;
        restart.onAction += UpdateRestartState;

        start.Add<fsm.EventTransition>(mainLoop, (int)EventType.Run);
        start.Add<fsm.EventTransition>(over, (int)EventType.Over);

        mainLoop.Add<fsm.EventTransition>(over, (int)EventType.Over);
        mainLoop.Add<fsm.EventTransition>(restart, (int)EventType.Reset);
        mainLoop.Add<fsm.EventTransition>(pause, (int)EventType.Pause);

        pause.Add<fsm.EventTransition>(mainLoop, (int)EventType.Resume);
        pause.Add<fsm.EventTransition>(restart, (int)EventType.Reset);

        over.Add<fsm.EventTransition>(restart, (int)EventType.Reset);

        restart.Add<fsm.EventTransition>(start, (int)EventType.Restart);


    }

    public string myLastIP = string.Empty;




    //protected new void Awake()
    //{
    //    //base.Awake();

    //    //// if we are in Test Stage
    //    //if (Game.instance == false)
    //    //{
    //    //    Game.CreateDefault();
    //    //    Game.instance.Init();
    //    //}
    //    //Game.gameStage = this;
    //}

    void Start()
    {
        if (Init())
        {
            if (stateMachine != null)
                stateMachine.Start();
        }
    }


    void OnDestroy()
    {

    }


    protected virtual void Update()
    {
        if (stateMachine != null)
            stateMachine.Update();
    }




    protected virtual bool Init()
    {
        return true;
    }


    protected virtual void Deinit()
    {
    }


    public override void Reset()
    {
        base.Reset();
    }


    public void Run()
    {
        stateMachine.Send((int)EventType.Run);
    }



    public void Pause()
    {
        stateMachine.Send((int)EventType.Pause);
    }




    public void Resume()
    {
        stateMachine.Send((int)EventType.Resume);
    }




    public void Restart()
    {
        stateMachine.Send((int)EventType.Reset);
    }



    public void ReadyToStart()
    {
        stateMachine.Send((int)EventType.Restart);
    }





    public void Over()
    {
        stateMachine.Send((int)EventType.Over);
    }

    protected virtual void EnterStartState(fsm.State _from, fsm.State _to, fsm.Event _event)
    {
        //deinited_ = false;
        //Debug.LogInternal("[Stage] Start: " + stageName);
        //Run();
    }




    protected virtual void ExitStartState(fsm.State _from, fsm.State _to, fsm.Event _event)
    {

    }

    protected virtual void UpdateStartState(fsm.State _curState)
    {
    }


    protected virtual void EnterMainLoopState(fsm.State _from, fsm.State _to, fsm.Event _event)
    {
        //if (_from.name == "pause")
        //    Debug.LogInternal("[Stage] Resume: " + stageName);
    }


    protected virtual void ExitMainLoopState(fsm.State _from, fsm.State _to, fsm.Event _event)
    {
    }



    protected virtual void UpdateMainLoopState(fsm.State _curState)
    {
        //if (Game.isTestStage)
        //    exDebugHelper.ScreenPrint("Test Stage");

        //exDebugHelper.ScreenPrint("Time.time = " + Time.time.ToString("f2"));
        //exDebugHelper.ScreenLog("Time.frameCount = " + Time.frameCount);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }




    protected virtual void EnterPauseState(fsm.State _from, fsm.State _to, fsm.Event _event)
    {
        //Debug.LogInternal("[Stage] Paused: " + stageName);
    }



    protected virtual void ExitPauseState(fsm.State _from, fsm.State _to, fsm.Event _event)
    {
    }



    protected virtual void UpdatePauseState(fsm.State _curState)
    {
        // exDebugHelper.ScreenPrint( "Pause" );

        // if ( Input.GetKeyDown( KeyCode.Space ) ) {
        //     Resume();
        // }
        // else if ( Input.GetKeyDown( KeyCode.R ) ) {
        //     Restart ();
        // }
    }

    protected virtual void EnterOverState(fsm.State _from, fsm.State _to, fsm.Event _event)
    {
        //Debug.LogInternal("[Stage] Over: " + stageName);
        //Deinit();
        //deinited_ = true;
    }



    protected virtual void ExitOverState(fsm.State _from, fsm.State _to, fsm.Event _event)
    {
    }




    protected virtual void UpdateOverState(fsm.State _curState)
    {
    }



    protected virtual void EnterRestartState(fsm.State _from, fsm.State _to, fsm.Event _event)
    {
        //Debug.LogInternal("[Stage] Restart");
        //ReadyToStart();
    }


    protected virtual void ExitRestartState(fsm.State _from, fsm.State _to, fsm.Event _event)
    {
        Reset();
    }


    protected virtual void UpdateRestartState(fsm.State _curState)
    {
    }


    #region 对象管理
    public struct UID
    {
        public int typeID;
        public int instanceID;

        public UID(int _typeID, int _instanceID)
        {
            typeID = _typeID;
            instanceID = _instanceID;
        }

        public override bool Equals(object _obj)
        {
            if (_obj is UID)
            {
                UID c = (UID)_obj;
                return (typeID == c.typeID && instanceID == c.instanceID);
            }
            else
            {
                return false;
            }
        }

        public bool Equals(UID c)
        {
            return (typeID == c.typeID && instanceID == c.instanceID);
        }

        public override int GetHashCode() { return typeID ^ (instanceID << 5); }
    }


    
    protected virtual void Regist()
    {
    }

    public virtual void UnRegist()
    {
    }

    public void UnRegistAll()
    {
        UnRegist();
    }

    #endregion

}





