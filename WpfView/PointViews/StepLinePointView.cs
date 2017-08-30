﻿//The MIT License(MIT)

//Copyright(c) 2016 Alberto Rodríguez Orozco & LiveCharts Contributors

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using LiveCharts.Charts;
using LiveCharts.Definitions.Points;
using LiveCharts.Definitions.Series;

namespace LiveCharts.Wpf.PointViews
{
    internal class StepLinePointView : PointView, IStepPointView
    {
        public double DeltaX { get; set; }
        public double DeltaY { get; set; }
        public Line Line1 { get; set; }
        public Line Line2 { get; set; }
        public Path Shape { get; set; }

        public override void Draw(ChartPoint previousDrawn, int index, ISeriesView series, ChartCore chart)
        {
            var invertedMode = ((StepLineSeries) ChartPoint.SeriesView).InvertedMode;

            //if (IsNew)
            {
                if (invertedMode)
                {
                    Line1.X1 = ChartPoint.ChartLocation.X;
                    Line1.X2 = ChartPoint.ChartLocation.X - DeltaX;
                    Line1.Y1 = chart.View.DrawMarginHeight;
                    Line1.Y2 = chart.View.DrawMarginHeight;

                    Line2.X1 = ChartPoint.ChartLocation.X - DeltaX;
                    Line2.X2 = ChartPoint.ChartLocation.X - DeltaX;
                    Line2.Y1 = chart.View.DrawMarginHeight;
                    Line2.Y2 = chart.View.DrawMarginHeight;
                }
                else
                {
                    Line1.X1 = ChartPoint.ChartLocation.X;
                    Line1.X2 = ChartPoint.ChartLocation.X;
                    Line1.Y1 = chart.View.DrawMarginHeight;
                    Line1.Y2 = chart.View.DrawMarginHeight;

                    Line2.X1 = ChartPoint.ChartLocation.X - DeltaX;
                    Line2.X2 = ChartPoint.ChartLocation.X;
                    Line2.Y1 = chart.View.DrawMarginHeight;
                    Line2.Y2 = chart.View.DrawMarginHeight;
                }

                if (Shape != null)
                {
                    Canvas.SetLeft(Shape, ChartPoint.ChartLocation.X - Shape.Width/2);
                    Canvas.SetTop(Shape, chart.View.DrawMarginHeight);
                }
            }

            if (Label != null && double.IsNaN(Canvas.GetLeft(Label)))
            {
                Canvas.SetTop(Label, chart.View.DrawMarginHeight);
                Canvas.SetLeft(Label, ChartPoint.ChartLocation.X);
            }

            if (chart.View.DisableAnimations)
            {
                if (invertedMode)
                {
                    Line1.X1 = ChartPoint.ChartLocation.X;
                    Line1.X2 = ChartPoint.ChartLocation.X - DeltaX;
                    Line1.Y1 = ChartPoint.ChartLocation.Y;
                    Line1.Y2 = ChartPoint.ChartLocation.Y;

                    Line2.X1 = ChartPoint.ChartLocation.X - DeltaX;
                    Line2.X2 = ChartPoint.ChartLocation.X - DeltaX;
                    Line2.Y1 = ChartPoint.ChartLocation.Y;
                    Line2.Y2 = ChartPoint.ChartLocation.Y - DeltaY;
                }
                else
                {
                    Line1.X1 = ChartPoint.ChartLocation.X;
                    Line1.X2 = ChartPoint.ChartLocation.X;
                    Line1.Y1 = ChartPoint.ChartLocation.Y;
                    Line1.Y2 = ChartPoint.ChartLocation.Y - DeltaY;

                    Line2.X1 = ChartPoint.ChartLocation.X - DeltaX;
                    Line2.X2 = ChartPoint.ChartLocation.X;
                    Line2.Y1 = ChartPoint.ChartLocation.Y - DeltaY;
                    Line2.Y2 = ChartPoint.ChartLocation.Y - DeltaY;
                }

                if (Shape != null)
                {
                    Canvas.SetLeft(Shape, ChartPoint.ChartLocation.X - Shape.Width/2);
                    Canvas.SetTop(Shape, ChartPoint.ChartLocation.Y - Shape.Height/2);
                }

                if (Label != null)
                {
                    Label.UpdateLayout();
                    var xl = CorrectXLabel(ChartPoint.ChartLocation.X - Label.ActualWidth * .5, chart);
                    var yl = CorrectYLabel(ChartPoint.ChartLocation.Y - Label.ActualHeight * .5, chart);
                    Canvas.SetLeft(Label, xl);
                    Canvas.SetTop(Label, yl);
                }

                return;
            }

            var animSpeed = chart.View.AnimationsSpeed;

            if (invertedMode)
            {
                Line1.BeginAnimation(Line.X1Property,
                    new DoubleAnimation(ChartPoint.ChartLocation.X, animSpeed));
                Line1.BeginAnimation(Line.X2Property,
                    new DoubleAnimation(ChartPoint.ChartLocation.X - DeltaX, animSpeed));
                Line1.BeginAnimation(Line.Y1Property,
                    new DoubleAnimation(ChartPoint.ChartLocation.Y, animSpeed));
                Line1.BeginAnimation(Line.Y2Property,
                    new DoubleAnimation(ChartPoint.ChartLocation.Y, animSpeed));

                Line2.BeginAnimation(Line.X1Property,
                    new DoubleAnimation(ChartPoint.ChartLocation.X - DeltaX, animSpeed));
                Line2.BeginAnimation(Line.X2Property,
                    new DoubleAnimation(ChartPoint.ChartLocation.X - DeltaX, animSpeed));
                Line2.BeginAnimation(Line.Y1Property,
                    new DoubleAnimation(ChartPoint.ChartLocation.Y, animSpeed));
                Line2.BeginAnimation(Line.Y2Property,
                    new DoubleAnimation(ChartPoint.ChartLocation.Y - DeltaY, animSpeed));
            }
            else
            {
                Line1.BeginAnimation(Line.X1Property,
                    new DoubleAnimation(ChartPoint.ChartLocation.X, animSpeed));
                Line1.BeginAnimation(Line.X2Property,
                    new DoubleAnimation(ChartPoint.ChartLocation.X, animSpeed));
                Line1.BeginAnimation(Line.Y1Property,
                    new DoubleAnimation(ChartPoint.ChartLocation.Y, animSpeed));
                Line1.BeginAnimation(Line.Y2Property,
                    new DoubleAnimation(ChartPoint.ChartLocation.Y - DeltaY, animSpeed));

                Line2.BeginAnimation(Line.X1Property,
                    new DoubleAnimation(ChartPoint.ChartLocation.X - DeltaX, animSpeed));
                Line2.BeginAnimation(Line.X2Property,
                    new DoubleAnimation(ChartPoint.ChartLocation.X, animSpeed));
                Line2.BeginAnimation(Line.Y1Property,
                    new DoubleAnimation(ChartPoint.ChartLocation.Y - DeltaY, animSpeed));
                Line2.BeginAnimation(Line.Y2Property,
                    new DoubleAnimation(ChartPoint.ChartLocation.Y - DeltaY, animSpeed));
            }

            if (Shape != null)
            {
                Shape.BeginAnimation(Canvas.LeftProperty,
                    new DoubleAnimation(ChartPoint.ChartLocation.X - Shape.Width/2, animSpeed));
                Shape.BeginAnimation(Canvas.TopProperty,
                    new DoubleAnimation(ChartPoint.ChartLocation.Y - Shape.Height/2, animSpeed));
            }

            if (Label != null)
            {
                Label.UpdateLayout();
                var xl = CorrectXLabel(ChartPoint.ChartLocation.X - Label.ActualWidth * .5, chart);
                var yl = CorrectYLabel(ChartPoint.ChartLocation.Y - Label.ActualHeight * .5, chart);
                Canvas.SetLeft(Label, xl);
                Canvas.SetTop(Label, yl);
            }

        }

        public override void Erase(ChartCore chart)
        {
            chart.View.RemoveFromDrawMargin(Shape);
            chart.View.RemoveFromDrawMargin(Label);
            chart.View.RemoveFromDrawMargin(Line1);
            chart.View.RemoveFromDrawMargin(Line2);
        }

        public override void OnHover()
        {
            var lineSeries = (StepLineSeries) ChartPoint.SeriesView;
            if (Shape != null) Shape.Fill = Shape.Stroke;
            lineSeries.StrokeThickness = lineSeries.StrokeThickness + 1;
        }

        public override void OnHoverLeave()
        {
            var lineSeries = (StepLineSeries) ChartPoint.SeriesView;
            if (Shape != null)
                Shape.Fill = ChartPoint.Fill == null
                    ? lineSeries.PointForeground
                    : (Brush) ChartPoint.Fill;
            lineSeries.StrokeThickness = lineSeries.StrokeThickness - 1;
        }

        protected double CorrectXLabel(double desiredPosition, ChartCore chart)
        {
            if (desiredPosition + Label.ActualWidth * .5 < -0.1) return -Label.ActualWidth;

            if (desiredPosition + Label.ActualWidth > chart.View.DrawMarginWidth)
                desiredPosition -= desiredPosition + Label.ActualWidth - chart.View.DrawMarginWidth + 2;

            if (desiredPosition < 0) desiredPosition = 0;

            return desiredPosition;
        }

        protected double CorrectYLabel(double desiredPosition, ChartCore chart)
        {
            desiredPosition -= (Shape?.ActualHeight * .5 ?? 0) + Label.ActualHeight * .5 + 2;

            if (desiredPosition + Label.ActualHeight > chart.View.DrawMarginHeight)
                desiredPosition -= desiredPosition + Label.ActualHeight - chart.View.DrawMarginHeight + 2;

            if (desiredPosition < 0) desiredPosition = 0;

            return desiredPosition;
        }
    }
}