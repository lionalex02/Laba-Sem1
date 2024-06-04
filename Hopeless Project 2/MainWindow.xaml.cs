using System;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using RPN.Logic;

namespace Hopeless_Project_2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            scaleSlider.ValueChanged += scaleSliderValueChanged;
            sbHorizontal.ValueChanged += SbHorizontalValueChanged;
            sbVertical.ValueChanged += SbVerticalValueChanged;
            sbStep.ValueChanged += SbStepValueChanged;
        }

        private void scaleSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) // Масштаб
        {
            string expression = tbExpression.Text.ToLower().Replace(".", ",");
            DrawGraph(expression, scaleSlider.Value, sbHorizontal.Value, sbVertical.Value, sbStep.Value);
        }
        private void SbStepValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) // Плотность точек
        {
            string expression = tbExpression.Text.ToLower().Replace(".", ",");
            DrawGraph(expression, scaleSlider.Value, sbHorizontal.Value, sbVertical.Value, sbStep.Value);
        }

        private void SbHorizontalValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) // Горизонтальный ползунок
        {
            string expression = tbExpression.Text.ToLower().Replace(".", ",");
            DrawGraph(expression, scaleSlider.Value, sbHorizontal.Value, sbVertical.Value, sbStep.Value);
        }

        private void SbVerticalValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) // Вертикальный ползунок
        {
            string expression = tbExpression.Text.ToLower().Replace(".", ",");
            DrawGraph(expression, scaleSlider.Value, sbHorizontal.Value, sbVertical.Value, sbStep.Value);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string expression = tbExpression.Text.ToLower().Replace(".", ",");
            double horizontal = sbHorizontal.Value;
            double vertical = sbVertical.Value;
            double scale = scaleSlider.Value;
            double step = sbStep.Value;

            DrawGraph(expression, scale, horizontal, vertical, step);
        }
        private void DrawGraph(string expression, double scale, double horizontal, double vertical, double step)
        {
            canvas.Children.Clear();
            double start = (-canvas.ActualWidth / 2) ;
            double end = (canvas.ActualHeight / 2) ;
            // double step = 1.0 - (Math.Round(scale) - 1.0) * 0.9 / 99.0;

            Line xAxis = new Line
            {
                X1 = 0,
                Y1 = (canvas.ActualHeight / 2) + vertical * scale,
                X2 = canvas.ActualWidth,
                Y2 = (canvas.ActualHeight / 2) + vertical * scale,
                Stroke = Brushes.Black
            };
            canvas.Children.Add(xAxis);

            Line yAxis = new Line
            {
                X1 = (canvas.ActualWidth / 2) + horizontal * scale,
                Y1 = 0,
                X2 = (canvas.ActualWidth / 2) + horizontal * scale,
                Y2 = canvas.ActualHeight,
                Stroke = Brushes.Black
            };
            canvas.Children.Add(yAxis);

            for (double x = start ; x <= end; x += 1)
            {
                Line tick = new Line
                {
                    X1 = x * scale + ((canvas.ActualWidth / 2) + horizontal * scale),
                    Y1 = ((canvas.ActualHeight / 2) + vertical * scale) - scale / 5,
                    X2 = x * scale + ((canvas.ActualWidth / 2) + horizontal * scale),
                    Y2 = ((canvas.ActualHeight / 2) + vertical * scale) + scale / 5,
                    Stroke = Brushes.Black
                };
                canvas.Children.Add(tick);
            }

            for (double y = start; y <= end; y += 1)
            {
                Line tick = new Line
                {
                    X1 = ((canvas.ActualWidth / 2) + horizontal * scale) - scale / 5,
                    Y1 = -y * scale + ((canvas.ActualHeight / 2) + vertical * scale),
                    X2 = ((canvas.ActualWidth / 2) + horizontal * scale) + scale / 5,
                    Y2 = -y * scale + ((canvas.ActualHeight / 2) + vertical * scale),
                    Stroke = Brushes.Black
                };
                canvas.Children.Add(tick);
            }
            Calculator calculator = new Calculator();
            Nullable<Point> previousPoint = null;

            for (double x = start; x <= end; x += step)
            {
                double y = calculator.Resulting(expression, x);


                if (double.IsNaN(y))
                {
                    previousPoint = null;
                    continue;
                }

                y = -y * scale + canvas.ActualHeight / 2.0;

                Ellipse ellipse = new Ellipse
                {
                    Width = 2,
                    Height = 2,
                    Fill = Brushes.Black
                };

                Canvas.SetLeft(ellipse, x * scale + ((canvas.ActualWidth / 2) + horizontal * scale));
                Canvas.SetTop(ellipse, y + vertical* scale );

                canvas.Children.Add(ellipse);

                if (previousPoint.HasValue)
                {
                    Line line = new Line
                    {
                        X1 = previousPoint.Value.X,
                        Y1 = previousPoint.Value.Y,
                        X2 = Canvas.GetLeft(ellipse),
                        Y2 = Canvas.GetTop(ellipse),
                        Stroke = Brushes.Red
                    };

                    canvas.Children.Add(line);
                }

                previousPoint = new Point(Canvas.GetLeft(ellipse), Canvas.GetTop(ellipse));
            }
        }
    }
}


