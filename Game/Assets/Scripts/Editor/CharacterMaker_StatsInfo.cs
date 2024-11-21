using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterMaker_StatsInfo : MainEditorVisualElementBase
{
    private VisualElement baseContainer;
    private Label baseStatsLabel;
    private Label advStatsLabel;
    private Label DMGTypeLabel;

    private CharacterMaker_StatsInput HP;
    private CharacterMaker_StatsInput ATK;
    private CharacterMaker_StatsInput DEF;
    private CharacterMaker_StatsInput SPD;

    private CharacterMaker_StatsInput CRITRate;
    private CharacterMaker_StatsInput CRITDMG;
    private CharacterMaker_StatsInput BreakEffect;
    private CharacterMaker_StatsInput OutgoingHealingBonus;
    private CharacterMaker_StatsInput EnergyRegenerationRate;
    private CharacterMaker_StatsInput EffectHitRate;
    private CharacterMaker_StatsInput EffectRES;

    private CharacterMaker_StatsInput PhysicalDMGBoost;
    private CharacterMaker_StatsInput FireDMGBoost;
    private CharacterMaker_StatsInput IceDMGBoost;
    private CharacterMaker_StatsInput LightningDMGBoost;
    private CharacterMaker_StatsInput WindDMGBoost;
    private CharacterMaker_StatsInput QuantumDMGBoost;
    private CharacterMaker_StatsInput ImaginaryDMGBoost;

    private CharacterMaker_StatsInput PhysicalRESBoost;
    private CharacterMaker_StatsInput FireRESBoost;
    private CharacterMaker_StatsInput IceRESBoost;
    private CharacterMaker_StatsInput LightningRESBoost;
    private CharacterMaker_StatsInput WindRESBoost;
    private CharacterMaker_StatsInput QuantumRESBoost;
    private CharacterMaker_StatsInput ImaginaryRESBoost;

    private TabView tabView;
    private Tab tabStats;
    private Tab tabAbilities;

    private ScrollView scrollView;

    public CharacterMaker_StatsInfo()
    {
        SetupBaseContainer();
        SetupBaseStats();

        Add(baseContainer);
    }

    private void SetupBaseContainer()
    {
        baseContainer = new VisualElement();
        baseContainer.style.flexDirection = FlexDirection.Column;
        baseContainer.style.flexGrow = 1;
        baseContainer.style.height = StyleKeyword.Auto;
        baseContainer.style.marginTop = 5;

        tabStats = new Tab("Stats");
        tabAbilities = new Tab("Abilities");

        tabView = new TabView();
        tabView.Add(tabStats);
        tabView.Add(tabAbilities);

        scrollView = new ScrollView();
        scrollView.verticalScrollerVisibility = ScrollerVisibility.AlwaysVisible;
        scrollView.horizontalScrollerVisibility = ScrollerVisibility.Hidden;
        scrollView.style.flexGrow = 1;

        baseContainer.Add(tabView);
        baseContainer.Add(scrollView);
    }

    private void SetupBaseStats()
    {
        baseStatsLabel = EditorUtils.CreateLabelBold("Base Stats");
        baseStatsLabel.style.marginTop = 5;

        HP = new CharacterMaker_StatsInput("HP");
        ATK = new CharacterMaker_StatsInput("ATK");
        DEF = new CharacterMaker_StatsInput("DEF");
        SPD = new CharacterMaker_StatsInput("SPD");

        advStatsLabel = EditorUtils.CreateLabelBold("Advance Stats");
        advStatsLabel.style.marginTop = 5;

        CRITRate = new CharacterMaker_StatsInput("CRIT Rate");
        CRITDMG = new CharacterMaker_StatsInput("CRIT DMG");
        BreakEffect = new CharacterMaker_StatsInput("Break Effect");
        OutgoingHealingBonus = new CharacterMaker_StatsInput("Outgoing Healing Bonus");
        EnergyRegenerationRate = new CharacterMaker_StatsInput("Energy Regeneration Rate");
        EffectHitRate = new CharacterMaker_StatsInput("Effect Hit Rate");
        EffectRES = new CharacterMaker_StatsInput("Effect RES");

        DMGTypeLabel = EditorUtils.CreateLabelBold("DMG Type");
        DMGTypeLabel.style.marginTop = 5;

        PhysicalDMGBoost = new CharacterMaker_StatsInput("Physical DMG Boost");
        FireDMGBoost = new CharacterMaker_StatsInput("Fire DMG Boost");
        IceDMGBoost = new CharacterMaker_StatsInput("Ice DMG Boost");
        LightningDMGBoost = new CharacterMaker_StatsInput("Lightning DMG Boost");
        WindDMGBoost = new CharacterMaker_StatsInput("Wind DMG Boost");
        QuantumDMGBoost = new CharacterMaker_StatsInput("Quantum DMG Boost");
        ImaginaryDMGBoost = new CharacterMaker_StatsInput("Imaginary DMG Boost");

        PhysicalRESBoost = new CharacterMaker_StatsInput("Physical RES Boost");
        FireRESBoost = new CharacterMaker_StatsInput("Fire RES Boost");
        IceRESBoost = new CharacterMaker_StatsInput("Ice RES Boost");
        LightningRESBoost = new CharacterMaker_StatsInput("Lightning RES Boost");
        WindRESBoost = new CharacterMaker_StatsInput("Wind RES Boost");
        QuantumRESBoost = new CharacterMaker_StatsInput("Quantum RES Boost");
        ImaginaryRESBoost = new CharacterMaker_StatsInput("Imaginary RES Boost");


        scrollView.Add(baseStatsLabel);
        scrollView.Add(HP);
        scrollView.Add(ATK);
        scrollView.Add(DEF);
        scrollView.Add(SPD);
        
        scrollView.Add(advStatsLabel);
        scrollView.Add(CRITRate);
        scrollView.Add(CRITDMG);
        scrollView.Add(BreakEffect);
        scrollView.Add(OutgoingHealingBonus);
        scrollView.Add(EnergyRegenerationRate);
        scrollView.Add(EffectHitRate);
        scrollView.Add(EffectRES);

        scrollView.Add(DMGTypeLabel);
        scrollView.Add(PhysicalDMGBoost);
        scrollView.Add(FireDMGBoost);
        scrollView.Add(IceDMGBoost);
        scrollView.Add(LightningDMGBoost);
        scrollView.Add(WindDMGBoost);
        scrollView.Add(QuantumDMGBoost);
        scrollView.Add(ImaginaryDMGBoost);

        scrollView.Add(PhysicalRESBoost);
        scrollView.Add(FireRESBoost);
        scrollView.Add(IceRESBoost);
        scrollView.Add(LightningRESBoost);
        scrollView.Add(WindRESBoost);
        scrollView.Add(QuantumRESBoost);
        scrollView.Add(ImaginaryRESBoost);
    }
}
