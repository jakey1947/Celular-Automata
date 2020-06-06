using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Celular_Automata
{
    static class ArrayUtilities
    {
        #region Remove From Array
        /// <summary>
        /// build array until removed object at index, then continue with offset of 1
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="index"></param>
        static public T[] RemoveFromArray<T>(this T[] array, int index)
        {
            T[] tempArray = array;

            array = new T[array.Length - 1];
            for (int i = 0; i < tempArray.Length - 1; i++)
            {
                if (i < index)
                    array[i] = tempArray[i];
                else
                    array[i] = tempArray[i + 1];
            }

            return array;
        }

        /// <summary>
        /// build array until removed object, then continue with offset of 1
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="item"></param>
        /// <param name="startFromEnd"></param>
        static public T[] RemoveFromArray<T>(this T[] array, T item, bool startFromEnd = false) 
        {
            T[] tempArray = array;

            int index = array.GetIndexOf(item, startFromEnd);

            array = new T[array.Length - 1];
            for (int i = 0; i < tempArray.Length - 1; i++)
            {
                if (i < index)
                    array[i] = tempArray[i];
                else
                    array[i] = tempArray[i + 1];
            }

            return array;
        }

        /// <summary>
        /// build array until removed object, then continue with offset of 1
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        static public T[] RemoveFromArray<T>(this T[] array) 
        {
            T[] tempArray = array;

            array = new T[array.Length - 1];
            for (int i = 0; i < tempArray.Length; i++)
            {
                array[i] = tempArray[i];
            }

            return array;
        }
        /// <summary>
        /// build referenced array until removed object at index, then continue with offset of 1
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="index"></param>
        static public T[] RemoveFromArray<T>(ref T[] array, int index) 
        {

            if (array.Length <= index)
                throw new Exception("Index out of bounds of the array");

            T[] tempArray = array;

            array = new T[array.Length - 1];
            for (int i = 0; i < tempArray.Length - 1; i++)
            {
                if (i < index)
                    array[i] = tempArray[i];
                else
                    array[i] = tempArray[i + 1];
            }

            return array;
        }

        /// <summary>
        /// build referenced array until removed object, then continue with offset of 1
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="item"></param>
        /// <param name="startFromEnd"></param>
        static public T[] RemoveFromArray<T>(ref T[] array, T item, bool startFromEnd = false)
        {
            T[] tempArray = array;

            int index = GetIndexOf(ref array, item, startFromEnd);

            array = new T[array.Length - 1];
            for (int i = 0; i < tempArray.Length - 1; i++)
            {
                if (i < index)
                    array[i] = tempArray[i];
                else
                    array[i] = tempArray[i + 1];
            }

            return array;
        }

        /// <summary>
        /// build referenced array until removed object, then continue with offset of 1
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        static public void RemoveFromArray<T>(ref T[] array) 
        {
            T[] tempArray = array;

            array = new T[array.Length - 1];
            for (int i = 0; i < tempArray.Length; i++)
            {
                array[i] = tempArray[i];
            }
        }
        #endregion

        #region Add To Array
        /// <summary>
        /// build array with new object at the end of the array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="item"></param>
        /// <returns>The newly added object</returns>
        static public T AddToArray<T>(this T[] array, T item) 
        {
            if (array == null)
                array = new T[0];
            T[] tempArray = array;
            array = new T[array.Length + 1];
            for (int i = 0; i < tempArray.Length; i++)
            {
                array[i] = tempArray[i];
            }
            array[array.Length - 1] = item;

            return array.GetLast();
        }

        /// <summary>
        /// build array with empty object at the end of the array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns>The newly added object</returns>
        static public T AddToArray<T>(this T[] array)
        {
            if (array == null)
                array = new T[0];
            T[] tempArray = array;
            array = new T[array.Length + 1];
            for (int i = 0; i < tempArray.Length; i++)
            {
                array[i] = tempArray[i];
            }
            return array.GetLast();
        }

        /// <summary>
        /// build array with new object at the index location in the array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="item"></param>
        /// <param name="index"></param>
        /// <returns>The newly added object</returns>
        static public T AddToArray<T>(this T[] array, T item, int index) 
        {
            if (array == null)
                array = new T[0];
            T[] tempArray = array;
            array = new T[array.Length + 1];
            for (int i = 0; i < tempArray.Length + 1; i++)
            {
                if (i < index)
                    array[i] = tempArray[i];
                else if (i == index)
                    array[i] = item;
                else
                    array[i] = tempArray[i - 1];
            }
            //array[array.Length - 1] = item;
            return array[index];
        }

        /// <summary>
        /// build array with new object at the end of the referenced array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="item"></param>
        /// <returns>The newly added object</returns>
        static public T AddToArray<T>(ref T[] array, T item) 
        {
            if (array == null)
                array = new T[0];
            T[] tempArray = array;
            array = new T[array.Length + 1];
            for (int i = 0; i < tempArray.Length; i++)
            {
                array[i] = tempArray[i];
            }
            array[array.Length - 1] = item;
            return array.GetLast();
        }

        /// <summary>
        /// build array with an empty object at the end of the referenced array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns>The newly added object</returns>
        static public T AddToArray<T>(ref T[] array) 
        {
            if (array == null)
                array = new T[0];
            T[] tempArray = array;
            array = new T[array.Length + 1];
            for (int i = 0; i < tempArray.Length; i++)
            {
                array[i] = tempArray[i];
            }

            return array.GetLast();
        }

        /// <summary>
        /// build array with new object at the index location in the referenced array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="item"></param>
        /// <param name="index"></param>
        /// <returns>The newly added object</returns>
        static public T AddToArray<T>(ref T[] array, T item, int index)
        {
            if (array == null)
                array = new T[0];
            T[] tempArray = array;
            array = new T[array.Length + 1];
            for (int i = 0; i < tempArray.Length + 1; i++)
            {
                if (i < index)
                    array[i] = tempArray[i];
                else if (i == index)
                    array[i] = item;
                else
                    array[i] = tempArray[i - 1];
            }
            //array[array.Length - 1] = item;
            return array[index];
        }
        #endregion

        /// <summary>
        /// Returns the index of the this item in the array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="item"></param>
        /// <param name="startFromEnd"></param>
        /// <returns></returns>
        static public int GetIndexOf<T>(this T[] array, T item, bool startFromEnd = false)
        {
            int index = 0;
            int itemHash = item.GetHashCode();
            bool foundIndex = false;

            if (!startFromEnd)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i].GetHashCode() == itemHash)
                    {
                        index = i;
                        foundIndex = true;
                        break;
                    }
                }
            }
            else
            {
                for (int i = array.Length - 1; i >= 0; i--)
                {
                    if (array[i].GetHashCode() == itemHash)
                    {
                        index = i;
                        foundIndex = true;
                        break;
                    }
                }
            }

            if (foundIndex)
                return index;
            else
                throw new Exception("Item does not exist in the context of the array");
        }

        /// <summary>
        /// Get an items index in an array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="item"></param>
        /// <param name="startFromEnd"></param>
        /// <returns>index of item as int</returns>
        static public int GetIndexOf<T>(ref T[] array, T item, bool startFromEnd = false)
        {
            int index = 0;
            int itemHash = item.GetHashCode();
            bool foundIndex = false;
            lock (array)
            {
                if (!startFromEnd)
                {
                    for (int i = 0; i < array.Length; i++)
                    {
                        if (array[i].GetHashCode() == itemHash)
                        {
                            index = i;
                            foundIndex = true;
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = array.Length - 1; i >= 0; i--)
                    {
                        if (array[i].GetHashCode() == itemHash)
                        {
                            index = i;
                            foundIndex = true;
                            break;
                        }
                    }
                }
            }

            if (foundIndex)
                return index;
            else
                throw new Exception("Item does not exist in the context of the array");
        }

        /// <summary>
        /// Remove item from index (oldIndex) and add it to (newIndex)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="oldIndex"></param>
        /// <param name="newIndex"></param>
        static public void MoveItem<T>(this T[] array, int oldIndex, int newIndex) 
        {
            T temp = array[oldIndex];
            RemoveFromArray(ref array, oldIndex);
            AddToArray(ref array, temp, newIndex - 1);
        }
        /// <summary>
        /// remove item from index (oldIndex) and add it to the end
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="oldIndex"></param>
        static public void MoveItem<T>(this T[] array, int oldIndex)
        {
            T temp = array[oldIndex];
            RemoveFromArray(ref array, oldIndex);
            AddToArray(ref array, temp);
        }

        /// <summary>
        /// remove item from index (oldIndex) and add it to (newIndex)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="oldIndex"></param>
        /// <param name="newIndex"></param>
        static public void MoveItem<T>(ref T[] array, int oldIndex, int newIndex)
        {
            T temp = array[oldIndex];
            RemoveFromArray(ref array, oldIndex);
            AddToArray(ref array, temp, newIndex - 1);
        }

        /// <summary>
        /// remove item from index (oldIndex) and add it to the end
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="oldIndex"></param>
        static public void MoveItem<T>(ref T[] array, int oldIndex) 
        {
            T temp = array[oldIndex];
            RemoveFromArray(ref array, oldIndex);
            AddToArray(ref array, temp);
        }

        /// <summary>
        /// Returns the last object in the given array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        static public T GetLast<T>(this T[] array)
        {
            if (array.Length > 0)
                return array[array.Length - 1];
            else
                throw new Exception("Given array is empty");
        }
    }
}
