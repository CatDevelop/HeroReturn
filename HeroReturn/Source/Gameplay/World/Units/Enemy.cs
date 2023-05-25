#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;
using System.Diagnostics;
using MonoGame.Extended.Sprites;
#endregion
namespace HeroReturn;

public class Enemy : Unit
{
    int direction = 1;
    private AnimatedSprite walkSprite;
    private AnimatedSprite actionSprite;
    private SoundEffect getMoneySound;
    private SoundEffect enemyActionSound;

    public bool isFight = false;
    private SpriteSheetAnimation walkAnimation;
    private SpriteSheetAnimation actionAnimation;
    private Knight fightKnight;
    public int doAction = 0;
    public int hp = 500;

    public GameStats gameStats;

    void PlayAS() => enemyActionSound.Play();
    public Enemy(SpriteSheet enemyWalkSpriteSheet, SpriteSheet enemyActionSpriteSheet, SoundEffect getMoneySound, SoundEffect woodcutterActionSound, GameStats gameStats) : 
        base(enemyWalkSpriteSheet, enemyActionSpriteSheet, "walk", new Vector2(1200, 500), new Vector2(24, 42))
    {
        walkSprite = new AnimatedSprite(enemyWalkSpriteSheet);
        actionSprite = new AnimatedSprite(enemyActionSpriteSheet);
        sprite = walkSprite;
        this.walkAnimation = sprite.Play("walk");
        this.gameStats = gameStats;
        this.getMoneySound = getMoneySound;
        this.enemyActionSound = woodcutterActionSound;
    }

    public void Update(float deltaSeconds, int number, Queue<Knight> knights)
    {
        base.UpdateUnit(deltaSeconds);

        if (!isFight && position.X != 660 + (100 * number))
            position.X -= 1;

        if (position.X == 660 + (100 * number))
        {
            if (number == 0)
            {
                if (knights.Count != 0)
                    fightKnight = knights.Peek();
                else
                {
                    fightKnight = null;
                    sprite = walkSprite;
                    walkAnimation = sprite.Play("walk");
                    walkAnimation.Pause();
                }


                if (fightKnight != null)
                {
                    if (fightKnight.position.X == 560)
                    {
                        fightKnight.hp -= 1;
                        sprite = actionSprite;
                        actionAnimation = sprite.Play("action");
                        actionAnimation.OnCompleted = PlayAS;
                    }
                    else
                    {
                        sprite = walkSprite;
                        walkAnimation = sprite.Play("walk");
                        walkAnimation.Pause();
                    }

                }
            } else{
                walkAnimation.Pause();
            }
        } else {
            if (walkAnimation.IsPaused)
                walkAnimation.Play();
        }
    }

    public override void Draw()
    {
        base.Draw();
    }
}

