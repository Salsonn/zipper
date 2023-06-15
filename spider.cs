namespace zipper {
    static class Spider // Contains methods for interacting with relevant parts of the host PC
    {
        public static string[] ls(string path, bool files = false, bool folders = false)
        {
            string[] list = new string[0];
            if (folders) {
                list = Directory.GetDirectories(path);
            }
            else if (files) {
                list = Directory.GetFiles(path);
            }
            for (int i = 0; i < list.Length; i++)
            {
                list[i] = list[i].Substring(path.Length + 1);
            }
            Console.Write("Items in " + path + " [files:" + files + "][folders:" + folders + "]");
            // DEBUGGING
            /*Console.WriteLine("");
            foreach (string item in list)
                Console.WriteLine(item);
            Console.Read(); */
            return list;
                // return Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);
        }
    }
}