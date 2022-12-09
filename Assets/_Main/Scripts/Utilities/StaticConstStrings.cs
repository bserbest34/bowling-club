using System;
using System.Reflection;

public static class TutorialType
{
    public const string TapAndHold = "TapAndHold";
    public const string TapTap = "TapTap";
    public const string Swerve = "Swerve";
    public const string DragAndMove = "DragAndMove";
    public const string SlideLeft = "SlideLeft";
    public const string SlideRight = "SlideRight";
}

public static class Key
{
    //Generic Keys
    public const string Level = "Level";
    public const string Money = "Money";

    //Upgrade System PlayerPrefs Keys
    public const string Button1_Level = "Button1_Level";
    public const string Button2_Level = "Button2_Level";
    public const string Button3_Level = "Button3_Level";
    public const string Button4_Level = "Button4_Level";

    public const string Button1_Money = "Button1_Money";
    public const string Button2_Money = "Button2_Money";
    public const string Button3_Money = "Button3_Money";
    public const string Button4_Money = "Button4_Money";

    //New Area System
    public const string ButtonBillard = "ButtonBillard";
    public const string ButtonFoosBall = "ButtonFoosball";

    public const string ButtonBillarMoney = "ButtonBillardMoney";
    public const string ButtonFoosballMoney = "ButtonFoosballMoney";

    //Shelf Upgrade
    public const string ButtonShelfUpgrade = "ButtonShelfUpgrade";
    public const string ShoesUpgrade = "ShoesUpgrade";

    public const string VipLangert = "VipLangert";
    public const string ButtonShelfUpgradeMoney = "ButtonShelfUpgradeMoney";

    //Bowling Area Buying and Upgrade
    public const string ButtonBowlingUpgrade = "ButtonBowlingUpgrade";
    public const string ButtonBowlingUpgradeMoney = "ButtonBowlingUpgradeMoney";

    public const string bannerIos = "Banner_iOS";
    public const string bannerAndroid = "Banner_Android";
    public const string intsIos = "Interstitial_iOS";
    public const string intsAndroid = "Interstitial_Android";
    public const string rewardedIos = "Rewarded_iOS";
    public const string rewardedAndroid = "Rewarded_Android";

    public static string GetBannerPlacementId()
    {
#if UNITY_ANDROID
        return bannerAndroid;
#else
        return bannerIos;
#endif
    }
    public static string GetIntsPlacementId()
    {
#if UNITY_ANDROID
        return intsAndroid;
#else
        return intsIos;
#endif
    }
    public static string GetRewardedPlacementId()
    {
#if UNITY_ANDROID
        return rewardedAndroid;
#else
        return rewardedIos;
#endif
    }

}
public static class Tags
{
    public const string Button = "Button";
    public const string UpgradeArea = "UpgradeArea";
    public const string NewArea = "NewArea";
    public const string BallCollector = "BallCollector";
    public const string Ball = "Ball";
    public const string BallPlatform = "BallPlatform";
    public const string OfficeArea = "OfficeArea";
    public const string BowlingAreaTarget = "BowlingAreaTarget";
    public const string ShoesArea = "ShoesArea";
    public const string DropShoes = "DropShoes";
    public const string Customer = "Customer";
    public const string Waiter = "Waiter";
    public const string RafBallArea = "RafBallArea";
    public const string Exit = "Exit";
    public const string BallTrigger = "BallTrigger";
    public const string Langert = "Langert";
    public const string Billard = "Billard";
    public const string BillardStickPoint = "BillardStickPoint";
    public const string BillardStickPoint2 = "BillardStickPoint2";
    public const string LangertTime = "LangertTime";
    public const string BillardTimer = "BillardTimer";
    public const string CleaningArea = "CleaningArea";
    public const string MoneyTrigger = "MoneyTrigger";
    public const string CafeArea = "CafeArea";
    public const string AICollector = "AICollector";
    public const string SpeedUpgrade = "SpeedUpgrade";
    public const string StackUpgrade = "StackUpgrade";
    public const string BarMoneyTrigger = "BarMoneyTrigger";
    public const string CafeChairOne = "CafeChairOne";
    public const string CafeChairTwo = "CafeChairTwo";
    public const string CafeChairThree = "CafeChairThree";
    public const string AIOfficer = "AIOfficer";
    public const string Upgrade2 = "Upgrade2";
    public const string NewBallArea = "NewBallArea";
    public const string BallArea = "BallArea";
    public const string ShoesCleanArea = "ShoesCleanArea";
    public const string CollectShoes = "CollectShoes";
    public const string ShoesAreaMoney = "ShoesAreaMoney";
    public const string CleanerAI = "CleanerAI";
    public const string UpgradeCanvas = "UpgradeCanvas";
    public const string BuyCustomer = "BuyCustomer";
    public const string NewAreaManager = "NewAreaManager";
    public const string Chair = "Chair";
    public const string ShelfUpgradeArea = "ShelfUpgradeArea";
    public const string ShoesUpgradeArea = "ShoesUpgradeArea";
    public const string NewBowlingArea = "NewBowlingArea";
    public const string OfficeDoor = "OfficeDoor";
    public const string BowlingUpgradeArea = "BowlingUpgradeArea";
    public const string SecondPart = "SecondPart";
    public const string NewDartArea = "NewDartArea";
    public const string MiniCafeArea = "MiniCafeArea";
    public const string TableTennis = "TableTennis";
    public const string ArcadeMachine = "ArcadeMachine";
    public const string HoverBoard = "HoverBoard";
    public const string MoneyBag = "MoneyBag";
    public const string Robots = "Robots";
    public const string VIPBowlingArea = "VIPBowlingArea";
    public const string TipBox = "TipBox";
    public const string Shelf = "Shelf";
    public const string BallCollectorAIs = "BallCollectorAIs";
}