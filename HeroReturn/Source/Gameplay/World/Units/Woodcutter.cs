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

public class Woodcutter : Unit
{
    int direction = 1;
    private AnimatedSprite walkSprite;
    private AnimatedSprite actionSprite;
    private SoundEffect getMoneySound;
    private SoundEffect woodcutterActionSound;

    public bool isCollect = false;
    private SpriteSheetAnimation actionAnimation;
    private Tree collectTree;
    public int collectTreesCount = 0;
    public int doAction = 0;

    public FinanceController finance;
    public Woodcutter(SpriteSheet woodcutterWalkSpriteSheet, SpriteSheet woodcutterActionSpriteSheet, SoundEffect getMoneySound, SoundEffect woodcutterActionSound, FinanceController financeController) : 
        base(woodcutterWalkSpriteSheet, woodcutterActionSpriteSheet, "rightWalk", new Vector2(139, 194), new Vector2(22.5f, 45))
    {
        walkSprite = new AnimatedSprite(woodcutterWalkSpriteSheet);
        actionSprite = new AnimatedSprite(woodcutterActionSpriteSheet);
        sprite = walkSprite;
        sprite.Play("rightWalk");
        finance = financeController;
        this.getMoneySound = getMoneySound;
        this.woodcutterActionSound = woodcutterActionSound;
    }

    public void Update(float deltaSeconds, List<Tree> treesArray)
    {
        base.UpdateUnit(deltaSeconds);

        if (collectTreesCount < 1 && !isCollect)
            foreach (var tree in treesArray)
            {
                if ((direction == 1 && position.X == tree.position.X - 20) || (direction == 2 && position.X == tree.position.X + 140))
                    if (tree.animation.IsComplete)
                        if (!tree.isCollecting)
                        {
                            doAction = 0;
                            sprite = actionSprite;
                            if (direction == 1)
                                actionAnimation = sprite.Play("rightAction");
                            else 
                                actionAnimation = sprite.Play("leftAction");
                            offset = new Vector2(22.5f, 45);
                            //offset = new Vector2(58.5f, 55.5f);
                            
                            tree.isCollecting = true;
                            isCollect = true;
                            collectTree = tree;
                        }
            }

        if (isCollect && doAction == 5)
        {
            switch (collectTree.level)
            {
                case 1:
                    collectTree.animation = collectTree.sprite.Play("firstLevel");
                    break;
                case 2:
                    collectTree.animation = collectTree.sprite.Play("secondLevel");
                    break;
                case 3:
                    collectTree.animation = collectTree.sprite.Play("thirdLevel");
                    break;
            }
            collectTree.animation.Rewind();
            collectTree.frameDelay = 0;
            collectTree.isCollecting = false;

            sprite = walkSprite;
            if (direction == 1)
                sprite.Play("rightWalk");
            else
                sprite.Play("leftWalk");
            offset = new Vector2(22.5f, 45);

            isCollect = false;
            collectTreesCount += 1;
        }

        if (isCollect && actionAnimation.IsComplete)
        {
            woodcutterActionSound.Play();
            doAction += 1;
            if (direction == 1)
                actionAnimation = sprite.Play("rightAction");
            else
                actionAnimation = sprite.Play("leftAction");
        }
            


        if (!isCollect && direction == 1)
            position.X += 1;

        if (!isCollect && direction == 2)
            position.X -= 1;

        if (position.X > 940)
        {
            sprite.Play("leftWalk");
            direction = 2;
        }

        if (collectTreesCount > 0)
        {
            sprite.Play("leftWalk");
            direction = 2;
        }


        if (position.X < 100)
        {
            if (collectTreesCount != 0)
                getMoneySound.Play();
            finance.worldMoney += 50 * collectTreesCount;
            sprite.Play("rightWalk");
            direction = 1;
            collectTreesCount = 0;
        }       
    }

    public override void Draw()
    {
        base.Draw();
    }
}

