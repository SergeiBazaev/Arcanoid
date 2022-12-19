using System;
using SFML.System;
using SFML.Audio;
using SFML.Graphics;
using SFML.Window;



namespace Arcan
{
    internal class Program
    {
        static RenderWindow window;

        static Texture ballTexture;
        static Texture stickTexture;
        static Texture BlueBlocksTexture;
        static Texture RedBlocksTexture;


        static Sprite stick;
        static Sprite[] blueBlocks;
        static Sprite[] redBlocks;

        static Font mainFont = new Font("comic.ttf");

        static Ball ball;

        static int level = 1;
        static int count;
        static int scanc = 3;

        public static void SetStartPosition()
        {
            
            
            int index = 0;
            int index2 = 0;
            
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    if (x%(level+2) == 0 || y% (level + 1) == 0 || (x+y)% (level + 1)==0)
                    {
                        blueBlocks[index].Position = new Vector2f(2000, 2000);
                        index++;
                        continue;
                    }

                    blueBlocks[index].Position = new Vector2f(x * 65 + 75, y * 30 + 50);
                    count++;
                    index++;

                  
                    
                }
            }
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {

                    if (x % (level + 2) == 0 || y % (level + 1) == 0 || (x + y) % (level + 1) == 0)
                    {
                        redBlocks[index2].Position = new Vector2f(2000, 2000);
                        index2++;
                        continue;
                    }

                    redBlocks[index2].Position = new Vector2f(2000, 2000);
                    index2++;

                }
            }

            stick.Position = new Vector2f(400, 500);
            ball.sprite.Position = new Vector2f(375, 400);
        }

        static void Main(string[] args)
        {

            window = new RenderWindow(new VideoMode(800, 600), "Arcanoid");
            window.SetFramerateLimit(60);
            window.Closed += Window_Closed;

            ballTexture = new Texture("Ball.png");
            stickTexture = new Texture("Stick.png");
            BlueBlocksTexture = new Texture("Block.png");
            RedBlocksTexture = new Texture("Blockred.png");

            ball = new Ball(ballTexture);
            stick = new Sprite(stickTexture);
            blueBlocks = new Sprite[100];
            redBlocks= new Sprite[100];

            
            
            int record = 0;
            int score = 0;
            

            for (int i = 0; i < blueBlocks.Length; i++)
            {
                blueBlocks[i] = new Sprite(BlueBlocksTexture);
            }

            for (int i = 0; i < redBlocks.Length; i++)
            {
                redBlocks[i] = new Sprite(RedBlocksTexture);
            }

            int speedBoll = 5;

            SetStartPosition();

                while (window.IsOpen == true)
                {
                    window.Clear();

                    window.DispatchEvents();

                      


                    Text text1 = new Text();
                    text1.Font = mainFont;
                    text1.FillColor = Color.Magenta;
                    text1.CharacterSize = 16;
                    text1.Position = new Vector2f(30, 530);
                    text1.DisplayedString = $"Попытки: {scanc}";

                    Text text2 = new Text();
                    text2.Font = mainFont;
                    text2.FillColor = Color.Magenta;
                    text2.CharacterSize = 16;
                    text2.Position = new Vector2f(30, 560);
                    text2.DisplayedString = $"Уровень: {level}";

                    Text text3 = new Text();
                    text3.Font = mainFont;
                    text3.FillColor = Color.Magenta;
                    text3.CharacterSize = 16;
                    text3.Position = new Vector2f(670, 530);
                    text3.DisplayedString = $"Очки: {score}";

                    Text text4 = new Text();
                    text4.Font = mainFont;
                    text4.FillColor = Color.Magenta;
                    text4.CharacterSize = 16;
                    text4.Position = new Vector2f(670, 560);
                    text4.DisplayedString = $"Рекорд: {record}";

                    Text text5 = new Text();
                    text5.Font = mainFont;
                    text5.FillColor = Color.Green;
                    text5.CharacterSize = 30;
                    text5.Position = new Vector2f(70, 240);
                    text5.DisplayedString = $"Победа! Ты переходишь на новый уровень!";

                Text text6 = new Text();
                text6.Font = mainFont;
                text6.FillColor = Color.Green;
                text6.CharacterSize = 30;
                text6.Position = new Vector2f(100, 340);
                text6.DisplayedString = $"Для продолжения нажми на кнопку \"R\"!";

                Text text7 = new Text();
                text7.Font = mainFont;
                text7.FillColor = Color.Magenta;
                text7.CharacterSize = 16;
                text7.Position = new Vector2f(320, 560);
                text7.DisplayedString = $"Скорость: {speedBoll - 4}";

                Text text8 = new Text();
                text8.Font = mainFont;
                text8.FillColor = Color.Red;
                text8.CharacterSize = 30;
                text8.Position = new Vector2f(50, 240);
                text8.DisplayedString = $"Ты проиграл ((( У тебя осталось попыток: {scanc - 1} ";

                Text text9 = new Text();
                text9.Font = mainFont;
                text9.FillColor = Color.Red;
                text9.CharacterSize = 30;
                text9.Position = new Vector2f(50, 240);
                text9.DisplayedString = $"Ты проиграл ((( У тебя не осталось попыток ";



                if (Mouse.IsButtonPressed(Mouse.Button.Left) == true)
                    {
                        ball.Start(speedBoll, new Vector2f(0, -1));
                    }

                    ball.Move(new Vector2i(0, 0), new Vector2i(800, 600));
                    ball.CheckCollision(stick, "Stick");

                    for (int i = 0; i < redBlocks.Length; i++)
                    {
                        if (ball.CheckCollision(redBlocks[i], "Block") == true)
                        {
                            redBlocks[i].Position = new Vector2f(1000, 1000);
                            score++;
                            count--;
                            break;
                        }
                    }
                    
                    for (int i = 0; i < blueBlocks.Length; i++)
                    {
                        if (ball.CheckCollision(blueBlocks[i], "Block") == true)
                        {

                            redBlocks[i].Position = blueBlocks[i].Position;
                            blueBlocks[i].Position = new Vector2f(1000, 1000);
                            
                            break;
                        }

                    }


                    stick.Position = new Vector2f(Mouse.GetPosition(window).X - stick.TextureRect.Width * 0.5f, stick.Position.Y);

                    window.Draw(ball.sprite);
                    window.Draw(stick);

                    for (int i = 0; i < blueBlocks.Length; i++)
                    {
                        window.Draw(blueBlocks[i]);
                    }
                    for (int i = 0; i < redBlocks.Length; i++)
                    {
                        window.Draw(redBlocks[i]);
                    }

                    if (count == 0)
                    {
                        
                         if (score > record)
                         {
                             record = score;
                         }
                         level++;
                         speedBoll++;
                        scanc = 3;

                       ball.Stop(0, new Vector2f(0, 0));
                   
                         while (true)
                         {
                              window.Clear();
                              window.Draw(text5);
                              window.Draw(text6);

                              window.Display();
                              if (Keyboard.IsKeyPressed(Keyboard.Key.R) == true)
                                   {

                                       break;

                                   }
                         }

                        SetStartPosition();


                     }

                    if(ball.sprite.Position.Y > 580)
                     {

                        if (scanc <= 1)
                        {
                            if (score > record)
                            {
                                record = score;
                            }
                            score = 0;
                            scanc = 4;
                            level = 1;
                            speedBoll = 5;
                            count = 0;
                            ball.Stop(0, new Vector2f(0, 0));

                            while (true)
                            {
                                
                                window.Clear();
                                window.Draw(text9);
                                window.Draw(text6);

                                window.Display();
                                if (Keyboard.IsKeyPressed(Keyboard.Key.R) == true)
                                {

                                    break;

                                }
                            }

                            SetStartPosition();
                        }

                                  if (score > record)
                                  {
                                      record = score;
                                  }
                                  scanc--;
                                  count = 0;
                                  ball.Stop(0, new Vector2f(0, 0));

                                  while (true)
                                  {
                                      window.Clear();
                                      window.Draw(text8);
                                      window.Draw(text6);

                                      window.Display();
                                      if (Keyboard.IsKeyPressed(Keyboard.Key.R) == true)
                                      {

                                          break;

                                      }
                                  }

                                  SetStartPosition();

                     }
                    

                    window.Draw(text1);
                    window.Draw(text2);
                    window.Draw(text3);
                    window.Draw(text4);
                     window.Draw(text7);

                    window.Display();


                }


        }

        private static void Window_Closed(object sender, EventArgs e)
        {
            window.Close();
        }
    }
}
