﻿using LeapInTheSadow.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace LeapInTheSadow.Entities
{
    class BackgroundManager
    {
        public BackgroundManager(Texture2D texture, Player player)
        {
            _startBackground = new Sprite(0,0 ,BACKGROUND_WIDTH,BACKGROUND_HEIGHT,texture,BACGROUND_SCALE);
            _defaultBackground = new Sprite(BACKGROUND_WIDTH,0 ,BACKGROUND_WIDTH,BACKGROUND_HEIGHT,texture,BACGROUND_SCALE);


            _backgroundTiles = new Queue<BackgroundTile>();
            _player = player;

            _player.LowDownAllBackground += MoveDownBackground;

            backgroundInit();
        }

        public void Draw(GameTime gameTime ,SpriteBatch spriteBatch)
        {
            foreach( BackgroundTile tile in _backgroundTiles)
            {
                tile.Draw(spriteBatch);
            }
        }

        public void Update(GameTime gameTime) 
        {
            _backgroundToRemove = 0;
            foreach( BackgroundTile tile in _backgroundTiles)
            {
                if(tile.Position.Y > SCREEN_HEIGHT)
                {
                    _backgroundToRemove++;
                }
            }

            while(_backgroundToRemove != 0)
            {
                _backgroundToRemove--;
                _backgroundTiles.Dequeue();
            }
        }

        public void MoveDownBackground(object sender, EventArgs e)
        {
            foreach (BackgroundTile tile in _backgroundTiles)
            {
                tile.Position = new Vector2(tile.Position.X, tile.Position.Y + GAP_BETWEEN_TILE);
            }
            if(_backgroundTiles.Last().Position.Y > -GAP_BETWEEN_TILE)
            {
                addNewBackground();
            }
        }
         private void addNewBackground()
         {
            _backgroundTiles.Enqueue(new BackgroundTile(_defaultBackground, new Vector2(0,-BACKGROUND_HEIGHT*BACGROUND_SCALE)) );
         }

        private void backgroundInit()
        {
            _backgroundTiles.Enqueue(new BackgroundTile(_startBackground, new Vector2(0,0)) );
            _backgroundTiles.Enqueue(new BackgroundTile(_defaultBackground, new Vector2(0,-BACKGROUND_HEIGHT*BACGROUND_SCALE)) );
        }

        private Queue<BackgroundTile> _backgroundTiles;

        private Sprite _startBackground;
        private Sprite _defaultBackground;

        private int _backgroundToRemove;
        private Player _player;

        private const int BACKGROUND_WIDTH = 100;
        private const int BACKGROUND_HEIGHT = 200;
        private const float BACGROUND_SCALE = 4f;
        private const int GAP_BETWEEN_TILE = 150;
        private const int SCREEN_HEIGHT = 800;

    }
}
