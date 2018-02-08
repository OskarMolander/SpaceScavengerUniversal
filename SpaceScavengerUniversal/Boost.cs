﻿using System;
using Windows.UI.Text.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Space_Scavenger
{
    public class Boost : DrawableGameComponent
    {
        private readonly SpaceScavenger _myGame;
        private KeyboardState _prevKeyboardState;
        private GamePadState _previousGpState;
        public int BoostRegenerationCoolDown;

        public int NrOfBoosts;
        public int MaxNrOfBoosts;


        public Boost(Game game) : base(game)
        {
            _myGame = (SpaceScavenger) game;
            MaxNrOfBoosts = 1;
            NrOfBoosts = MaxNrOfBoosts;
            BoostRegenerationCoolDown = 0;
        }

        public override void Update(GameTime gameTime)
        {
            var state = Keyboard.GetState();
            var gpState = GamePad.GetState(PlayerIndex.One);
            if (NrOfBoosts > 0)
            {
                if (BoostRegenerationCoolDown > 0)
                    BoostRegenerationCoolDown--;

                if (state.IsKeyDown(Keys.Space) && !_prevKeyboardState.IsKeyDown(Keys.Space)
                    || gpState.IsButtonDown(Buttons.LeftTrigger) && _previousGpState.IsButtonDown(Buttons.LeftTrigger) != gpState.IsButtonDown(Buttons.LeftTrigger))
                {
                    _myGame.Player.Speed =
                        new Vector2((float) Math.Cos(_myGame.Player.Rotation),
                            (float) Math.Sin(_myGame.Player.Rotation)) * 40f;
                    NrOfBoosts--;
                    BoostRegenerationCoolDown = 300;
                }
                if (BoostRegenerationCoolDown <= 0)
                    if (NrOfBoosts >= 1 && NrOfBoosts < MaxNrOfBoosts)
                    {
                        NrOfBoosts++;
                        BoostRegenerationCoolDown = 300;
                    }
            }
            else if (NrOfBoosts == 0)
            {
                if (BoostRegenerationCoolDown > 0)
                {
                    BoostRegenerationCoolDown--;
                }
                else if (BoostRegenerationCoolDown <= 0)
                {
                    NrOfBoosts++;
                    BoostRegenerationCoolDown = 300;
                }
            }
            _prevKeyboardState = state;
            _previousGpState = gpState;
        }
    }
}