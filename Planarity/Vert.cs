using System.Collections.Generic;
using System.Drawing;

namespace Planarity
{
    /// <summary>
    /// Вершина графа
    /// </summary>
    public class Vert
    {
        /// <summary>
        /// Координаты вершины
        /// </summary>
        public Point Position { get; set; }
        /// <summary>
        /// Радиус вершины
        /// </summary>
        public const int R = 10;
        /// <summary>
        /// Связанные вершины
        /// </summary>
        public List<Vert> LinkedVers = new List<Vert>();
        /// <summary>
        /// Вершина графа
        /// </summary>
        /// <param name="position">Координаты вершины</param>
        public Vert(Point position)
        {
            Position = position;
        }
        /// <summary>
        /// Добавить связанную вершину
        /// </summary>
        /// <param name="vert">Вершина</param>
        public void AddAsLinked(Vert vert)
        {
            LinkedVers.Add(vert);
        }
    }
}
