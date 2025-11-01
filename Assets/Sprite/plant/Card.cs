using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
  public enum PlantType
    {
        Sunflower,
        Peashooter
    }
public enum CardState
{
    Disabled,
    Ready,
    Cooling
}
public class Card : MonoBehaviour
{
  

    [SerializeField]
    public float coolingTime;//冷却时间
    public float coolingTimer;//冷却计时器
    public int sunSpend;//花费的阳光数量
    public PlantType plantType;
    CardState currentState;
    public bool isCooling;
    public bool iscanBuyOnStart;//是否可以开局购买
    public Image mask;
    Button button;
    public void Start()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnClick); // 注册按钮点击事件
        }
        currentState = CardState.Disabled;
    }
    public void Update()
    {
        if (isCooling)
        {
            mask.fillAmount = 1 - coolingTimer / coolingTime;
            coolingTimer += Time.deltaTime;
            if (coolingTimer >= coolingTime)
            {
                FinishCooling();
            }
        }
        // 移除了else分支中的FinishCooling()调用，因为它会导致按钮状态不断重置
    }
    public void StartCooling()
    {
        isCooling = true;
        mask.gameObject.SetActive(true);
        button.enabled = false;
        currentState = CardState.Cooling;
    }
    public void FinishCooling()
    {
        isCooling = false;
        currentState = CardState.Ready;
        coolingTimer = 0;
        mask.fillAmount = 1;
        // 冷却完成后立即更新Mask状态
        if (sunSpend <= SunManager.instance._sunPoint)//阳光足够或可以免费购买时，隐藏Mask
        { mask.gameObject.SetActive(false);
           button.enabled = true; 
        }
        else //阳光不足且不能免费购买时，显示Mask
        {
            mask.gameObject.SetActive(true);
            button.enabled = false;
        }
    }
    public void OnClick()
    {
        if (currentState == CardState.Disabled)
        {
            return;
        }
        SunManager.instance.SubSun(sunSpend);
        StartCooling();
        HandManage.instance.BuyPlant(plantType);
    }
    public void DisableCard() { 
    currentState = CardState.Disabled;
    }
    public void EnableCard() { 
        currentState = CardState.Ready;
        // 不再调用StartCooling()，而是直接设置卡片为就绪状态
        isCooling = false;
        coolingTimer = 0;
        mask.fillAmount = 1;
        
        // 检查阳光是否足够
        if (sunSpend <= SunManager.instance._sunPoint)
        {
            mask.gameObject.SetActive(false);
            button.enabled = true;
        }
        else
        {
            mask.gameObject.SetActive(true);
            button.enabled = false;
        }
    }
}
