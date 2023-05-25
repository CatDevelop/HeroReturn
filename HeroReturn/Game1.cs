using System;
using System.Linq;
using System.Reflection.Metadata;
using HeroReturn;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.Content;
using MonoGame.Extended.Serialization;
using MonoGame.Extended.Sprites;

namespace HeroReturn;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private GameState gameState = GameState.StartMenu;

    Gameplay gameplay;

    Basic2d cursor;
    Basic2d menuBackground;
    Basic2d mapBackground;
    Basic2d gameMenuBackground;
    Basic2d smithyBackground;
    Basic2d upgradeBackground;

    Basic2d SmithyFirstLevelVegetableUpgradeOn;
    Basic2d SmithySecondLevelVegetableUpgradeOn;
    Basic2d SmithyThirdLevelVegetableUpgradeOn;

    Basic2d SmithyFirstLevelTreeUpgradeOn;
    Basic2d SmithySecondLevelTreeUpgradeOn;
    Basic2d SmithyThirdLevelTreeUpgradeOn;

    Basic2d HouseFirstLevelUpgradeOn;
    Basic2d HouseSecondLevelUpgradeOn;
    Basic2d HouseThirdLevelUpgradeOn;

    Basic2d SmithyFirstLevelVegetableUpgrade;
    Basic2d SmithySecondLevelVegetableUpgrade;
    Basic2d SmithyThirdLevelVegetableUpgrade;

    Basic2d SmithyFirstLevelTreeUpgrade;
    Basic2d SmithySecondLevelTreeUpgrade;
    Basic2d SmithyThirdLevelTreeUpgrade;
    Basic2d CoinsIcon;

    Basic2d HouseFirstLevelUpgrade;
    Basic2d HouseSecondLevelUpgrade;
    Basic2d HouseThirdLevelUpgrade;

    Basic2d SmithyBuying;
    Basic2d UpgradeBuying;

    Basic2d FirstActDone;
    Basic2d SecondActDone;
    Basic2d ThirdActDone;
    Basic2d FourthActDone;
    Basic2d FiveActDone;

    Basic2d FirstActCloud;
    Basic2d SecondActCloud;
    Basic2d ThirdActCloud;
    Basic2d FourthActCloud;

    SpriteFont font;
    SpriteFont smithyUpgradeCostFont;

    Song menuMusic;
    Song gameMusic;
    public SoundEffect door;
    public SoundEffect pressKey;

    public Button2d NewGameButton;
    public Button2d ContinueGameButton;
    public Button2d LeftButton;
    public Button2d RightButton;
    public Button2d SmithyButton;
    public Button2d UpgradeButton;
    public Button2d MapButton;
    public Button2d MapBackButton;
    public Button2d BackSmithyButton;
    public Button2d BackUpgradeButton;
    public Button2d BuySmithyUpgradeButton;
    public Button2d BuyHouseUpgradeButton;

    public Button2d SmithyFirstVegetableUpgradeButton;
    public Button2d SmithySecondVegetableUpgradeButton;
    public Button2d SmithyThirdVegetableUpgradeButton;

    public Button2d SmithyFirstTreeUpgradeButton;
    public Button2d SmithySecondTreeUpgradeButton;
    public Button2d SmithyThirdTreeUpgradeButton;

    public Button2d HouseFirstUpgradeButton;
    public Button2d HouseSecondUpgradeButton;
    public Button2d HouseThirdUpgradeButton;

    public Button2d GoToFirstAct;
    public Button2d GoToSecondAct;
    public Button2d GoToThirdAct;
    public Button2d GoToFourthAct;
    public Button2d GoToFiveAct;

    //public FinanceController finance;

    int menuState = 0;
    int menuOffset = 0;
    bool menuMove = false;

    int gameMenuState = 1;
    int gameMenuOffset = 0;
    int gameMenuMove = 0;

    SelectSmithy selectSmithyUpdateLevel = SelectSmithy.firstLevelVegetable;
    int selectHouse = 1;

    GameStats gameStats;

    //int money = 0;
    //int act = 0;
    //int houseUpgrade = 0;
    //int vegetableUpgrade = 0;
    //int treesUpgrade = 0;

    public Game1()
    {
        Content.RootDirectory = "Content";
        _graphics = new GraphicsDeviceManager(this);
        IsMouseVisible = false;
    }

    protected override void Initialize()
    {
        _graphics.IsFullScreen = false;
        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 720;
        _graphics.ApplyChanges();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        base.LoadContent();
        Globals.content = this.Content;
        Globals.spriteBatch = new SpriteBatch(GraphicsDevice);

        cursor = new Basic2d("2D\\Misc\\cursor", new Vector2(0, 0), new Vector2(24, 28));
        font = Globals.content.Load<SpriteFont>("Fonts/WorldMoneyFont");
        smithyUpgradeCostFont = Globals.content.Load<SpriteFont>("Fonts/SmithyUpgradeCost");
        menuBackground = new Basic2d("2D\\Backgrounds\\BACKGROUND_MainMenu", new Vector2(1296, 360), new Vector2(2592, 720));
        gameMenuBackground = new Basic2d("2D\\Backgrounds\\BACKGROUND_Menu", new Vector2(-1296, 0), new Vector2(3888, 720));
        smithyBackground = new Basic2d("2D\\Backgrounds\\BACKGROUND_Smithy", new Vector2(648, 360), new Vector2(1296, 720));
        upgradeBackground = new Basic2d("2D\\Backgrounds\\BACKGROUND_Upgrade", new Vector2(648, 360), new Vector2(1296, 720));
        mapBackground = new Basic2d("2D\\Backgrounds\\BACKGROUND_Map", new Vector2(648, 360), new Vector2(1296, 720));

        SmithyFirstLevelVegetableUpgradeOn = new Basic2d("2D\\UI\\BTN_Smithy_First_On", new Vector2(491, 91), new Vector2(96, 96));
        SmithySecondLevelVegetableUpgradeOn = new Basic2d("2D\\UI\\BTN_Smithy_Second_On", new Vector2(491, 222), new Vector2(96, 96));
        SmithyThirdLevelVegetableUpgradeOn = new Basic2d("2D\\UI\\BTN_Smithy_Third_On", new Vector2(491, 353), new Vector2(96, 96));

        SmithyFirstLevelTreeUpgradeOn = new Basic2d("2D\\UI\\BTN_Smithy_First_On", new Vector2(662, 91), new Vector2(96, 96));
        SmithySecondLevelTreeUpgradeOn = new Basic2d("2D\\UI\\BTN_Smithy_Second_On", new Vector2(662, 222), new Vector2(96, 96));
        SmithyThirdLevelTreeUpgradeOn = new Basic2d("2D\\UI\\BTN_Smithy_Third_On", new Vector2(662, 353), new Vector2(96, 96));

        SmithyFirstLevelVegetableUpgrade = new Basic2d("2D\\UI\\SmithyFirstVegetableUpgrade", new Vector2(800, 42), new Vector2(377, 635));
        SmithySecondLevelVegetableUpgrade = new Basic2d("2D\\UI\\SmithySecondVegetableUpgrade", new Vector2(800, 42), new Vector2(377, 635));
        SmithyThirdLevelVegetableUpgrade = new Basic2d("2D\\UI\\SmithyThirdVegetableUpgrade", new Vector2(800, 42), new Vector2(377, 635));

        SmithyFirstLevelTreeUpgrade = new Basic2d("2D\\UI\\SmithyFirstTreeUpgrade", new Vector2(800, 42), new Vector2(377, 635));
        SmithySecondLevelTreeUpgrade = new Basic2d("2D\\UI\\SmithySecondTreeUpgrade", new Vector2(800, 42), new Vector2(377, 635));
        SmithyThirdLevelTreeUpgrade = new Basic2d("2D\\UI\\SmithyThirdTreeUpgrade", new Vector2(800, 42), new Vector2(377, 635));
        CoinsIcon = new Basic2d("2D\\UI\\CoinsIcon", new Vector2(900, 510), new Vector2(32, 32));

        HouseFirstLevelUpgradeOn = new Basic2d("2D\\UI\\BTN_Smithy_First_On", new Vector2(118, 155), new Vector2(96, 96));
        HouseSecondLevelUpgradeOn = new Basic2d("2D\\UI\\BTN_Smithy_Second_On", new Vector2(118, 277), new Vector2(96, 96));
        HouseThirdLevelUpgradeOn = new Basic2d("2D\\UI\\BTN_Smithy_Third_On", new Vector2(118, 406), new Vector2(96, 96));

        HouseFirstLevelUpgrade = new Basic2d("2D\\UI\\FirstLevelHouseUpgrade", new Vector2(648, 360), new Vector2(1296, 720));
        HouseSecondLevelUpgrade = new Basic2d("2D\\UI\\SecondLevelHouseUpgrade", new Vector2(648, 360), new Vector2(1296, 720));
        HouseThirdLevelUpgrade = new Basic2d("2D\\UI\\ThirdLevelHouseUpgrade", new Vector2(648, 360), new Vector2(1296, 720));

        FirstActDone = new Basic2d("2D\\UI\\BTN_V", new Vector2(266, 486), new Vector2(111, 80));
        SecondActDone = new Basic2d("2D\\UI\\BTN_V", new Vector2(569, 388), new Vector2(111, 80));
        ThirdActDone = new Basic2d("2D\\UI\\BTN_V", new Vector2(302, 253), new Vector2(111, 80));
        FourthActDone = new Basic2d("2D\\UI\\BTN_V", new Vector2(928, 473), new Vector2(111, 80));
        FiveActDone = new Basic2d("2D\\UI\\BTN_V", new Vector2(873, 324), new Vector2(111, 80));

        SmithyBuying = new Basic2d("2D\\UI\\UI_Smithy_Buying", new Vector2(648, 360), new Vector2(1296, 720));
        UpgradeBuying = new Basic2d("2D\\UI\\UI_Upgrade_Buying", new Vector2(648, 360), new Vector2(1296, 720));

        FirstActCloud = new Basic2d("2D\\UI\\UI_Cloud_Act1", new Vector2(150, 0), new Vector2(1002, 720));
        SecondActCloud = new Basic2d("2D\\UI\\UI_Cloud_Act2", new Vector2(150, 0), new Vector2(1002, 720));
        ThirdActCloud = new Basic2d("2D\\UI\\UI_Cloud_Act3", new Vector2(150, 0), new Vector2(1002, 720));
        FourthActCloud = new Basic2d("2D\\UI\\UI_Cloud_Act4", new Vector2(150, 0), new Vector2(1002, 720));

        menuMusic = Content.Load<Song>("Sounds\\Music\\MenuMusic");
        gameMusic = Content.Load<Song>("Sounds\\Music\\GameMusic");
        door = Globals.content.Load<SoundEffect>("Sounds\\Effects\\Door");
        pressKey = Globals.content.Load<SoundEffect>("Sounds\\Effects\\PressKey");

        MediaPlayer.Play(menuMusic);
        MediaPlayer.IsRepeating = true;
        MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;

        // 0 - 3224
        // 1 - 1920
        // 2 - 700

        NewGameButton = new Button2d("2D\\UI\\BTN_New_Game",
            new Vector2(423, 246),
            new Vector2(431, 109),
            StartNewGame,
            null,
            pressKey
        );

        ContinueGameButton = new Button2d("2D\\UI\\BTN_Continue_Game",
            new Vector2(423, 380),
            new Vector2(431, 109),
            GoTo,
            GameState.GameMenu,
            pressKey
        );

        LeftButton = new Button2d("2D\\UI\\BTN_Left",
            new Vector2(36, 324),
            new Vector2(45, 72),
            LeftGameMenu,
            null,
            pressKey
        );

        RightButton = new Button2d("2D\\UI\\BTN_Right",
            new Vector2(1225, 324),
            new Vector2(45, 72),
            RightGameMenu,
            null,
            pressKey
        );

        SmithyButton = new Button2d("2D\\UI\\BTN_Smithy",
            new Vector2(1120, 10),
            new Vector2(116, 126),
            GoTo,
            GameState.FirstUpgrade,
            pressKey
        );

        UpgradeButton = new Button2d("2D\\UI\\BTN_Upgrade",
            new Vector2(1100, 10),
            new Vector2(172, 124),
            GoTo,
            GameState.SecondUpgrade,
            pressKey
        );

        MapButton = new Button2d("2D\\UI\\BTN_Map",
            new Vector2(1120, 10),
            new Vector2(96, 126),
            GoTo,
            GameState.Map,
            pressKey
        );

        SmithyFirstVegetableUpgradeButton = new Button2d("2D\\UI\\BTN_Smithy_First",
            new Vector2(491, 91),
            new Vector2(96, 96),
            SetSelectSmithy,
            SelectSmithy.firstLevelVegetable,
            pressKey
        );

        SmithySecondVegetableUpgradeButton = new Button2d("2D\\UI\\BTN_Smithy_Second",
            new Vector2(491, 222),
            new Vector2(96, 96),
            SetSelectSmithy,
            SelectSmithy.secondLevelVegetable,
            pressKey
        );


        SmithyThirdVegetableUpgradeButton = new Button2d("2D\\UI\\BTN_Smithy_Third",
            new Vector2(491, 353),
            new Vector2(96, 96),
            SetSelectSmithy,
            SelectSmithy.thirdLevelVegetable,
            pressKey
        );

        SmithyFirstTreeUpgradeButton = new Button2d("2D\\UI\\BTN_Smithy_First",
            new Vector2(662, 91),
            new Vector2(96, 96),
            SetSelectSmithy,
            SelectSmithy.firstLevelTree,
            pressKey
        );

        SmithySecondTreeUpgradeButton = new Button2d("2D\\UI\\BTN_Smithy_Second",
            new Vector2(662, 222),
            new Vector2(96, 96),
            SetSelectSmithy,
            SelectSmithy.secondLevelTree,
            pressKey
        );


        SmithyThirdTreeUpgradeButton = new Button2d("2D\\UI\\BTN_Smithy_Third",
            new Vector2(662, 353),
            new Vector2(96, 96),
            SetSelectSmithy,
            SelectSmithy.thirdLevelTree,
            pressKey
        );

        HouseFirstUpgradeButton = new Button2d("2D\\UI\\BTN_Smithy_First",
            new Vector2(118, 155),
            new Vector2(96, 96),
            SetSelectHouse,
            1,
            pressKey
        );

        HouseSecondUpgradeButton = new Button2d("2D\\UI\\BTN_Smithy_Second",
            new Vector2(118, 277),
            new Vector2(96, 96),
            SetSelectHouse,
            2,
            pressKey
        );

        HouseThirdUpgradeButton = new Button2d("2D\\UI\\BTN_Smithy_Third",
            new Vector2(118, 406),
            new Vector2(96, 96),
            SetSelectHouse,
            3,
            pressKey
        );

        BackSmithyButton = new Button2d("2D\\UI\\BTN_Back",
            new Vector2(927, 630),
            new Vector2(140, 37),
            GoTo,
            GameState.GameMenu,
            pressKey
        );

        BackUpgradeButton = new Button2d("2D\\UI\\BTN_Back",
            new Vector2(98, 600),
            new Vector2(140, 37),
            GoTo,
            GameState.GameMenu,
            pressKey
        );

        BuySmithyUpgradeButton = new Button2d("2D\\UI\\BTN_BuyUpgrade",
            new Vector2(915, 580),
            new Vector2(159, 37),
            BuySmithyUpgrade,
            null,
            pressKey
        );

        GoToFirstAct = new Button2d("2D\\UI\\BTN_X",
            new Vector2(267, 482),
            new Vector2(111, 100),
            GoToGame,
            1,
            pressKey
        );

        GoToSecondAct = new Button2d("2D\\UI\\BTN_X",
            new Vector2(569, 384),
            new Vector2(111, 100),
            GoToGame,
            2,
            pressKey
        );

        GoToThirdAct = new Button2d("2D\\UI\\BTN_X",
            new Vector2(311, 241),
            new Vector2(111, 100),
            GoToGame,
            3,
            pressKey
        );

        GoToFourthAct = new Button2d("2D\\UI\\BTN_X",
            new Vector2(929, 463),
            new Vector2(111, 100),
            GoToGame,
            4,
            pressKey
        );

        GoToFiveAct = new Button2d("2D\\UI\\BTN_X",
            new Vector2(874, 213),
            new Vector2(111, 100),
            GoToGame,
            5,
            pressKey
        );

        MapBackButton = new Button2d("2D\\UI\\BTN_Map_Back",
            new Vector2(534, 610),
            new Vector2(226, 94),
            GoTo,
            GameState.GameMenu,
            pressKey
        );

        BuyHouseUpgradeButton = new Button2d("2D\\UI\\BTN_Upgrade_House",
            new Vector2(458, 583),
            new Vector2(216, 55),
            BuyHouseUpgrade,
            null,
            pressKey
        );


        Globals.keyboard = new McKeyboard();
        Globals.mouse = new McMouseControl();

        gameStats = new(new FinanceController(450, 200), 0, 0, 0, 0);
        (gameStats.act, gameStats.finance.gameMoney, gameStats.houseUpgrade, gameStats.treesUpgrade, gameStats.vegetableUpgrade) = Engine.MetaData.ReadData();

    }

    void SaveGame()
    {
        Engine.MetaData.UpdateData(gameStats.act, gameStats.finance.gameMoney, gameStats.houseUpgrade, gameStats.treesUpgrade, gameStats.vegetableUpgrade);
    }

    private void StartNewGame(object i)
    {
        gameStats.finance.gameMoney = 450;
        gameStats.act = 1;
        gameStats.houseUpgrade = 0;
        gameStats.treesUpgrade = 0;
        gameStats.vegetableUpgrade = 0;
        SaveGame();
        GoTo(GameState.GameMenu);
    }

    private void ContinueGame(object i)
    {
        (gameStats.act, gameStats.finance.gameMoney, gameStats.houseUpgrade, gameStats.treesUpgrade, gameStats.vegetableUpgrade) = Engine.MetaData.ReadData();
        GoTo(GameState.GameMenu);
    }

    void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
    {
        MediaPlayer.Volume -= 0.1f;
    }

    private void GoTo(object i)
    {
        this.gameState = (GameState) i;
        this.selectSmithyUpdateLevel = SelectSmithy.firstLevelVegetable;
        this.selectHouse = 1;
    }

    private void GoToGame(object i)
    {
        this.gameState = GameState.Game;
        MediaPlayer.Stop();
    }
    private void SetSelectSmithy(object i)
    {
        this.selectSmithyUpdateLevel = (SelectSmithy)i;
    }

    private void SetSelectHouse(object i)
    {
        this.selectHouse = (int)i;
    }

    private void BuySmithyUpgrade(object i)
    {
        if(selectSmithyUpdateLevel == SelectSmithy.firstLevelVegetable && gameStats.vegetableUpgrade == 0)
            if(gameStats.finance.gameMoney >= FinanceStats.VegetableUpdateFirstLevelCost)
            {
                gameStats.vegetableUpgrade = 1;
                gameStats.finance.gameMoney -= FinanceStats.VegetableUpdateFirstLevelCost;
                SaveGame();
            }

        if (selectSmithyUpdateLevel == SelectSmithy.secondLevelVegetable && gameStats.vegetableUpgrade == 1 && gameStats.treesUpgrade != 0 && gameStats.houseUpgrade != 0)
            if (gameStats.finance.gameMoney >= FinanceStats.VegetableUpdateSecondLevelCost)
            {
                gameStats.vegetableUpgrade = 2;
                gameStats.finance.gameMoney -= FinanceStats.VegetableUpdateSecondLevelCost;
                SaveGame();
            }

        if (selectSmithyUpdateLevel == SelectSmithy.thirdLevelVegetable && gameStats.vegetableUpgrade == 2 && gameStats.treesUpgrade != 0 && gameStats.houseUpgrade != 0)
            if (gameStats.finance.gameMoney >= FinanceStats.VegetableUpdateThirdLevelCost)
            {
                gameStats.vegetableUpgrade = 3;
                gameStats.finance.gameMoney -= FinanceStats.VegetableUpdateThirdLevelCost;
                SaveGame();
            }



        if (selectSmithyUpdateLevel == SelectSmithy.firstLevelTree && gameStats.treesUpgrade == 0)
            if (gameStats.finance.gameMoney >= FinanceStats.TreeUpdateFirstLevelCost)
            {
                gameStats.treesUpgrade = 1;
                gameStats.finance.gameMoney -= FinanceStats.TreeUpdateFirstLevelCost;
                SaveGame();
            }

        if (selectSmithyUpdateLevel == SelectSmithy.secondLevelTree && gameStats.treesUpgrade == 1 && gameStats.vegetableUpgrade != 0 && gameStats.houseUpgrade != 0)
            if (gameStats.finance.gameMoney >= FinanceStats.TreeUpdateSecondLevelCost)
            {
                gameStats.treesUpgrade = 2;
                gameStats.finance.gameMoney -= FinanceStats.TreeUpdateSecondLevelCost;
                SaveGame();
            }

        if (selectSmithyUpdateLevel == SelectSmithy.thirdLevelTree && gameStats.treesUpgrade == 2 && gameStats.vegetableUpgrade != 0 && gameStats.houseUpgrade != 0)
            if (gameStats.finance.gameMoney >= FinanceStats.TreeUpdateThirdLevelCost)
            {
                gameStats.treesUpgrade = 3;
                gameStats.finance.gameMoney -= FinanceStats.TreeUpdateThirdLevelCost;
                SaveGame();
            }
    }

    private void BuyHouseUpgrade(object i)
    {
        if (selectHouse == 1 && gameStats.houseUpgrade == 0)
            if (gameStats.finance.gameMoney >= FinanceStats.HouseUpdateFirstLevelCost)
            {
                gameStats.houseUpgrade = 1;
                gameStats.finance.gameMoney -= FinanceStats.HouseUpdateFirstLevelCost;
                SaveGame();
            }

        if (selectHouse == 2 && gameStats.houseUpgrade == 1 && gameStats.treesUpgrade != 0 && gameStats.vegetableUpgrade != 0)
            if (gameStats.finance.gameMoney >= FinanceStats.HouseUpdateSecondLevelCost)
            {
                gameStats.houseUpgrade = 2;
                gameStats.finance.gameMoney -= FinanceStats.HouseUpdateSecondLevelCost;
                SaveGame();
            }

        if (selectHouse == 3 && gameStats.houseUpgrade == 2 && gameStats.treesUpgrade != 0 && gameStats.vegetableUpgrade != 0)
            if (gameStats.finance.gameMoney >= FinanceStats.HouseUpdateThirdLevelCost)
            {
                gameStats.houseUpgrade = 3;
                gameStats.finance.gameMoney -= FinanceStats.HouseUpdateThirdLevelCost;
                SaveGame();
            }
    }


    private void LeftGameMenu(object i)
    {
        gameMenuMove = -1;
    }

    private void RightGameMenu(object i)
    {
        gameMenuMove = 1;
    }

    protected override void Update(GameTime gameTime)
    {
        var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();


        Globals.gameTime = gameTime;

        Globals.keyboard.Update();
        Globals.mouse.Update();

        base.Update(gameTime);

        switch (gameState)
        {
            case (GameState.StartMenu):
                if (Globals.keyboard.GetSinglePress("Space"))
                    menuMove = true;

                if (menuMove)
                    menuOffset -= 8;

                if (menuOffset == -1280)
                {
                    menuState = 1;
                    menuMove = false;
                }

                if(menuState == 1)
                {
                    if (gameStats.act != 0)
                        ContinueGameButton.Update(new Vector2(215, 54));
                    NewGameButton.Update(new Vector2(215, 54));
                }
                    
                   
                break;
            case (GameState.Game):
                if (gameplay == null)
                    gameplay = new Gameplay(gameStats, pressKey, GoTo);
                gameplay.Update(deltaSeconds);
                break;
            case (GameState.FirstUpgrade):
                if (MediaPlayer.State == MediaState.Playing)
                {
                    MediaPlayer.Stop();
                    door.Play();
                }
                BackSmithyButton.Update(new Vector2(70, 18.5f));
               

                if((selectSmithyUpdateLevel == SelectSmithy.firstLevelVegetable && gameStats.vegetableUpgrade ==0) ||
                    (selectSmithyUpdateLevel == SelectSmithy.secondLevelVegetable && gameStats.vegetableUpgrade == 1) ||
                    (selectSmithyUpdateLevel == SelectSmithy.thirdLevelVegetable && gameStats.vegetableUpgrade == 2) ||
                    (selectSmithyUpdateLevel == SelectSmithy.firstLevelTree && gameStats.treesUpgrade == 0) ||
                    (selectSmithyUpdateLevel == SelectSmithy.secondLevelTree && gameStats.treesUpgrade == 1) ||
                    (selectSmithyUpdateLevel == SelectSmithy.thirdLevelTree && gameStats.treesUpgrade == 2))
                    BuySmithyUpgradeButton.Update(new Vector2(79.5f, 18.5f));

                if (selectSmithyUpdateLevel != SelectSmithy.firstLevelVegetable)
                    SmithyFirstVegetableUpgradeButton.Update(new Vector2(48, 48));
                if (selectSmithyUpdateLevel != SelectSmithy.secondLevelVegetable)
                    SmithySecondVegetableUpgradeButton.Update(new Vector2(48, 48));
                if (selectSmithyUpdateLevel != SelectSmithy.thirdLevelVegetable)
                    SmithyThirdVegetableUpgradeButton.Update(new Vector2(48, 48));

                if (selectSmithyUpdateLevel != SelectSmithy.firstLevelTree)
                    SmithyFirstTreeUpgradeButton.Update(new Vector2(48, 48));
                if (selectSmithyUpdateLevel != SelectSmithy.secondLevelTree)
                    SmithySecondTreeUpgradeButton.Update(new Vector2(48, 48));
                if (selectSmithyUpdateLevel != SelectSmithy.thirdLevelTree)
                    SmithyThirdTreeUpgradeButton.Update(new Vector2(48, 48));

                break;
            case (GameState.SecondUpgrade):
                if(MediaPlayer.State == MediaState.Stopped)
                {
                    MediaPlayer.Play(menuMusic);
                    MediaPlayer.IsRepeating = true;
                }
                
                if ((selectHouse == 1 && gameStats.houseUpgrade == 0) ||
                    (selectHouse == 2 && gameStats.houseUpgrade == 1) ||
                    (selectHouse == 3 && gameStats.houseUpgrade == 2))
                    BuyHouseUpgradeButton.Update(new Vector2(108, 27.5f));

                if (selectHouse != 1)
                    HouseFirstUpgradeButton.Update(new Vector2(48, 48));
                if (selectHouse != 2)
                    HouseSecondUpgradeButton.Update(new Vector2(48, 48));
                if (selectHouse != 3)
                    HouseThirdUpgradeButton.Update(new Vector2(48, 48));

                BackUpgradeButton.Update(new Vector2(70, 18.5f));

                break;
            case (GameState.GameMenu):
                if (MediaPlayer.State == MediaState.Stopped)
                {
                    MediaPlayer.Play(menuMusic);
                    MediaPlayer.IsRepeating = true;
                }
                    

                if (gameMenuMove == 1)
                    gameMenuOffset -= 8;

                if (gameMenuMove == -1)
                    gameMenuOffset += 8;

                if (gameMenuMove != 0 && gameMenuOffset > 1270)
                {
                    gameMenuState = 0;
                    gameMenuMove = 0;
                }

                if (gameMenuMove != 0 && gameMenuOffset == 0)
                {
                    gameMenuState = 1;
                    gameMenuMove = 0;
                }

                if (gameMenuMove != 0 && gameMenuOffset < -1280)
                {
                    gameMenuState = 2;
                    gameMenuMove = 0;
                }

                if (gameMenuState == 0)
                {
                    RightButton.Update(new Vector2(22, 36));
                    SmithyButton.Update(new Vector2(48, 63));
                }
                    

                if (gameMenuState == 1)
                {
                    RightButton.Update(new Vector2(22, 36));
                    LeftButton.Update(new Vector2(22, 36));
                    UpgradeButton.Update(new Vector2(76, 77));
                }
                    

                if (gameMenuState == 2)
                {
                    LeftButton.Update(new Vector2(22, 36));

                    if(gameStats.vegetableUpgrade != 0 && gameStats.treesUpgrade != 0 && gameStats.houseUpgrade != 0)
                        MapButton.Update(new Vector2(48, 63));
                }
                    


                break;   
            case (GameState.Map):
                if (gameStats.act == 1)
                    GoToFirstAct.Update(new Vector2(55.5f, 50));
                if (gameStats.act == 2)
                    GoToSecondAct.Update(new Vector2(55.5f, 50));
                if (gameStats.act == 3)
                    GoToThirdAct.Update(new Vector2(55.5f, 50));
                if (gameStats.act == 4)
                    GoToFourthAct.Update(new Vector2(55.5f, 50));
                if (gameStats.act == 5)
                    GoToFiveAct.Update(new Vector2(55.5f, 50));
                MapBackButton.Update(new Vector2(113, 47));
                break;
        }

        Globals.keyboard.UpdateOld();
        Globals.mouse.UpdateOld();
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        
        Globals.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);


        base.Draw(gameTime);

        switch (gameState)
        {
            case (GameState.StartMenu):
                menuBackground.Draw(new Vector2(menuOffset, 0));
                if (menuState == 1)
                {
                    if (gameStats.act != 0)
                        ContinueGameButton.Draw(Vector2.Zero);
                    NewGameButton.Draw(Vector2.Zero);
                }
                    
                break;

            case (GameState.Game):
                if(MediaPlayer.State == MediaState.Stopped)
                {
                    MediaPlayer.Play(gameMusic);
                    MediaPlayer.IsRepeating = true;
                }
                    

                if (gameplay == null)
                    gameplay = new Gameplay(gameStats, pressKey, GoTo);
                gameplay.Draw();
                break;

            case (GameState.FirstUpgrade):
                smithyBackground.Draw(new Vector2(0, 0));
                Globals.spriteBatch.DrawString(font, gameStats.finance.gameMoney.ToString(), new Vector2(605, 625), Color.White);
                CoinsIcon.Draw(new Vector2(32, 32));
                if (selectSmithyUpdateLevel == SelectSmithy.firstLevelVegetable)
                {
                    if (gameStats.vegetableUpgrade == 0)
                        BuySmithyUpgradeButton.Draw(new Vector2(0, 0));
                    Globals.spriteBatch.DrawString(font, "За 1шт - " + FinanceStats.VegetableFirstLevelCost, new Vector2(915, 470), Color.White);
                    Globals.spriteBatch.DrawString(smithyUpgradeCostFont, FinanceStats.VegetableUpdateFirstLevelCost.ToString(), new Vector2(955, 500), Color.White);
                    SmithyFirstLevelVegetableUpgrade.Draw(new Vector2(188.5f, 317.5f));
                    SmithyFirstLevelVegetableUpgradeOn.Draw(new Vector2(48, 48));

                    if (gameStats.vegetableUpgrade >= 1)
                        SmithyBuying.Draw(new Vector2(0, 0));
                } else
                    SmithyFirstVegetableUpgradeButton.Draw(new Vector2(0, 0));

                if (selectSmithyUpdateLevel == SelectSmithy.secondLevelVegetable)
                {
                    if (gameStats.vegetableUpgrade == 1)
                        BuySmithyUpgradeButton.Draw(new Vector2(0, 0));
                    Globals.spriteBatch.DrawString(font, "За 1шт - " + FinanceStats.VegetableSecondLevelCost, new Vector2(915, 470), Color.White);
                    Globals.spriteBatch.DrawString(smithyUpgradeCostFont, FinanceStats.VegetableUpdateSecondLevelCost.ToString(), new Vector2(955, 500), Color.White);
                    SmithySecondLevelVegetableUpgrade.Draw(new Vector2(188.5f, 317.5f));
                    SmithySecondLevelVegetableUpgradeOn.Draw(new Vector2(48, 48));

                    if (gameStats.vegetableUpgrade >= 2)
                        SmithyBuying.Draw(new Vector2(0, 0));
                } else
                    SmithySecondVegetableUpgradeButton.Draw(new Vector2(0, 0));

                if (selectSmithyUpdateLevel == SelectSmithy.thirdLevelVegetable)
                {
                    if (gameStats.vegetableUpgrade == 2)
                        BuySmithyUpgradeButton.Draw(new Vector2(0, 0));
                    Globals.spriteBatch.DrawString(font, "За 1шт - " + FinanceStats.VegetableThirdLevelCost, new Vector2(915, 470), Color.White);
                    Globals.spriteBatch.DrawString(smithyUpgradeCostFont, FinanceStats.VegetableUpdateThirdLevelCost.ToString(), new Vector2(955, 500), Color.White);
                    SmithyThirdLevelVegetableUpgrade.Draw(new Vector2(188.5f, 317.5f));
                    SmithyThirdLevelVegetableUpgradeOn.Draw(new Vector2(48, 48));

                    if (gameStats.vegetableUpgrade >= 3)
                        SmithyBuying.Draw(new Vector2(0, 0));
                } else
                    SmithyThirdVegetableUpgradeButton.Draw(new Vector2(0, 0));

                if (selectSmithyUpdateLevel == SelectSmithy.firstLevelTree)
                {
                    if (gameStats.treesUpgrade == 0)
                        BuySmithyUpgradeButton.Draw(new Vector2(0, 0));
                    Globals.spriteBatch.DrawString(smithyUpgradeCostFont, FinanceStats.TreeUpdateFirstLevelCost.ToString(), new Vector2(955, 500), Color.White);
                    Globals.spriteBatch.DrawString(font, "За 1шт - " + FinanceStats.TreeFirstLevelCost, new Vector2(915, 470), Color.White);
                    SmithyFirstLevelTreeUpgrade.Draw(new Vector2(188.5f, 317.5f));
                    SmithyFirstLevelTreeUpgradeOn.Draw(new Vector2(48, 48));

                    if (gameStats.treesUpgrade >= 1)
                        SmithyBuying.Draw(new Vector2(0, 0));
                } else
                    SmithyFirstTreeUpgradeButton.Draw(new Vector2(0, 0));

                if (selectSmithyUpdateLevel == SelectSmithy.secondLevelTree)
                {
                    if (gameStats.treesUpgrade == 1)
                        BuySmithyUpgradeButton.Draw(new Vector2(0, 0));
                    Globals.spriteBatch.DrawString(smithyUpgradeCostFont, FinanceStats.TreeUpdateSecondLevelCost.ToString(), new Vector2(955, 500), Color.White);
                    Globals.spriteBatch.DrawString(font, "За 1шт - " + FinanceStats.TreeSecondLevelCost, new Vector2(915, 470), Color.White);
                    SmithySecondLevelTreeUpgrade.Draw(new Vector2(188.5f, 317.5f));
                    SmithySecondLevelTreeUpgradeOn.Draw(new Vector2(48, 48));

                    if (gameStats.treesUpgrade >= 2)
                        SmithyBuying.Draw(new Vector2(0, 0));
                } else
                    SmithySecondTreeUpgradeButton.Draw(new Vector2(0, 0));

                if (selectSmithyUpdateLevel == SelectSmithy.thirdLevelTree)
                {
                    if (gameStats.treesUpgrade == 2)
                        BuySmithyUpgradeButton.Draw(new Vector2(0, 0));
                    Globals.spriteBatch.DrawString(smithyUpgradeCostFont, FinanceStats.TreeUpdateThirdLevelCost.ToString(), new Vector2(955, 500), Color.White);
                    Globals.spriteBatch.DrawString(font, "За 1шт - "+ FinanceStats.TreeThirdLevelCost, new Vector2(915, 470), Color.White);
                    SmithyThirdLevelTreeUpgrade.Draw(new Vector2(188.5f, 317.5f));
                    SmithyThirdLevelTreeUpgradeOn.Draw(new Vector2(48, 48));

                    if (gameStats.treesUpgrade >= 3)
                        SmithyBuying.Draw(new Vector2(0, 0));
                } else
                    SmithyThirdTreeUpgradeButton.Draw(new Vector2(0, 0));

                

                BackSmithyButton.Draw(Vector2.Zero);
                break;

            case (GameState.SecondUpgrade):
                upgradeBackground.Draw(new Vector2(0, 0));
                Globals.spriteBatch.DrawString(font, gameStats.finance.gameMoney.ToString(), new Vector2(145, 525), Color.White);
                if (selectHouse == 1)
                {
                    if (gameStats.houseUpgrade == 0)
                        BuyHouseUpgradeButton.Draw(new Vector2(0, 0));
                    HouseFirstLevelUpgrade.Draw(new Vector2(0, 0));
                    HouseFirstLevelUpgradeOn.Draw(new Vector2(48, 48));
                    HouseSecondUpgradeButton.Draw(new Vector2(0, 0));
                    HouseThirdUpgradeButton.Draw(new Vector2(0, 0));
                    Globals.spriteBatch.DrawString(font, FinanceStats.HouseUpdateFirstLevelCost.ToString(), new Vector2(534, 481), Color.White);

                    if (gameStats.houseUpgrade >= 1)
                        UpgradeBuying.Draw(new Vector2(0, 0));
                }
                if (selectHouse == 2)
                {
                    if (gameStats.houseUpgrade == 1)
                        BuyHouseUpgradeButton.Draw(new Vector2(0, 0));
                    HouseSecondLevelUpgrade.Draw(new Vector2(0, 0));
                    HouseSecondLevelUpgradeOn.Draw(new Vector2(48, 48));
                    HouseFirstUpgradeButton.Draw(new Vector2(0, 0));
                    HouseThirdUpgradeButton.Draw(new Vector2(0, 0));
                    Globals.spriteBatch.DrawString(font, FinanceStats.HouseUpdateSecondLevelCost.ToString(), new Vector2(534, 481), Color.White);
                    if (gameStats.houseUpgrade >= 2)
                        UpgradeBuying.Draw(new Vector2(0, 0));
                }
                if (selectHouse == 3)
                {
                    if (gameStats.houseUpgrade == 2)
                        BuyHouseUpgradeButton.Draw(new Vector2(0, 0));
                    HouseThirdLevelUpgrade.Draw(new Vector2(0, 0));
                    HouseThirdLevelUpgradeOn.Draw(new Vector2(48, 48));
                    HouseFirstUpgradeButton.Draw(new Vector2(0, 0));
                    HouseSecondUpgradeButton.Draw(new Vector2(0, 0));
                    Globals.spriteBatch.DrawString(font, FinanceStats.HouseUpdateThirdLevelCost.ToString(), new Vector2(534, 481), Color.White);
                    if (gameStats.houseUpgrade >= 3)
                        UpgradeBuying.Draw(new Vector2(0, 0));
                }
                    
                BackUpgradeButton.Draw(new Vector2(0, 0));
                break;
            case (GameState.GameMenu):
                gameMenuBackground.Draw(new Vector2(gameMenuOffset, 0), Vector2.Zero);
                if (gameMenuMove == 0 && gameMenuState == 0)
                {
                    SmithyButton.Draw(Vector2.Zero);
                    RightButton.Draw(Vector2.Zero);
                }
                    
                if (gameMenuMove == 0 && gameMenuState == 1)
                {
                    LeftButton.Draw(Vector2.Zero);
                    RightButton.Draw(Vector2.Zero);
                    UpgradeButton.Draw(Vector2.Zero);
                }
                if (gameMenuMove == 0 && gameMenuState == 2)
                {
                    LeftButton.Draw(Vector2.Zero);
                    MapButton.Draw(Vector2.Zero);
                }
                    

                break;

            case (GameState.Map):
                MediaPlayer.Stop();
                mapBackground.Draw(new Vector2(0, 0));

                if(gameStats.act == 1)
                {
                    GoToFirstAct.Draw(new Vector2(0, 0));
                    FirstActCloud.Draw(new Vector2(501, 360));
                }

                if(gameStats.act == 2)
                {
                    FirstActDone.Draw(new Vector2(55.5f, 40));
                    GoToSecondAct.Draw(new Vector2(0, 0));
                    SecondActCloud.Draw(new Vector2(501, 360));
                }

                if(gameStats.act == 3)
                {
                    FirstActDone.Draw(new Vector2(55.5f, 40));
                    SecondActDone.Draw(new Vector2(55.5f, 40));
                    GoToThirdAct.Draw(new Vector2(0, 0));
                    ThirdActCloud.Draw(new Vector2(501, 360));
                }

                if(gameStats.act == 4)
                {
                    FirstActDone.Draw(new Vector2(55.5f, 40));
                    SecondActDone.Draw(new Vector2(55.5f, 40));
                    ThirdActDone.Draw(new Vector2(55.5f, 40));
                    GoToFourthAct.Draw(new Vector2(0, 0));
                    FourthActCloud.Draw(new Vector2(501, 360));
                }

                if (gameStats.act == 5)
                {
                    FirstActDone.Draw(new Vector2(55.5f, 40));
                    SecondActDone.Draw(new Vector2(55.5f, 40));
                    ThirdActDone.Draw(new Vector2(55.5f, 40));
                    FourthActDone.Draw(new Vector2(55.5f, 40));
                    GoToFiveAct.Draw(new Vector2(0, 0));
                }

                if (gameStats.act == 6)
                {
                    FirstActDone.Draw(new Vector2(55.5f, 40));
                    SecondActDone.Draw(new Vector2(55.5f, 40));
                    ThirdActDone.Draw(new Vector2(55.5f, 40));
                    FourthActDone.Draw(new Vector2(55.5f, 40));
                    FiveActDone.Draw(new Vector2(55.5f, 40));
                }
                MapBackButton.Draw(new Vector2(0, 0));
                break;
        }

        cursor.Draw(new Vector2(Globals.mouse.newMousePos.X - 10, Globals.mouse.newMousePos.Y - 2), Vector2.Zero);

        Globals.spriteBatch.End();
    }
}