namespace TpSort
{
    public static class TopologicalSort
    {
        /// <summary>
        /// Topologically sorts objects
        /// </summary>
        /// <typeparam name="T">Type of objects to be sorted</typeparam>
        /// <param name="pairs">Array of pairs. Pairs contain objects to sort</param>
        /// <returns>Array of topologically sorted objects. Array may contain nulls if input was incorrect</returns>
        public static T[] Sort<T>(Pair<T>[] pairs)
        {
            List<T> inputs = new List<T>();
            T[] result;
            Element[] elements;
            SucNext? p;
            int f, r;
            int n;
            int[] count;
            int[] qlink;

            foreach (Pair<T> pair in pairs)
            {
                if(!inputs.Contains(pair.Larger))
                {
                    inputs.Add(pair.Larger);
                }
                if(!inputs.Contains(pair.Smaller))
                {
                    inputs.Add(pair.Smaller);
                }
            }

            elements = new Element[inputs.Count + 1];
            count = new int[inputs.Count + 1];
            qlink = new int[inputs.Count + 1];

            n = inputs.Count;
            result = new T[n];

            foreach(Pair<T> pair in pairs)
            {
                int j = inputs.IndexOf(pair.Smaller) + 1;
                int k = inputs.IndexOf(pair.Larger) + 1;


                if (elements[j] == null)
                {
                    elements[j] = new Element(j);
                }

                if (elements[k] == null)
                {
                    elements[k] = new Element(k);
                }

                p = new SucNext(k);
                if (elements[j].Top == null)
                {
                    elements[j].Top = p;
                }
                else
                {
#pragma warning disable CS8604
                    findNullNext(elements[j].Top).Next = p;
#pragma warning restore CS8604
                }

                count[k]++;
            }

            r = 0;
            qlink[0] = 0;

            for(int k = 1; k <= n; k++)
            {
                if (count[k] == 0)
                {
                    qlink[r] = k;
                    r = k;
                }
            }
            f = qlink[0];

            int i = 0;
            while (f != 0)
            {
                result[i] = inputs[f - 1];
                i++;
                p = elements[f].Top;

                while (p != null)
                {
                    if (count[p.Suc] != 0)
                    {
                        count[p.Suc]--;
                    }

                    if (count[p.Suc] == 0)
                    {
                        qlink[r] = p.Suc;
                        r = p.Suc;
                    }
                    p = p.Next;
                }
                f = qlink[f];
            }

            return result;





            static SucNext findNullNext(SucNext sucNext)
            {
                if (sucNext.Next == null)
                {
                    return sucNext;
                }
                else
                {
                    return findNullNext(sucNext.Next);
                }
            }
        }



        private class Element
        {
            public Element(int k) : this(k, null) { }

            public Element(int k, SucNext? top)
            {
                K = k;
                Top = top;
            }

            public int K { get; set; }
            public SucNext? Top { get; set; }
        }



        private class SucNext
        {
            public SucNext(int suc) : this(suc, null) { }

            public SucNext(int suc, SucNext? next)
            {
                Suc = suc;
                Next = next;
            }

            public int Suc { get; private set; }
            public SucNext? Next { get; set; }
        }
    }
}
