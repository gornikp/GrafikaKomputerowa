using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafikaKomputerowa.Zad9
{
    public static class PointOperations
    {
        public static PointF MovePoint(PointF pointToMove, PointF vectorMoving)
        {
            return new PointF(pointToMove.X + vectorMoving.X, pointToMove.Y - vectorMoving.Y);
        }
        public static PointF RotatePoint(PointF pointToMove, PointF vectorMoving, int alfa)
        {
            float x0 = vectorMoving.X, x = pointToMove.X, y = pointToMove.Y, y0 = vectorMoving.Y;
            float alfaRad = (float)((double)alfa * Math.PI / 180);
            return new PointF((float)(x0 + (x - x0) * Math.Cos(alfaRad) - (y - y0) * Math.Sin(alfaRad)), (float)(y0 + (x - x0) * Math.Sin(alfaRad) + (y - y0) * Math.Cos(alfaRad)));
        }
        public static PointF ScalePoint(PointF pointToMove, PointF vectorMoving, float k)
        {
            float x0 = vectorMoving.X, x = pointToMove.X, y = pointToMove.Y, y0 = vectorMoving.Y;
            return new PointF(x * k + (1 - k) * x0, y * k + (1 - k) * y0);
        }

        public static List<PointF> MovePointList(List<PointF> pointToMove, PointF vectorMoving)
        {
            List<PointF> outputList = new List<PointF>();
            foreach (var point in pointToMove)
            {
                outputList.Add(MovePoint(point, vectorMoving));
            }
            return outputList;
        }
       
        public static List<PointF> RotatePointList(List<PointF> pointToMove, PointF vectorMoving, int alfa)
        {
            List<PointF> outputList = new List<PointF>();
            foreach (var point in pointToMove)
            {
                outputList.Add(RotatePoint(point, vectorMoving, alfa));
            }
            return outputList;
        }      

        public static List<PointF> ScalePointList(List<PointF> pointToMove, PointF vectorMoving, float k)
        {
            List<PointF> outputList = new List<PointF>();
            foreach (var point in pointToMove)
            {
                outputList.Add(ScalePoint(point, vectorMoving, k));
            }
            return outputList;
        }      
    }
}
