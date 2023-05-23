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
using Assimp;
#endregion
namespace HeroReturn;

public class Collector : Unit
{
    int direction = 1;
    private AnimatedSprite walkSprite;
    private AnimatedSprite actionSprite;
    public bool isCollect = false;
    private SpriteSheetAnimation actionAnimation;
    private Vegetable collectVegetable;
    public int collectVegetablesCount = 0;
    private SoundEffect getMoneySound;

    public FinanceController finance; 

    public Collector(SpriteSheet collectorWalkSpriteSheet, SpriteSheet collectorActionSpriteSheet, SoundEffect getMoneySound, FinanceController financeController) : 
        base(collectorWalkSpriteSheet, collectorActionSpriteSheet, "rightWalk", new Vector2(139, 194), new Vector2(28.5f, 46.5f))
    {
        walkSprite = new AnimatedSprite(collectorWalkSpriteSheet);
        actionSprite = new AnimatedSprite(collectorActionSpriteSheet);
        sprite = walkSprite;
        sprite.Play("rightWalk");
        finance = financeController;
        this.getMoneySound = getMoneySound;
    }

    public void Update(float deltaSeconds, List<Vegetable> vegetablesArray)
    {
        base.UpdateUnit(deltaSeconds);

        if(collectVegetablesCount < 2 && !isCollect)
            foreach(var vegetable in vegetablesArray)
            {
                if((direction == 1 && position.X == vegetable.position.X-20) || (direction == 2 && position.X == vegetable.position.X + 25))
                    if (vegetable.animation.IsComplete)
                        if(!vegetable.isCollecting)
                        {
                            sprite = actionSprite;
                            if(direction == 1)
                                actionAnimation = sprite.Play("rightCollect");
                            else
                                actionAnimation = sprite.Play("leftCollect");
                            vegetable.isCollecting = true;
                            isCollect = true;
                            collectVegetable = vegetable;
                        }
            }

        if(isCollect && actionAnimation.IsComplete)
        {
            switch (collectVegetable.level)
            {
                case 1:
                    collectVegetable.animation = collectVegetable.sprite.Play("firstLevel");
                    break;
                case 2:
                    collectVegetable.animation = collectVegetable.sprite.Play("secondLevel");
                    break;
                case 3:
                    collectVegetable.animation = collectVegetable.sprite.Play("thirdLevel");
                    break;
            }
            collectVegetable.animation.Rewind();
            collectVegetable.frameDelay = 0;
            collectVegetable.isCollecting = false;
            sprite = walkSprite;
            if (direction == 1)
                sprite.Play("rightWalk");
            else
                sprite.Play("leftWalk");
            isCollect = false;
            collectVegetablesCount += 1;
        }
        
        
        if (!isCollect && direction == 1)
        {
            position.X += 1;
        }
            

        if (!isCollect && direction == 2)
            position.X -= 1;

        if (position.X > 550)
        {
            sprite.Play("leftWalk");
            direction = 2;
        }

        if(collectVegetablesCount > 1)
        {
            sprite.Play("leftWalk");
            direction = 2;
        }
            

        if (position.X < 130)
        {
            if (collectVegetablesCount != 0)
                getMoneySound.Play();
            finance.worldMoney += 10 * collectVegetablesCount;
            collectVegetablesCount = 0;
            sprite.Play("rightWalk");
            direction = 1;
        }
            
    }

    public override void Draw()
    {
        base.Draw();
    }
}

