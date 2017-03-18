using System.Drawing;

namespace Planarity
{
    /// <summary>
    /// Помощь
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Принадлежит ли точка окружности
        /// </summary>
        /// <param name="x">Центр</param>
        /// <param name="y">Точка</param>
        /// <param name="r">Радиус</param>
        /// <returns>Находится ли точка в окружности</returns>
        public static bool InCircle(Point x,Point y, int r)
        {
            //Math.Pow() строит ряд, лишние затраты для квадрата
            return (x.X-y.X)* (x.X - y.X) + (x.Y - y.Y)* (x.Y - y.Y) <=r*r;
        }
    }
}
