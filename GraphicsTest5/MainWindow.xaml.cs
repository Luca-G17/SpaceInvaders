using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
        public MainWindow()
        {
            InitializeComponent();
            gameTimer.Tick += GameLoop;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();
            MyCanvas.Focus();
            playerobj = new PlayerObject(72, 42, 400, 300, MyCanvas);
            for (int i = 0; i < 15; i++)
            {
                enemies.Add(new EnemyObject(30, 30, 50 + 35 * i, 50, MyCanvas));
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
        private void EnemyCollision()
        {
            int first_enemy = 0;
            int last_enemy = -1;
            bool enemy_found = false;
            foreach (EnemyObject enemy in enemies)
            {
                if (enemy.Visible)
                {
                    last_enemy += 1;
                    if (!enemy_found)
                    {
                        first_enemy = enemies.IndexOf(enemy);
                        enemy_found = true;
                    }
                }
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
                            //enemy.rect.Fill = Brushes.Yellow;
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
                        playerobj.rect.Fill = Brushes.Green;
                    }
                }
            }
           
        }

    }
}
