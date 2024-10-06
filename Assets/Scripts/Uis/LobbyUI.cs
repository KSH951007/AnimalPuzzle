using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyUI : MonoBehaviour
{
    [Header("Heart")]
    [SerializeField] private TextMeshProUGUI heartCountTMP;
    [SerializeField] private TextMeshProUGUI heartChargeTimeTMP;
    private string chargeTimeFormat = @"{0} : {1}";

    [Header("Coin")]
    [SerializeField] private TextMeshProUGUI coinCountTMP;

    [SerializeField] private TextMeshProUGUI stageLevelBtnTMP;

    [SerializeField] private StageInfoUI stageInfoUI;
    private Player player;



    private void OnEnable()
    {
        GameManager.Instance.onStageInfoChanged += OnStageChanged;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        SetHeartCount(player.HeartCount);
        player.onChangeHeartCount += SetHeartCount;
        SetCoinCount(player.CoinCount);
        player.onChangeCoinCount += SetCoinCount;

    }

    private void OnDestroy()
    {
        player.onChangeHeartCount -= SetHeartCount;
        player.onChangeCoinCount -= SetCoinCount;
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void SetHeartCount(int heartCount)
    {
        heartCountTMP.text = heartCount.ToString();
    }
    public void SetCoinCount(int coinCount)
    {
        coinCountTMP.text = coinCount.ToString();
    }

    public void OnStageChanged(StageData stageData)
    {
        stageLevelBtnTMP.text = $"Stage {stageData.StageLevel}";
        stageInfoUI.SetStageInfo(stageData);
    }

    public void PressEntryStageInfo()
    {

        stageInfoUI.Show();
    }
}
