using System;
using System.Collections.Generic;
using SadConsole;
using System.Linq;

namespace SadConsole
{
    public static class ConsoleCollectionExtensions
    {
        public static void RemoveAll<T>(this SadConsole.ConsoleCollection _thisCliClec) where T : SadConsole.Console
        {
            foreach (SadConsole.Console cli in _thisCliClec)
                if (cli is T cliAsT)
                    _thisCliClec.Remove(cliAsT);
        }

        public static void RemoveAll<T>(this SadConsole.ConsoleCollection _thisCliClec, out IEnumerable<T> removedElements) where T : SadConsole.Console
        {
            var removed = new List<T>();

            foreach (SadConsole.Console cli in _thisCliClec)
            {
                if (cli is T cliAsT)
                {
                    removed.Add(cliAsT);
                    _thisCliClec.Remove(cli);
                }
            }

            removedElements = removed;
        }

        public static void RemoveAll<T>(this SadConsole.ConsoleCollection _thisCliClec, out ICollection<T> removedElements) where T : SadConsole.Console
        {
            RemoveAll<T>(_thisCliClec, out IEnumerable<T> removedByType);
            removedElements = removedByType.ToList();
        }

        public static void RemoveAll(this SadConsole.ConsoleCollection _thisCliClec)
        {
            foreach (SadConsole.Console cli in _thisCliClec)
                _thisCliClec.Remove(cli);
        }

        public static void RemoveAll(this SadConsole.ConsoleCollection _thisCliClec, out IEnumerable<SadConsole.Console> removedElements)
        {
            removedElements = _thisCliClec.AsEnumerable();
            RemoveAll(_thisCliClec);
        }

        public static void RemoveAll(this SadConsole.ConsoleCollection _thisCliClec, out ICollection<SadConsole.Console> removedElements)
        {
            removedElements = _thisCliClec.ToList();
            RemoveAll(_thisCliClec);
        }
    }
}
