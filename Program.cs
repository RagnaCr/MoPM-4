using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Mops4
{
    public class BSTNode
    {
        public int Key { get; set; }
        public string Value { get; set; }
        public BSTNode Left { get; set; }
        public BSTNode Right { get; set; }

        public BSTNode(int key, string value)
        {
            Key = key;
            Value = value;
            Left = null;
            Right = null;
        }
    }

    public class BinarySearchTree
    {
        private BSTNode root;

        public BinarySearchTree()
        {
            root = null;
        }

        public void Insert(int key, string value)
        {
            root = InsertRec(root, key, value);
        }

        public void Delete(int key)
        {
            root = DeleteRec(root, key);
        }

        public string Search(int key)
        {
            BSTNode result = SearchRec(root, key);
            return result?.Value;
        }

        private BSTNode InsertRec(BSTNode root, int key, string value)
        {
            if (root == null)
            {
                return new BSTNode(key, value);
            }

            if (key < root.Key)
            {
                root.Left = InsertRec(root.Left, key, value);
            }
            else if (key > root.Key)
            {
                root.Right = InsertRec(root.Right, key, value);
            }
            else
            {
                // Якщо ключ вже існує, оновити значення
                root.Value = value;
            }

            return root;
        }

        private BSTNode DeleteRec(BSTNode root, int key)
        {
            if (root == null)
            {
                return root;
            }

            if (key < root.Key)
            {
                root.Left = DeleteRec(root.Left, key);
            }
            else if (key > root.Key)
            {
                root.Right = DeleteRec(root.Right, key);
            }
            else
            {
                // Вузол з однією або без дітей
                if (root.Left == null)
                {
                    return root.Right;
                }
                else if (root.Right == null)
                {
                    return root.Left;
                }

                // Вузол з двома дітьми: замінити його найменшим вузлом в правому піддереві
                root.Key = MinValue(root.Right);
                root.Right = DeleteRec(root.Right, root.Key);
            }

            return root;
        }

        private int MinValue(BSTNode root)
        {
            int minValue = root.Key;
            while (root.Left != null)
            {
                minValue = root.Left.Key;
                root = root.Left;
            }
            return minValue;
        }

        private BSTNode SearchRec(BSTNode root, int key)
        {
            if (root == null || root.Key == key)
            {
                return root;
            }

            if (key < root.Key)
            {
                return SearchRec(root.Left, key);
            }

            return SearchRec(root.Right, key);
        }

        public int Count()
        {
            return CountRec(root);
        }

        private int CountRec(BSTNode root)
        {
            if (root == null)
            {
                return 0;
            }
            return 1 + CountRec(root.Left) + CountRec(root.Right);
        }
        public void PrintElemnts()
        {
            PrintRec(root);
        }
        private void PrintRec(BSTNode root)
        {
            if (root != null)
            {
                PrintRec(root.Left);
                Console.WriteLine($"Key: {root.Key}, Value: {root.Value}");
                PrintRec(root.Right);
            }
        }
    }

    class Program
    {
        static void Main()
        {
            Testing.TestMethod1(false);//обязательно из-за инициализации
            Testing.TestMethod2();
            Testing.TestMethod3();
            Testing.TestMethod4();
            Testing.TestMethod5();
            Testing.TestMethod6();
            Testing.TestMethod7();


        }
    }
    class Testing
    {
        static BinarySearchTree bst = new BinarySearchTree();
        static bool Info = true; // вывод доп информации
        private static void Init()
        {
            Dictionary<int, string> map = new Dictionary<int, string>();
            map.Add(12, "Twelve");
            map.Add(13, "Thirteen");
            map.Add(14, "Fourteen");
            map.Add(15, "Fifteen");
            map.Add(16, "Sixteen");
            map.Add(17, "Seventeen");
            map.Add(18, "Eighteen");
            map.Add(19, "Nineteen");
            map.Add(20, "Twenty");
            map.Add(21, "Twenty-One");
            map.Add(22, "Twenty-Two");
            map.Add(23, "Twenty-Three");
            map.Add(24, "Twenty-Four");
            map.Add(25, "Twenty-Five");
            map.Add(26, "Twenty-Six");
            map.Add(27, "Twenty-Seven");
            map.Add(28, "Twenty-Eight");
            map.Add(29, "Twenty-Nine");
            map.Add(30, "Thirty");
            map.Add(31, "Thirty-One");

            bst = new BinarySearchTree();
            foreach (var item in map){ bst.Insert(item.Key, item.Value); }
        }
        //З даних у структурі створити дерево BSTNode, обійти дерево і перевірити, що кількість вузлів дорівнює 20
        public static void TestMethod1(bool info)
        {
            int expected = 20;
            int actual;

            Info = info;
            Init();
            if (Info){ bst.PrintElemnts(); Console.WriteLine(); }
            actual = bst.Count();
            Assert.AreEqual<int>(expected, actual);
        }
        //- Знайти кілька існуючих елементів за ключами, вивести їх значення.
        public static void TestMethod2()
        {
            string[] expected = { "Twenty", "Twenty-Nine", "Thirty" };
            List<string> actual = new List<string>();

            int[] existingKeys = { 20, 29, 30 };
            foreach (int key in existingKeys)
            {
                string result = bst.Search(key);
                if (Info) { Console.WriteLine($"Search for key {key}: {result}"); }
                actual.Add(result);
            }
            if (Info) { Console.WriteLine(); }
            Assert.AreEqual<string[]>(expected, actual.ToArray());
        }
        //- Спробувати знайти неіснуючий елемент, переконатися, що структура повертає Null.
        public static void TestMethod3()
        {
            string expected = "Null";
            string actual;

            int nonExistingKey = 99;
            actual = bst.Search(nonExistingKey);
            if (Info) { Console.WriteLine($"Search for non-existing key {nonExistingKey}: {actual ?? "Null"}\n"); }

            Assert.AreEqual<object>(expected, actual ?? "Null");
        }
        /*- Вставити новий елемент із цим пропущеним ключем.
		  - Провести пошук по цьому ключу, переконатися, що елемент присутній у дереві
		  - Вставити елемент із вже існуючим ключем, але іншим значенням рядка
		  - провести обхід дерева, перевірити, що кількість вузлів дорівнює 20*/
        public static void TestMethod4()
        {
            int expected = 20;
            int actual;

            bst.Delete(21);
            bst.Insert(21, "New Twenty-One");
            actual = bst.Count();
            if (Info) { bst.PrintElemnts(); Console.WriteLine(); }

            Assert.AreEqual<int>(expected, actual);
        }
        //- Провести пошук по цьому ключу і переконатися, що значення оновилося
        public static void TestMethod5()
        {
            string expected = "New Twenty-One";
            string actual;

            actual = bst.Search(21);
            if (Info) { Console.WriteLine($"Key: 21 Value: {actual}"); }
            Assert.AreEqual<object>(expected, actual ?? "Null");
        }
        /*- Видалити елемент із ключем із середини списку.
		  - провести обхід дерева, перевірити, що кількість вузлів дорівнює 19*/
        public static void TestMethod6()
        {
            Init(); // заново инициализируем ибо проводили операции над деревом
            int expected = 19;
            int actual;

            bst.Delete(20);
            actual = bst.Count();
            if (Info) { bst.PrintElemnts(); Console.WriteLine(); }
            Assert.AreEqual(expected, actual);
        }
        public static void TestMethod7()
        {
            string expected = "Null";
            string actual;

            int nonExistingKey = 20;
            actual = bst.Search(nonExistingKey);
            if (Info) { Console.WriteLine($"Search for non-existing key {nonExistingKey}: {actual ?? "Null"}\n"); }

            Assert.AreEqual<object>(expected, actual ?? "Null");
        }
    }
    class Assert
    {
        public static void AreEqual<T>(T expected, T actual, string message = null)
        {
            string callingMethodName = new StackTrace().GetFrame(1).GetMethod().Name;

            if (typeof(IEnumerable).IsAssignableFrom(typeof(T)))
            {
                // Обработка сравнения массивов или коллекций
                if (AreCollectionsEqual(expected as IEnumerable, actual as IEnumerable))
                {
                    Console.WriteLine($"{callingMethodName} - Test Passed: Collections are equal");
                }
                else
                {
                    Console.WriteLine($"{callingMethodName} - Test Failed: Collections are not equal");
                    if (!string.IsNullOrEmpty(message))
                    {
                        Console.WriteLine($"Message: {message}");
                    }
                }
            }
            else if (object.Equals(expected, actual))
            {
                // Обработка сравнения простых типов
                Console.WriteLine($"{callingMethodName} - Test Passed: Expected '{expected}', Actual '{actual}'");
            }
            else
            {
                Console.WriteLine($"{callingMethodName} - Test Failed: Expected '{expected}', Actual '{actual}'");
                if (!string.IsNullOrEmpty(message))
                {
                    Console.WriteLine($"Message: {message}");
                }
            }
        }

        private static bool AreCollectionsEqual(IEnumerable expected, IEnumerable actual)
        {
            if (expected == null && actual == null)
            {
                return true;
            }

            if (expected == null || actual == null)
            {
                return false;
            }

            IEnumerator expectedEnumerator = expected.GetEnumerator();
            IEnumerator actualEnumerator = actual.GetEnumerator();

            while (expectedEnumerator.MoveNext() && actualEnumerator.MoveNext())
            {
                if (!object.Equals(expectedEnumerator.Current, actualEnumerator.Current))
                {
                    return false;
                }
            }

            return !expectedEnumerator.MoveNext() && !actualEnumerator.MoveNext();
        }
    }
}
