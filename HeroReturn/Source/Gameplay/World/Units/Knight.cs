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

public class Knight : Unit
{
    delegate void PlayActionSound();
    int direction = 1;
    private AnimatedSprite walkSprite;
    private AnimatedSprite actionSprite;
    private SoundEffect getMoneySound;
    private SoundEffect knightActionSound;

    public bool isFight = false;
    private SpriteSheetAnimation actionAnimation;
    private SpriteSheetAnimation walkAnimation;
    private PlayActionSound playActionSound;
    private Enemy fightEnemy;
    public int doAction = 0;
    public int hp = 800;

    void PlayAS() => knightActionSound.Play();

    public GameStats gameStats;
    public Knight(SpriteSheet knightWalkSpriteSheet, SpriteSheet knightActionSpriteSheet, SoundEffect getMoneySound, SoundEffect knightActionSound, GameStats gameStats) :
        base(knightWalkSpriteSheet, knightActionSpriteSheet, "walk", new Vector2(95, 500), new Vector2(22.5f, 46.5f))
    {
        walkSprite = new AnimatedSprite(knightWalkSpriteSheet);
        actionSprite = new AnimatedSprite(knightActionSpriteSheet);
        sprite = walkSprite;
        this.walkAnimation = sprite.Play("walk");
        this.gameStats = gameStats;
        this.getMoneySound = getMoneySound;
        this.knightActionSound = knightActionSound;

    }

    public void Update(float deltaSeconds, int number, Queue<Enemy> enemies)
    {
        base.UpdateUnit(deltaSeconds);

        if (!isFight && position.X != 560 - (100 * number))
            position.X += 1;

        if (position.X == 560 - (100 * number))
        {
            if (number == 0)
            {
                if(enemies.Count != 0)
                    fightEnemy = enemies.Peek();
                else
                    walkAnimation.Pause();

                if (fightEnemy != null)
                {
                    if (fightEnemy.position.X == 660)
                    {
                        fightEnemy.hp -= 1;
                        sprite = actionSprite;
                        actionAnimation = sprite.Play("action");
                        actionAnimation.OnCompleted = PlayAS;
                    } else {
                        sprite = walkSprite;
                        walkAnimation = sprite.Play("walk");
                        walkAnimation.Stop();
                    }

                }

                
            } else {
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

