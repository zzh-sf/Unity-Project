/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlantType
{ 
    Sunflower,
    Peashooter
}

[System.Serializable]
public class StateChange : MonoBehaviour
{
    enum CardState
    { 
        Ready,
        WaitingS,
        Cooling
    }
    
    public PlantType plantType; // 添加植物类型字段
    public int SunSpend;
    CardState currentState;
    public int currentCardIndex;
    public float coolingTime;
    public float coolingTimer;
    public bool isCooling;
    public bool isWaitingS;
    public bool isReady;
    public Image Mask;
    SunManager sun;
    public bool iscanBuy;
    Button button;
    
    void Awake() 
    {
        button = GetComponent<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
        sun = SunManager.instance; // 初始化sun引用
        if(button != null) {
            button.onClick.AddListener(OnClick); // 注册按钮点击事件
        }
        
        // 初始化状态
        UpdateMaskState();
        currentState = CardState.Ready;
        currentCardIndex = 0;
        coolingTimer = 0;
        isCooling = false;
        isWaitingS = false;
        isReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMaskState();
        
        if (isCooling && Mask != null)
        {
            Mask.fillAmount = 1 - coolingTimer / coolingTime;
            coolingTimer += Time.deltaTime;
            if (coolingTimer >= coolingTime)
            {
               FinishCooling(); // 冷却完成
            }
        }
    }

    void UpdateMaskState()
    {
        // 独立的Mask状态更新方法，确保每帧都会检查
        if (Mask != null)
        {
            
            if (sun!=null&& sun._sunPoint >= SunSpend)
            {
                // 阳光足够或可以免费购买时，隐藏Mask
                if (Mask.gameObject.activeSelf)
                {
                    Mask.gameObject.SetActive(false);
                    Debug.Log($"{plantType} 准备就绪！所需阳光: {SunSpend}");
                }
            }
            else
            {
                // 阳光不足且不能免费购买时，显示Mask
                if (!Mask.gameObject.activeSelf)
                {
                    Mask.gameObject.SetActive(true);
                    Mask.fillAmount = 1;
                }
            }
        }
    }
    
    void ReadyUpdate() 
    {
    
    }
    
    public void StartCooling()
    {
        if(Mask != null) {
            Mask.gameObject.SetActive(true);
            Mask.fillAmount = 1; // 初始填充为1（完全遮罩）
        }
        isReady = false;
        isWaitingS = false;
        isCooling = true;
        currentState = CardState.Cooling;
        coolingTimer = 0; // 重置冷却计时器
    }
    
    void FinishCooling()
    {
        isCooling = false;
        isReady = true;
        isWaitingS = false;
        currentState = CardState.Ready;
        // 冷却完成后立即更新Mask状态
        UpdateMaskState();
        coolingTimer = 0; // 重置冷却计时器
    }
    
    public void OnClick() 
    {
        // 只有在准备状态下且阳光足够时才能点击
        if(isReady && (sun != null && sun._sunPoint >= SunSpend)) {
            sun._sunPoint -= SunSpend;
            if (HandManage.instance != null)
            {
                HandManage.instance.BuyPlant(plantType);
            }
            StartCooling();
        }
    }
} */