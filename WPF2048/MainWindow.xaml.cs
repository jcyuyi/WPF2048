﻿using System;
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

namespace WPF2048
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        BlockController blockController;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void newGame()
        {
            blockController = new BlockController();
            initBlocks();
            drawBlocks();
        }
        private void initBlocks()
        {
            blockController.initBlocks();
        }
        private void drawBlocks()
        {
            grid.Children.Clear();
            Block[,] blocks = blockController.blocks;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (blocks[i, j].num == 0)
                    {
                        continue;
                    }
                    Button btn = new Button();
                    //btn.IsEnabled = false;
                    btn.Content = blocks[i, j].num.ToString();
                    btn.FontSize = 48;
                    btn.Foreground = Brushes.SkyBlue;
                    btn.Background = BlockColor.getBlockColorBrush(blocks[i,j]);
                    Grid.SetColumn(btn, j);
                    Grid.SetRow(btn, i);
                    grid.Children.Add(btn);
                }
            }
        }

        private void tryUp()
        {
            if (blockController.canMove(BlockController.Direction.Up))
            {
                blockController.move(BlockController.Direction.Up);
                drawBlocks();
                printBeforeMovedBlocks();
            }
        }

        private void tryDown()
        {
            if (blockController.canMove(BlockController.Direction.Down))
            {
                blockController.move(BlockController.Direction.Down);
                drawBlocks();
                printBeforeMovedBlocks();
            }
        }

        private void tryLeft()
        {
            if (blockController.canMove(BlockController.Direction.Left))
            {
                blockController.move(BlockController.Direction.Left);
                drawBlocks();
                printBeforeMovedBlocks();
            }
        }

        private void tryRight()
        {
            if (blockController.canMove(BlockController.Direction.Right))
            {
                blockController.move(BlockController.Direction.Right);
                drawBlocks();
                printBeforeMovedBlocks();
            }
        }

        private void undoMove()
        {

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.U:
                    undoMove();
                    break;
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

        private void printBeforeMovedBlocks()
        {
            Debug.WriteLine("-------------------------------");
            Block[,] blocks = blockController.beforeMovedBlocks;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Debug.Write(blocks[i, j].movesteps.ToString() + " ");
                }
                Debug.Write("\n");
            }
        }
    }
}
