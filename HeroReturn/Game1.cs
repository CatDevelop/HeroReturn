using System;
using System.Linq;
using HeroReturn;
using HeroReturn.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HeroReturn;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private GameState gameState = GameState.StartMenu;

    Gameplay gameplay;

    Basic2d cursor;
    Basic2d menuBackground;
    Basic2d gameMenuBackground;
    Basic2d smithyBackground;
    Basic2d upgradeBackgroundFirstLevel;
    Basic2d upgradeBackgroundSecondLevel;
    Basic2d upgradeBackgroundThirdLevel;

    public Button2d NewGameButton;
    public Button2d LeftButton;
    public Button2d RightButton;
    public Button2d SmithyButton;
    public Button2d UpgradeButton;

    int menuState = 0;
    int menuOffset = 0;
    bool menuMove = false;

    int gameMenuState = 1;
    int gameMenuOffset = 0;
    int gameMenuMove = 0;

    int money = 0;
    int act = 0;
    int houseUpgrade = 0;
    int vegetableUpgrade = 0;
    int treesUpdate = 0;

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
        menuBackground = new Basic2d("2D\\Backgrounds\\BACKGROUND_MainMenu", new Vector2(1296, 360), new Vector2(2592, 720));
        gameMenuBackground = new Basic2d("2D\\Backgrounds\\BACKGROUND_Menu", new Vector2(-1296, 0), new Vector2(3888, 720));
        smithyBackground = new Basic2d("2D\\Backgrounds\\BACKGROUND_Smithy", new Vector2(648, 360), new Vector2(1296, 720));
        upgradeBackgroundFirstLevel = new Basic2d("2D\\Backgrounds\\BACKGROUND_Upgrade_FirstLvl", new Vector2(648, 360), new Vector2(1296, 720));
        // 0 - 3224
        // 1 - 1920
        // 2 - 700

        NewGameButton = new Button2d("2D\\UI\\BTN_New_Game",
            new Vector2(423, 246),
            new Vector2(431, 109),
            GoTo,
            GameState.GameMenu
        );

        LeftButton = new Button2d("2D\\UI\\BTN_Left",
            new Vector2(36, 324),
            new Vector2(45, 72),
            LeftGameMenu,
            null
        );

        RightButton = new Button2d("2D\\UI\\BTN_Right",
            new Vector2(1225, 324),
            new Vector2(45, 72),
            RightGameMenu,
            null
        );

        SmithyButton = new Button2d("2D\\UI\\BTN_Smithy",
            new Vector2(1120, 10),
            new Vector2(96, 126),
            GoTo,
            GameState.FirstUpgrade
        );

        UpgradeButton = new Button2d("2D\\UI\\BTN_Upgrade",
            new Vector2(1120, 10),
            new Vector2(152, 124),
            GoTo,
            GameState.SecondUpgrade
        );

        Globals.keyboard = new McKeyboard();
        Globals.mouse = new McMouseControl();

        gameplay = new Gameplay();

        (money, act, houseUpgrade, treesUpdate, vegetableUpgrade) = Engine.MetaData.ReadData();

        // TODO: use this.Content to load your game content here
    }

    //private void StartGame(object i)
    //{
    //  gameState = GameState.GameMenu;
    //}

    private void GoTo(object i)
    {
       this.gameState = (GameState) i;
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
                    NewGameButton.Update(new Vector2(215, 54));
                   
                break;
            case (GameState.Game):
                gameplay.Update();
                break;
            case (GameState.FirstUpgrade):

                break;
            case (GameState.SecondUpgrade):

                break;
            case (GameState.GameMenu):
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
                    LeftButton.Update(new Vector2(22, 36));


                break;   
            case (GameState.Map):

                throw new NotImplementedException();
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
                    NewGameButton.Draw(Vector2.Zero);
                break;

            case (GameState.Game):
                gameplay.Draw();
                break;

            case (GameState.FirstUpgrade):
                smithyBackground.Draw(new Vector2(0, 0));
                break;

            case (GameState.SecondUpgrade):
                upgradeBackgroundFirstLevel.Draw(new Vector2(0, 0));
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
                    LeftButton.Draw(Vector2.Zero);

                break;

            case (GameState.Map):
                throw new NotImplementedException();
        }

        cursor.Draw(new Vector2(Globals.mouse.newMousePos.X - 10, Globals.mouse.newMousePos.Y - 2), Vector2.Zero);

        Globals.spriteBatch.End();
    }
}