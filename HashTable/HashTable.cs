using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    /// <summary>
    /// Хеш-таблица.
    /// </summary>
    /// <remarks>
    /// Используется метод цепочек (открытое хеширование).
    /// </remarks>
    public class HashTable
    { 
        /// <summary>
        /// Коллекция хранимых данных.
        /// </summary>
        /// <remarks>
        /// Представляет собой словарь, ключ которого представляет собой хеш ключа хранимых данных,
        /// а значение это список элементов с одинаковым хешем ключа.
        /// </remarks>
        private Dictionary<string, Item> _items = null;

        /// <summary>
        /// Коллекция хранимых данных в хеш-таблице в виде пар Хеш-Значения.
        /// </summary>
        public IReadOnlyCollection<KeyValuePair<string, Item>> Items => _items?.ToList()?.AsReadOnly();

        /// <summary>
        /// Создать новый экземпляр класса HashTable.
        /// </summary>
        public HashTable(int size)
        {
            // Инициализируем коллекцию максимальным количество элементов.
            _items = new Dictionary<string, Item>(size);
        }

        /// <summary>
        /// Добавить данные в хеш таблицу.
        /// </summary>
        /// <param name="key"> Ключ хранимых данных. </param>
        /// <param name="value"> Хранимые данные. </param>
        public void PutPair(object key, object value)
        {
            // Создаем новый экземпляр данных.
            var item = new Item(key, value);

            // Получаем хеш ключа
            var hash = GetHash(item.Key);

            if (_items.ContainsKey(hash))
            {
                //удаляем старый элемент
                _items.Remove(hash);
                // добавляем новый
                _items.Add(hash, item);
            }
            else
            {
                // Добавляем данные в таблицу.
                _items.Add(hash, item);
            }
        }

        /// <summary>
        /// Удалить данные из хеш таблицы по ключу.
        /// </summary>
        /// <param name="key"> Ключ. </param>
        public void Delete(object key)
        {
            // Получаем хеш ключа.
            var hash = GetHash(key);

            // Если значения с таким хешем нет в таблице, 
            // то завершаем выполнение метода.
            if (!_items.ContainsKey(hash))
            {
                return;
            }
            
            _items.Remove(hash);
            
        }

        /// <summary>
        /// Поиск значения по ключу.
        /// </summary>
        /// <param name="key"> Ключ. </param>
        /// <returns> Найденные по ключу хранимые данные. </returns>
        public object GetValueByKey(object key)
        {
            // Получаем хеш ключа.
            var hash = GetHash(key);

            // Если таблица не содержит такого хеша,
            // то завершаем выполнения метода вернув null.
            if (!_items.ContainsKey(hash))
            {
                return null;
            }

            return _items[hash].Value;
        }
        /// <summary>
        /// Хеш функция.
        /// </summary>
        /// <remarks>
        /// Возвращает длину строки.
        /// </remarks>
        /// <param name="value"> Хешируемая строка. </param>
        /// <returns> Хеш строки. </returns>
        private string GetHash(object value)
        {
            var data = value.ToString();
            byte[] hash;
            using (MD5 md5 = MD5.Create())
            {
                md5.Initialize();
                md5.ComputeHash(Encoding.UTF8.GetBytes(data));
                hash = md5.Hash;
            }
           
            return Encoding.UTF8.GetString(hash, 0, hash.Length); ;
        }
    }

    /// <summary>
    /// Элемент данных хеш таблицы.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Ключ.
        /// </summary>
        public object Key { get; private set; }

        /// <summary>
        /// Хранимые данные.
        /// </summary>
        public object Value { get; private set; }

        /// <summary>
        /// Создать новый экземпляр хранимых данных Item.
        /// </summary>
        /// <param name="key"> Ключ. </param>
        /// <param name="value"> Значение. </param>
        public Item(object key, object value)
        {
            // Устанавливаем значения.
            Key = key;
            Value = value;
        }        
    }
}

