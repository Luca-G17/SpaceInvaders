using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace GraphicsTest5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PlayerObject playerobj;
        DispatcherTimer gameTimer = new DispatcherTimer();
        List<Bullet> bullets = new List<Bullet>();
        List<EnemyObject> enemies = new List<EnemyObject>();
        int down_timer = 0;
        Random random = new Random();
        ImageBrush playerSkin = new ImageBrush();
        bool end_game = false;
        public MainWindow()
        {
            InitializeComponent();
            gameTimer.Tick += GameLoop;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();
            MyCanvas.Focus();
            playerobj = new PlayerObject(72, 42, 400, 300, MyCanvas);
            int x = 0;
            for (int i = 0; i < 15; i++)
            {
                x += 1;
                if (x == 8)
                {
                    x = 1;
                }
                enemies.Add(new EnemyObject(30, 30, 50 + 35 * i, 50, MyCanvas, x));
            }
        }
        private void GameLoop(object sender, EventArgs e)
        {
            EnemyCollision();
            EnemyMoveDown();
            foreach (EnemyObject enemy in enemies)
            {
                if (enemy.Visible)
                {
                    enemy.Move();
                    Canvas.SetLeft(enemy.rect, enemy.GetLeft);
                    Canvas.SetTop(enemy.rect, enemy.GetTop);
                    EnemyShoot(enemy);
                }
            }
            foreach (Bullet bullet in bullets)
            {
                if (bullet.Visible)
                {
                    bullet.Move();
                    Canvas.SetTop(bullet.rect, bullet.GetTop);
                }
            }
            playerobj.Move();
            Canvas.SetLeft(playerobj.rect, playerobj.GetLeft);
            if (end_game)
            {
                Thread.Sleep(1000);
                Application.Current.Shutdown();
            }

        }
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (Math.Abs(playerobj.X_vel) < 15)
            {
                if (e.Key == Key.A)
                {
                    playerobj.X_vel += -2;
                }
                if (e.Key == Key.D)
                {
                    playerobj.X_vel += 2;
                }
            }

        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                bullets.Add(new Bullet(30, 10, playerobj.GetLeft + playerobj.Width / 2 - 5, playerobj.GetTop - 50, MyCanvas, -10));
            }

        }
        private void EnemyShoot(EnemyObject enemy)
        {
            if (playerobj.GetLeft <= enemy.GetLeft + enemy.Width && playerobj.GetLeft + playerobj.Width >= enemy.GetLeft)
            {
                if (random.Next(150) == 5)
                {
                    bullets.Add(new Bullet(30, 10, enemy.GetLeft + enemy.Width / 2 - 5, enemy.GetTop + 40, MyCanvas, 5));
                }
            }
        }
        private void EnemyMoveDown()
        {
            if (down_timer == 5)
            {
                down_timer = 0;
                foreach (EnemyObject enemy in enemies)
                {
                    enemy.GetTop += 40;
                }
            }
        }
        private void CreateATextBlock(string msg, Color color)
        {
            TextBlock txtBlock = new TextBlock();
            txtBlock.Height = 450;
            txtBlock.Width = 800;
            txtBlock.Text = msg;
            txtBlock.FontSize = 150;
            txtBlock.Foreground = new SolidColorBrush(color);
            MyCanvas.Children.Add(txtBlock);
        }

        private void EnemyCollision()
        {
            bool enemy_found = false;
            int first_enemy = 0;
            int last_enemy = 0;
            int enemy_count = 0;
            for (int i = 0; i < enemies.Count(); i++)
            {
                if (enemies[i].Visible)
                {
                    if (!enemy_found)
                    {
                        first_enemy = i;
                        enemy_found = true;
                    }
                    enemy_count += 1;
                }
            }
            for (int i = enemies.Count() - 1; i > -1; i--)
            {
                if (enemies[i].Visible)
                {
                    last_enemy = i;
                    break;
                }
            }
            if (enemy_count == 0 && !end_game)
            {
                CreateATextBlock("YOU WIN", Colors.Green);
                end_game = true;
            }
            if (enemies[last_enemy].GetLeft >= 742 || enemies[first_enemy].GetLeft <= 0)
            {
                down_timer += 1;
                foreach (EnemyObject enemy in enemies)
                {
                    enemy.X_vel *= -1;
                }
            }
        
            foreach (Bullet bullet in bullets)
            {
                foreach (EnemyObject enemy in enemies)
                {
                    if (bullet.GetLeft <= enemy.GetLeft + enemy.Width && bullet.GetLeft + bullet.Width >= enemy.GetLeft && enemy.Visible && bullet.Visible)
                    {
                        if (bullet.GetTop <= enemy.GetTop + enemy.Height && bullet.GetTop + bullet.Height >= enemy.GetTop)
                        {
                            MyCanvas.Children.Remove(enemy.rect);
                            MyCanvas.Children.Remove(bullet.rect);
                            enemy.Visible = false;
                            bullet.Visible = false;
                        }
                    }
                }
                if (bullet.GetLeft <= playerobj.GetLeft + playerobj.Width && bullet.GetLeft + bullet.Width >= playerobj.GetLeft && bullet.Visible)
                {
                    if (bullet.GetTop <= playerobj.GetTop + playerobj.Height && bullet.GetTop + bullet.Height >= playerobj.GetTop)
                    {
                        MyCanvas.Children.Remove(bullet.rect);
                        bullet.Visible = false;
                        CreateATextBlock("YOU DIED", Colors.Red);
                        end_game = true;
                    }
                }
            }
           
        }

    }
}
