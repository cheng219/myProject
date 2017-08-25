//=====================
//作者：邓成
//日期：2017/8/24
//用途：整个游戏的控制中心
//====================

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameCenter : Game
{
    /// <summary>
    /// 整个游戏的控制中心的唯一实例 by 邓成
    /// </summary>
    public static new GameCenter instance = null;

    protected static GameObject stageObj = null;
 

    //TO DO：要把这些对象全部弄进对象池，避免重复创建和删除 by邓成
    [System.NonSerialized]
    public GameObject dummyMobPrefab;
    [System.NonSerialized]
    public GameObject dummyNpcPrefab;
    [System.NonSerialized]
    public GameObject dummyOpcPrefab;

    #region 全局管理类

    #endregion

    #region 跟随场景的管理类
    /// <summary>
    /// 当前游戏运行逻辑  by邓成
    /// </summary>
    public static GameStage curGameStage = null;
    #endregion

    #region 跟随玩家的管理类


    #endregion

    #region UNITY
    void Start()
    {
        instance = this;
        stateMachine.Start();
        if (isPlatform)
        {

        }
    }


	protected int frames = 0;
    public int FPS
	{
		get{
			return fps;
		}
	}
    protected int fps = 0;
	protected float lastCountTime = 0;
	protected float blockTimes=0;
	protected float fpsNums=0;


    new void Update()
    {
		++frames;
        base.Update();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SdkExit();
        }

        if (isDevelopmentPattern)
        {
            UpdateTimeScale();
        }
    }


    void UpdateTimeScale()
    {
        if (Input.GetKey(KeyCode.Minus))
        {
            Time.timeScale = Mathf.Max(Time.timeScale - 0.01f, 0.0f);
        }
        else if (Input.GetKey(KeyCode.Equals))
        {
            Time.timeScale = Mathf.Min(Time.timeScale + 0.01f, 10.0f);
        }

        if (Input.GetKey(KeyCode.Alpha0))
        {
            Time.timeScale = 0.0f;
        }
        else if (Input.GetKey(KeyCode.Alpha9))
        {
            Time.timeScale = 1.0f;
        }
    }


    public void SdkExit()
    {
		GameExit();
    }
    void OnExitResult(string result)
    {
        GameExit();
    }

    void OnDisable()
    {

    }
    #endregion



    /// <summary>
    /// 注册消息监听
    /// </summary>
    void ResgistMsg()
    {

    }

    #region FSM 状态机控制部分
    /// <summary>
    /// 游戏状态枚举 by邓成
    /// </summary>
    public enum EventType
    {
        UPDATEASSET = fsm.Event.USER_FIELD + 1,
        AWAKE,
        PLATFORMLOGIN,
        INIT_CONFIG,
        LOGIN,
        WAIT_CONNECT,
        SELECTCHAR,
        CREATCHAR,
        ENTER_CITY,
        RUN_CITY,
        ENTER_DUNGEON,
        RUN_DUNGEON,
        LOAD_GAME,
        RUN_GAME,

        ENTER_ARENA,
        RUN_ARENA,
    }
    /// <summary>
    /// 初始化状态机 by邓成
    /// </summary>
    protected override void InitStateMachine()
    {
        fsm.State updateAsset = new fsm.State("updateAsset", stateMachine);
        updateAsset.onEnter += EnterUpdateAssetState;
        updateAsset.onExit += ExitUpdateAssetState;
        updateAsset.onAction += UpdateAssetState;

        fsm.State awake = new fsm.State("awake", stateMachine);
        awake.onEnter += EnterAwakeState;
        awake.onExit += ExitAwakeState;
        awake.onAction += UpdateAwakeState;


        fsm.State initConfig = new fsm.State("initConfig", stateMachine);
        initConfig.onEnter += EnterInitConfigState;
        initConfig.onExit += ExitInitConfigState;
        initConfig.onAction += UpdateInitConfigState;

        fsm.State platformLogin = new fsm.State("platformLogin", stateMachine);
        platformLogin.onEnter += EnterPlatformLoginState;
        platformLogin.onExit += ExitPlatformLoginState;

        fsm.State login = new fsm.State("login", stateMachine);
        login.onEnter += EnterLoginState;
        login.onExit += ExitLoginState;
        login.onAction += UpdateLoginState;

//        fsm.State selectChar = new fsm.State("selectChar", stateMachine);
//        selectChar.onEnter += EnterSelectCharState;
//        selectChar.onExit += ExitSelectCharState;
//        selectChar.onAction += UpdateSelectCharState;


        fsm.State createChar = new fsm.State("createChar", stateMachine);
        createChar.onEnter += EnterCreateCharState;
        createChar.onExit += ExitCreateCharState;
        createChar.onAction += UpdateCreateCharState;


        fsm.State enterCity = new fsm.State("enterCity", stateMachine);
        enterCity.onEnter += EnterInitCityState;
        enterCity.onExit += ExitInitCityState;
        enterCity.onAction += UpdateInitCityState;


        fsm.State runCity = new fsm.State("runCity", stateMachine);
        runCity.onEnter += EnterRunCityState;
        runCity.onExit += ExitRunCityState;
        runCity.onAction += UpdateRunCityState;


        ///竞技场的增加 .
        fsm.State enterArana = new fsm.State("enterArana", stateMachine);
        enterArana.onEnter += EnterInitArenaState;
        enterArana.onExit += ExitInitArenaState;
        enterArana.onAction += UpdateInitArenaState;

        fsm.State runArana = new fsm.State("runArana", stateMachine);
        //  runArana.onEnter += EnterRunAranaState;
        //  runArana.onExit += ExitRunAranaState;
        runArana.onAction += UpdateRunArenaState;
        /// end 竞技场


        fsm.State enterDungeon = new fsm.State("enterDungeon", stateMachine);
        enterDungeon.onEnter += EnterInitDungeonState;
        enterDungeon.onExit += ExitInitDungeonState;
        enterDungeon.onAction += UpdateInitDungeonState;



        fsm.State runDungeon = new fsm.State("runDungeon", stateMachine);
        runDungeon.onEnter += EnterRunDungeonState;
        runDungeon.onExit += ExitRunDungeonState;
        runDungeon.onAction += UpdateRunDungeonState;



        awake.Add<fsm.EventTransition>(updateAsset, (int)EventType.UPDATEASSET);
        awake.Add<fsm.EventTransition>(initConfig, (int)EventType.INIT_CONFIG);

        updateAsset.Add<fsm.EventTransition>(initConfig, (int)EventType.INIT_CONFIG);

        initConfig.Add<fsm.EventTransition>(login, (int)EventType.LOGIN);
        initConfig.Add<fsm.EventTransition>(platformLogin, (int)EventType.PLATFORMLOGIN);
        initConfig.Add<fsm.EventTransition>(enterDungeon, (int)EventType.ENTER_DUNGEON);

        platformLogin.Add<fsm.EventTransition>(login, (int)EventType.LOGIN);

		//        login.Add<fsm.EventTransition>(selectChar, (int)EventType.SELECTCHAR);
        login.Add<fsm.EventTransition>(createChar, (int)EventType.CREATCHAR);
        login.Add<fsm.EventTransition>(platformLogin, (int)EventType.PLATFORMLOGIN);
		
		login.Add<fsm.EventTransition>(enterDungeon, (int)EventType.ENTER_DUNGEON);
		login.Add<fsm.EventTransition>(enterCity, (int)EventType.ENTER_CITY);

		//        selectChar.Add<fsm.EventTransition>(enterCity, (int)EventType.ENTER_CITY); 
//        selectChar.Add<fsm.EventTransition>(createChar, (int)EventType.CREATCHAR);
//        selectChar.Add<fsm.EventTransition>(enterDungeon, (int)EventType.ENTER_DUNGEON);
//        selectChar.Add<fsm.EventTransition>(login, (int)EventType.LOGIN); //选择角色到登录




		//        createChar.Add<fsm.EventTransition>(selectChar, (int)EventType.SELECTCHAR);
        createChar.Add<fsm.EventTransition>(enterCity, (int)EventType.ENTER_CITY);
		createChar.Add<fsm.EventTransition>(login, (int)EventType.LOGIN);
        createChar.Add<fsm.EventTransition>(enterDungeon, (int)EventType.ENTER_DUNGEON);//登录到地下城

        enterCity.Add<fsm.EventTransition>(runCity, (int)EventType.RUN_CITY);
        enterCity.Add<fsm.EventTransition>(updateAsset, (int)EventType.UPDATEASSET);//进主城时,跳转更新 
		enterCity.Add<fsm.EventTransition>(login, (int)EventType.LOGIN);//断线重连跳转登陆

        runCity.Add<fsm.EventTransition>(enterCity, (int)EventType.ENTER_CITY);
        runCity.Add<fsm.EventTransition>(enterDungeon, (int)EventType.ENTER_DUNGEON);
        runCity.Add<fsm.EventTransition>(enterArana, (int)EventType.ENTER_ARENA);// 跳转到竞技场
        runCity.Add<fsm.EventTransition>(login, (int)EventType.LOGIN);//主城到登录跳转
		//        runCity.Add<fsm.EventTransition>(selectChar, (int)EventType.SELECTCHAR);//主城跳转到选择角色列表  
        runCity.Add<fsm.EventTransition>(updateAsset, (int)EventType.UPDATEASSET);//在主城时,跳转更新 

        enterDungeon.Add<fsm.EventTransition>(runDungeon, (int)EventType.RUN_DUNGEON);
		enterDungeon.Add<fsm.EventTransition>(login, (int)EventType.LOGIN);//断线重连跳转登陆

        enterArana.Add<fsm.EventTransition>(runArana, (int)EventType.RUN_ARENA);
		enterArana.Add<fsm.EventTransition>(login, (int)EventType.LOGIN);//断线重连跳转登陆

        runArana.Add<fsm.EventTransition>(enterCity, (int)EventType.ENTER_CITY);
        runArana.Add<fsm.EventTransition>(login, (int)EventType.LOGIN);
        runArana.Add<fsm.EventTransition>(enterArana, (int)EventType.ENTER_ARENA);//添加竞技场到竞技场的跳转 add by邓成
        runArana.Add<fsm.EventTransition>(enterDungeon, (int)EventType.ENTER_DUNGEON);//添加竞技场到副本的跳转 add by邓成

        runDungeon.Add<fsm.EventTransition>(enterCity, (int)EventType.ENTER_CITY);
        runDungeon.Add<fsm.EventTransition>(enterDungeon, (int)EventType.ENTER_DUNGEON);
        runDungeon.Add<fsm.EventTransition>(login, (int)EventType.LOGIN);
        runDungeon.Add<fsm.EventTransition>(enterArana, (int)EventType.ENTER_ARENA);// 跳转到竞技场

        stateMachine.initState = awake;
    }

    #region 更新部分
    protected virtual void EnterUpdateAssetState(fsm.State _from, fsm.State _to, fsm.Event _event)
    {
        Destroy(stageObj.GetComponent<GameStage>());
        //curGameStage = stageObj.AddComponent<UpdateAssetStage>();
        Debug.Log("EnterUpdateAssetState");
    }
    protected virtual void ExitUpdateAssetState(fsm.State _from, fsm.State _to, fsm.Event _event)
    {
        Debug.Log("ExitUpdateAssetState");
    }

    protected virtual void UpdateAssetState(fsm.State _curState)
    {

    }
    #endregion

    #region 启动部分 by邓成
    protected virtual void EnterAwakeState(fsm.State _from, fsm.State _to, fsm.Event _event)
    {
        //Debug.Log("EnterAwakeState");
        if (!isDevelopmentPattern)
        {
            Application.targetFrameRate = 30;//限制最多每秒计算30帧
        }
        Caching.CleanCache();
        //LayerMng.Init();
        if (stageObj == null)
        {
            stageObj = new GameObject();
            stageObj.transform.parent = this.transform;
            stageObj.transform.localPosition = Vector3.zero;
            stageObj.name = "Stage";
        }


        GameObject assetLoadObj = new GameObject();
        assetLoadObj.transform.parent = this.transform;
        assetLoadObj.transform.localPosition = Vector3.zero;
        assetLoadObj.name = "AssetLoad";
        AssetMng assetMng = assetLoadObj.AddComponent<AssetMng>();
        assetMng.Init();

        GameObject.DontDestroyOnLoad(this.gameObject);

        ResgistMsg();

        //UI部分
        if (isDevelopmentPattern)
        {
            stateMachine.Send((int)EventType.INIT_CONFIG);
        }
        else
        {
            //GameCenter.uIMng.SwitchToUI(GUIType.UPDATEASSET);
            stateMachine.Send((int)EventType.UPDATEASSET);
        }

    }

    protected virtual void ExitAwakeState(fsm.State _from, fsm.State _to, fsm.Event _event)
    {

    }

    protected virtual void UpdateAwakeState(fsm.State _curState)
    {
    }
    #endregion

    #region 初始化数据配置状态 by邓成
    protected virtual void EnterInitConfigState(fsm.State _from, fsm.State _to, fsm.Event _event)
    {
        Resources.UnloadUnusedAssets();
		#region InitTable
        TableMng.intance.InitAll();
		#endregion
        //GameCenter.uIMng.GenGUI(GUIType.MESSAGE, true);
        ResgistMsg();
    }

    protected virtual void ExitInitConfigState(fsm.State _from, fsm.State _to, fsm.Event _event)
    {
        //	NGUIDebug.Log("ExitInitConfigState");
    }

    protected virtual void UpdateInitConfigState(fsm.State _curState)
    {
        if (TableMng.intance.LoadFinish)
        {
            stateMachine.Send((int)EventType.ENTER_DUNGEON);
        }
    }
    #endregion

    #region  平台登陆部分
    protected virtual void EnterPlatformLoginState(fsm.State _from, fsm.State _to, fsm.Event _event)
    {
        Resources.UnloadUnusedAssets();

    }
    protected virtual void ExitPlatformLoginState(fsm.State _from, fsm.State _to, fsm.Event _event)
    {

    }
    void OnLoginResult(string result)
    {

    }

    #endregion

    #region 登录部分 by邓成
    protected virtual void EnterLoginState(fsm.State _from, fsm.State _to, fsm.Event _event)
    {

    }

    protected virtual void ExitLoginState(fsm.State _from, fsm.State _to, fsm.Event _event)
    {
        ExitLoginState();
    }

    protected void ExitLoginState()
    {

    }

    protected virtual void UpdateLoginState(fsm.State _curState)
    {
        NetUpdate();
    }
    #endregion

    #region 等待连接部分 by邓成
    public enum WaitConnectType
    {
        normal,
        autoReconnect,
        playerRecconnect,
    }
    #endregion

    #region 创角部分 by邓成
    protected void EnterCreateCharState(fsm.State _from, fsm.State _to, fsm.Event _event)
    {
        curGameStage.UnRegistAll();
        curGameStage = null;
        Destroy(stageObj.GetComponent<GameStage>());
        //curGameStage = stageObj.AddComponent<CharacterCreateStage>();
    }

    protected void ExitCreateCharState(fsm.State _from, fsm.State _to, fsm.Event _event)
    {
        ExitCreateCharState();
    }

    protected void ExitCreateCharState()
    {

    }

    protected void UpdateCreateCharState(fsm.State _curState)
    {
        NetUpdate(false);
    }
    #endregion

    #region 主城部分 by邓成
    protected virtual void EnterInitCityState(fsm.State _from, fsm.State _to, fsm.Event _event)
    {
        GoRunCity();
    }

    protected virtual void ExitInitCityState(fsm.State _from, fsm.State _to, fsm.Event _event)
    {

    }

    protected virtual void UpdateInitCityState(fsm.State _curState)
    {
        NetUpdate();
    }


    protected virtual void EnterRunCityState(fsm.State _from, fsm.State _to, fsm.Event _event)
    {

    }

    protected virtual void ExitRunCityState(fsm.State _from, fsm.State _to, fsm.Event _event)
    {

    }

    protected virtual void UpdateRunCityState(fsm.State _curState)
    {
        NetUpdate();
    }
    #endregion

    #region 竞技场 部分 add 

    /// <summary>
    /// 进入竞技场 add .
    /// </summary>
    /// <param name="_from"></param>
    /// <param name="_to"></param>
    /// <param name="_event"></param>
    protected virtual void EnterInitArenaState(fsm.State _from, fsm.State _to, fsm.Event _event)
    {

    }

    protected virtual void ExitInitArenaState(fsm.State _from, fsm.State _to, fsm.Event _event)
    {
    }

    protected virtual void UpdateInitArenaState(fsm.State _curState)
    {
        NetUpdate();
    }

    protected virtual void UpdateRunArenaState(fsm.State _curState)
    {
        NetUpdate();
    }

    #endregion

    #region 地下城部分 by邓成
    protected virtual void EnterInitDungeonState(fsm.State _from, fsm.State _to, fsm.Event _event)
    {
        GoRunDungeon();
    }

    protected virtual void ExitInitDungeonState(fsm.State _from, fsm.State _to, fsm.Event _event)
    {
    }

    protected virtual void UpdateInitDungeonState(fsm.State _curState)
    {
        NetUpdate();
    }


    protected virtual void EnterRunDungeonState(fsm.State _from, fsm.State _to, fsm.Event _event)
    {
        GameStage stage = stageObj.GetComponent<GameStage>();
        if (stage != null) Destroy(stage);
        curGameStage = stageObj.AddComponent<DungeonStage>();
    }

    protected virtual void ExitRunDungeonState(fsm.State _from, fsm.State _to, fsm.Event _event)
    {

    }

    protected virtual void UpdateRunDungeonState(fsm.State _curState)
    {
        NetUpdate();
    }
    #endregion


    /// <summary>
    /// 切换到输入帐号密码的登录状态 by邓成
    /// </summary>
    public void GoPassWord()
    {
        stateMachine.Send((int)EventType.LOGIN);
    }

    /// <summary>
    /// 切换到加载数据配置的状态 by邓成
    /// </summary>
    public void GoInitConfig()
    {
        stateMachine.Send((int)EventType.INIT_CONFIG);
    }

    /// <summary>
    /// 切换到选择角色状态 by邓成
    /// </summary>
    public void GoSelectChar()
    {
        stateMachine.Send((int)EventType.SELECTCHAR);
    }

    /// <summary>
    /// 切换到创建角色状态 by邓成
    /// </summary>
    public void GoCreatChar()
    {
        stateMachine.Send((int)EventType.CREATCHAR);
    }

    /// <summary>
    /// 切换到初始化主城状态 by邓成
    /// </summary>
    public void GoInitCity()
    {
        stateMachine.Send((int)EventType.ENTER_CITY);
    }

    /// <summary>
    /// 切换到运行主城的状态 by邓成
    /// </summary>
    public void GoRunCity()
    {
        stateMachine.Send((int)EventType.RUN_CITY);
    }

    /// <summary>
    /// 切换到初始化地下城的状态 by邓成
    /// </summary>
    public void GoInitDungeon()
    {
        stateMachine.Send((int)EventType.ENTER_DUNGEON);
    }

    /// <summary>
    /// 切换到运行地下城的状态 by邓成
    /// </summary>
    public void GoRunDungeon()
    {
        stateMachine.Send((int)EventType.RUN_DUNGEON);
    }


    /// <summary>
    /// 切换到初始化竞技场的状态 .
    /// </summary>
    public void GoInitArenaFight()
    {
        stateMachine.Send((int)EventType.ENTER_ARENA);
    }

    /// <summary>
    /// 进入竞技场 add .
    /// </summary>
    public void GotoArenaFight()
    {
        stateMachine.Send((int)EventType.RUN_ARENA);
    }
    /// <summary>
    /// 进入到更新界面 
    /// </summary>
    public void GotoUpdate()
    {
        stateMachine.Send((int)EventType.UPDATEASSET);
    }

    public void GoWaitConnect()
    {
        stateMachine.Send((int)EventType.WAIT_CONNECT);
    }

    public void GoPlatformLogin()
    {
        stateMachine.Send((int)EventType.PLATFORMLOGIN);
    }
    /// <summary>
    /// 是否是切换账号,切换账号不需要自行调用SDK登陆接口  by邓成
    /// </summary>
    [HideInInspector]public bool isSwitchAccount = false;
    /// <summary>
    /// 切换账号
    /// </summary>
    public void SwitchAccount()
    {
        if (isPlatform)
        {

        }
        else
        {
            GoPassWord();
        }
    }
    #endregion

    /// <summary>
    /// 游戏退出 add .
    /// </summary>
    public void GameExit()
    {
        if (isPlatform)
        {

        }
        else
        {
            Application.Quit();
        }
    }


    #region 作弊命令
    protected bool openGodMsg = false;
    protected string godMsgStr = string.Empty;
    protected bool openCameraEditor = false;

    void OnGUI()
    {

    }
    #endregion

    #region 网络相关

    /// <summary>
    /// 是否正在重新连接
    /// </summary>
    protected bool isReConnecteding = false;
    /// <summary>
    /// 是否正在重新连接
    /// </summary>
    public bool IsReConnecteding
    {
        get
        {
            return isReConnecteding;
        }
        set
        {
            isReConnecteding = value;
        }
    }

    /// <summary>
    /// 流畅ping值
    /// </summary>
    public const int pingSmooth = 200;
    /// <summary>
    /// 中等ping值
    /// </summary>
    public const int pingOrdinary = 500;
    /// <summary>
    /// 延迟ping值
    /// </summary>
    public const int pingMax = 1000;
    /// <summary>
    /// 断网ping值
    /// </summary>
    public const int pingOff = 20000;

    public const int pingMaxNum = 15;

    List<bool> pingListNum = new List<bool>();
    public List<bool> PingListNum
    {
        get
        {
            return pingListNum;
        }
    }
    protected long pingTime = 10;
    /// <summary>
    /// ping网络的总时间
    /// </summary>
    public long PingTime
    {
        get
        {
            return pingTime;
        }
        set
        {
            pingTime = value;
            pingListNum.Clear();
        }
    }


    protected long netDelayTime = 10;
    public long NetDelayTime
    {
        get
        {
            return netDelayTime;
        }
        protected set
        {
            netDelayTime = value;
        }
    }

    //int pingDegress = 0;
    /// <summary>
    /// 开始ping网络的时间
    /// </summary>
    protected long pingStartTiem = 0;
    

    /// <summary>
    /// 连接状态发生变化的事件
    /// </summary>
    public static Action<bool> OnConnectStateChange;

    /// <summary>
    /// 0通讯忍耐时间
    /// </summary>
    public float pingWarnningTime = 2f;
    float curPingNetSendStartTime = 0;

    protected bool lastFrameHasPing = false;

    protected void NetUpdate(bool _needPing = true)
    {

    }
    #endregion
}