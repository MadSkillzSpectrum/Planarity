using System.Collections.Generic;
using System.Linq;

namespace Planarity
{
    /// <summary>
    /// Граф
    /// </summary>
    public class Graph
    {
        ///<summary>Вершины графа</summary>
        private List<Vert> _verts = new List<Vert>();

        ///<summary>
        /// Добавляет вершину в граф
        /// </summary>
        ///<returns>Возвращает добавленную вершину</returns>
        public Vert AddVert(Vert vert)
        {
            _verts.Add(vert);
            return vert;
        }

        ///<summary>
        /// Получить вершины графа
        /// </summary>
        ///<returns>Возвращает список вершин графа</returns>
        public List<Vert> GetVerts()
        {
            return _verts;
        }

        /// <summary>
        /// Чистит граф
        /// </summary>
        public void New()
        {
            _verts.Clear();
        }

        /// <summary>
        /// Проверка графа на планарность
        /// </summary>
        /// <returns>Возвращает планарен ли граф</returns>
        public bool IsPlanar()
        {
            if (_verts.Count < 5)
                return true;
            if (_verts.Count == 5)
                return !_verts.All(v1 => v1.LinkedVers.Count > 3);
            var combies = Get5Combi(_verts);
            foreach (var l in combies)
            {
                var edges = l.SelectMany(v1 => v1.LinkedVers).Count(v2 => l.Contains(v2));
                if (edges == 20)
                    return false;
            }
            combies = Get6Combi(_verts);
            foreach (var l in combies)
            {
                var firstOfCombi = Get3Combi(l);
                foreach (var l2 in firstOfCombi)
                {
                    var others = l.Where(v => !l2.Contains(v)).ToList();
                    var edges1 = GetEdgesFor3(l2, others);
                    var edges2 = GetEdgesFor3(others, l2);
                    if (edges1 == 3 && edges2 == 3)
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Число связывающих ребер
        /// </summary>
        /// <param name="combi"></param>
        /// <param name="others"></param>
        /// <returns>Возвращает число связывающих граней</returns>
        private int GetEdgesFor3(List<Vert> combi, List<Vert> others)
        {
            return combi.Count(v => v.LinkedVers.Contains(others[0]) && v.LinkedVers.Contains(others[1]) && v.LinkedVers.Contains(others[2]));
        }

        /// <summary>
        /// Получить все подграфы по 5 вершин
        /// </summary>
        /// <param name="allVers">Доступные для проверки вершины</param>
        /// <returns>Возвращает все комбинации по 5 связанных вершин</returns>
        private static List<List<Vert>> Get5Combi(List<Vert> allVers)
        {
            var combies = new List<List<Vert>>();
            for (var i = 0; i < allVers.Count - 4; i++)
                for (var j = i + 1; j < allVers.Count - 3; j++)
                    for (var k = j + 1; k < allVers.Count - 2; k++)
                        for (var l = k + 1; l < allVers.Count - 1; l++)
                            for (var m = l + 1; m < allVers.Count; m++)
                            {
                                var curList = new List<Vert> { allVers[i], allVers[j], allVers[k], allVers[l], allVers[m] };
                                if (!combies.Contains(curList))
                                {
                                    combies.Add(curList);
                                }
                            }
            return combies;
        }

        /// <summary>
        /// Получить все подграфы по 6 вершин
        /// </summary>
        /// <param name="allVers">Доступные для проверки вершины</param>
        /// <returns>Возвращает все комбинации по 6 связанных вершин</returns>
        private static List<List<Vert>> Get6Combi(List<Vert> allVers)
        {
            var combies = new List<List<Vert>>();
            for (var n = 0; n < allVers.Count - 5; n++)
                for (var i = n + 1; i < allVers.Count - 4; i++)
                    for (var j = i + 1; j < allVers.Count - 3; j++)
                        for (var k = j + 1; k < allVers.Count - 2; k++)
                            for (var l = k + 1; l < allVers.Count - 1; l++)
                                for (var m = l + 1; m < allVers.Count; m++)
                                {
                                    var curList = new List<Vert>
                               {
                                   allVers[n],
                                   allVers[i],
                                   allVers[j],
                                   allVers[k],
                                   allVers[l],
                                   allVers[m]
                               };
                                    if (!combies.Contains(curList))
                                    {
                                        combies.Add(curList);
                                    }
                                }
            return combies;
        }

        /// <summary>
        /// Получить все подграфы по 3 вершины
        /// </summary>
        /// <param name="allVers">Доступные для проверки вершины</param>
        /// <returns>Возвращает все комбинации по 3 связанных вершины</returns>
        private static List<List<Vert>> Get3Combi(List<Vert> allVers)
        {
            List<List<Vert>> combies = new List<List<Vert>>();
            for (var k = 0; k < allVers.Count - 2; k++)
                for (var l = k + 1; l < allVers.Count - 1; l++)
                    for (var m = l + 1; m < allVers.Count; m++)
                    {
                        var curList = new List<Vert> { allVers[k], allVers[l], allVers[m] };
                        if (!combies.Contains(curList))
                        {
                            combies.Add(curList);
                        }
                    }
            return combies;
        }
    }
}
