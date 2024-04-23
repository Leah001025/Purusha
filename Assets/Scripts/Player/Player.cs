using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field: Header("References")]
    [field: SerializeField] public PlayerSO Data { get; private set; }

    [field: Header("Animations")]
    [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public PlayerInput Input { get; private set; }
    public CharacterController Controller { get; private set; }
    public CharacterActionController ActionController { get; private set; }

    private PlayerStateMachine stateMachine;
    public GameObject WorldMapAttackEffect;

    private void Awake()
    {
        //Data = Resources.Load<PlayerSO>("DataBase/PlayerSO");
        AnimationData = new PlayerAnimationData();
        AnimationData.Initialize();
        ActionController = new CharacterActionController();
        stateMachine = new PlayerStateMachine(this);
    }
    private void Init()
    {
        if (BattleManager.Instance == null)
        {
            Animator = gameObject.transform.GetChild(1).GetComponentInChildren<Animator>();
            Input = GetComponent<PlayerInput>();
            Controller = GetComponent<CharacterController>();
            stateMachine.ChangeState(stateMachine.WorldIdleState);
        }
        else
        {
            Animator = GetComponentInChildren<Animator>();
            stateMachine.ChangeState(stateMachine.BattleIdleState);
            ActionController.OnDie += ThisDie;
        }
    }
    private void Start()
    {
        Init();
        EffectLoad();
        //Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }
    private void ThisDie()
    {
        ActionController.OnDie -= ThisDie;
        Destroy(gameObject);
    }
    private void EffectLoad()
    {
        var res = ResourceManager.Instance.Load<GameObject>("UI/WorldMapUI/Attack");
        WorldMapAttackEffect = Instantiate(res, transform.GetChild(0));
        WorldMapAttackEffect.SetActive(false);
    }
    public void EffectPlay()
    {
        StartCoroutine(AttackEffect());
    }
    IEnumerator AttackEffect()
    {
        WorldMapAttackEffect.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        WorldMapAttackEffect.SetActive(false);
    }
}