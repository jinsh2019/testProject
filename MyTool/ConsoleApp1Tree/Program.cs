using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Runtime;
namespace ConsoleApp1Tree
{
    public class UploadForAppModel
    {
        public string sessionId { get; set; }
        public string index { get; set; }
        public string base64FileString { get; set; }
        /// <summary>
        /// frontend filename
        /// </summary>
        public string name { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {

            var listUp = new List<UploadForAppModel>();
            listUp.Add(new UploadForAppModel()
            {
                name = "peter"
            });
            var peter = listUp[0];
            peter.index = "1";
            //listUp.ad(peter);

            var _UploadForAppModel = new UploadForAppModel(){};
            var extension = Path.GetExtension(_UploadForAppModel.name );
            var categories = new List<category>()
            {
                new category(1, "Sport", 0),
                new category(2, "Balls", 1),
                new category(3, "Shoes", 1),
                new category(4, "Electronics", 0),
                new category(5, "Cameras", 4),
                new category(6, "Lenses", 5),
                new category(7, "Tripod", 5),
                new category(8, "Computers", 4),
                new category(9, "Laptops", 8),
                new category(10, "Empty", 0),
                new category(-1, "Broken", 999),
            };
            var root = categories.GenerateTree(c => c.Id, c => c.ParentId);
            var test = JsonConvert.SerializeObject(root);
        }
    }

    class category
    {
        public int Id;
        public int ParentId;
        public string Name;

        public category(int id, string name, int pid)
        {
            Id = id;
            ParentId = pid;
            Name = name;
        }

        public List<category> Subcategories;
    }

    public class TreeItem<T>
    {
        public T Item { get; set; }
        public IEnumerable<TreeItem<T>> Children { get; set; }
    }


    internal static class GenericHelpers
    {
        /// <summary>
        /// Generates tree of items from item list
        /// </summary>
        /// 
        /// <typeparam name="T">Type of item in collection</typeparam>
        /// <typeparam name="K">Type of parent_id</typeparam>
        /// 
        /// <param name="collection">Collection of items</param>
        /// <param name="id_selector">Function extracting item's id</param>
        /// <param name="parent_id_selector">Function extracting item's parent_id</param>
        /// <param name="root_id">Root element id</param>
        /// 
        /// <returns>Tree of items</returns>
        public static IEnumerable<TreeItem<T>> GenerateTree<T, K>(
            this IEnumerable<T> collection,
            Func<T, K> id_selector,
            Func<T, K> parent_id_selector,
            K root_id = default(K))
        {
            foreach (var c in collection.Where(c => parent_id_selector(c).Equals(root_id)))
            {
                yield return new TreeItem<T>
                {
                    Item = c,
                    Children = collection.GenerateTree(id_selector, parent_id_selector, id_selector(c))
                };
            }
        }
    }

    public class FilesModel
    {

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("size")] public int Size { get; set; }

        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("url")] public string Url { get; set; }

        [JsonProperty("status")] public string Status { get; set; }

        [JsonProperty("fileName")] public string FileName { get; set; }
    }
}

