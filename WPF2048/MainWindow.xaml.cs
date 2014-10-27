using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.Windows.Media.Animation;

namespace WPF2048
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Storyboard myStoryboard;
        BlockController blockController;
        DoubleAnimation doubleAnimation;
        int score;
        Button[,] btnArray;
        public MainWindow()
        {
            InitializeComponent();
            doubleAnimation = new DoubleAnimation();
            doubleAnimation.Duration = TimeSpan.FromSeconds(0.2);
            doubleAnimation.From = 48;
            doubleAnimation.To = 75;
            myStoryboard = new Storyboard();
            myStoryboard.Children.Add(doubleAnimation);
            doubleAnimation.AutoReverse = true;
            newGame();
        }

        private void newGame()
        {
            blockController = new BlockController();
            score = 0;
            initBlocks();
            moveBlocks();
        }

        private void initBlocks()
        {
            blockController.initBlocks();
        }

        private void moveBlocks()
        {
            drawNewBlocks();
            combineAnimation();
            showScore();
            isDead();
        }

        private void isDead()
        {
            if (!blockController.canMove(BlockController.Direction.Down)&&
                !blockController.canMove(BlockController.Direction.Up)&&
                !blockController.canMove(BlockController.Direction.Left)&&
                !blockController.canMove(BlockController.Direction.Right)
                )
            {
                MessageBox.Show("Game over! Your score: " + score);
            }
        }

        private void showScore()
        {
            lbScore.Content = score;
        }

        private void drawNewBlocks()
        {
            grid.Children.Clear();
            Block[,] blocks = blockController.blocks;
            btnArray = new Button[4, 4];
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (blocks[row, col].num == 0)
                    {
                        continue;
                    }
                    Button btn = new Button();
                    //btn.IsEnabled = false;
                    btn.Content = blocks[row, col].num.ToString();
                    btn.FontSize = 48;
                    btn.Foreground = Brushes.SkyBlue;
                    btn.Background = BlockColor.getBlockColorBrush(blocks[row, col]);
                    Grid.SetColumn(btn, col);
                    Grid.SetRow(btn, row);
                    grid.Children.Add(btn);
                    btn.Name = "btn" + row + col;
                    try
                    {
                        this.UnregisterName(btn.Name);
                    }
                    catch { } //try Unregister 
                    this.RegisterName(btn.Name, btn);
                    btnArray[row, col] = btn;
                }
            }
        }
        private void combineAnimation()
        {
            Block[,] oldBlocks = blockController.oldBlocks;
            bool flag = false;
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (oldBlocks[row, col].status == Block.BlockStatus.Combined)
                    {
                        Button btn = btnArray[row, col];
                        Storyboard.SetTargetName(doubleAnimation, btn.Name);
                        Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(Button.FontSizeProperty));
                        flag = true;
                    }
                }
            }
            if (flag)
            {
                myStoryboard.Begin(this);
            }
        }
        private void tryUp()
        {
            if (blockController.canMove(BlockController.Direction.Up))
            {
                score += blockController.move(BlockController.Direction.Up);
                moveBlocks();
                printBeforeMovedBlocks();
            }
        }

        private void tryDown()
        {
            if (blockController.canMove(BlockController.Direction.Down))
            {
                score += blockController.move(BlockController.Direction.Down);
                moveBlocks();
                printBeforeMovedBlocks();
            }
        }

        private void tryLeft()
        {
            if (blockController.canMove(BlockController.Direction.Left))
            {
                score += blockController.move(BlockController.Direction.Left);
                moveBlocks();
                printBeforeMovedBlocks();
            }
        }

        private void tryRight()
        {
            if (blockController.canMove(BlockController.Direction.Right))
            {
                score += blockController.move(BlockController.Direction.Right);
                moveBlocks();
                printBeforeMovedBlocks();
            }
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    tryUp();
                    break;
                case Key.Down:
                    tryDown();
                    break;
                case Key.Right:
                    tryRight();
                    break;
                case Key.Left:
                    tryLeft();
                    break;
                default:
                    break;
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            newGame();
        }

        // For debug
        private void printBeforeMovedBlocks()
        {
            Debug.WriteLine("------------ Old ------------");
            Block[,] blocks = blockController.oldBlocks;
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    Debug.Write(blocks[row, col].movesteps.ToString() + " ");
                }
                Debug.Write("\n");
            }
        }
    }
}
