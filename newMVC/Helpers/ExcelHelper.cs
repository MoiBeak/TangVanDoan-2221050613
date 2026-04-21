using ClosedXML.Excel;

namespace newMVC.Helpers
{
    public class ExcelHelper
    {
        public static List<T> ReadExcel<T>(
            Stream stream,
            Func<IXLRow, Dictionary<string, int>, T> mapFunc,
            Dictionary<string, string>? columnMapping = null)
        {
            var list = new List<T>();

            using (var workbook = new XLWorkbook(stream))
            {
                var ws = workbook.Worksheet(1);

                var headerRow = ws.Row(1);

                var columnMap = new Dictionary<string, int>();

                Console.WriteLine("=== HEADER FILE ===");

                foreach (var cell in headerRow.Cells())
                {
                    var rawHeader = cell.GetValue<string>();
                    var headerName = Normalize(rawHeader);

                    Console.WriteLine($"'{rawHeader}' -> '{headerName}'");

                    if (columnMapping != null)
                    {
                        var mapped = columnMapping
                            .FirstOrDefault(m => Normalize(m.Key) == headerName);

                        if (!string.IsNullOrEmpty(mapped.Value))
                        {
                            columnMap[mapped.Value] = cell.Address.ColumnNumber;
                        }
                    }
                }

                Console.WriteLine("=== COLUMN MAP ===");
                foreach (var c in columnMap)
                {
                    Console.WriteLine($"{c.Key} -> Column {c.Value}");
                }

                var rows = ws.RowsUsed().Skip(1);

                foreach (var row in rows)
                {
                    var item = mapFunc(row, columnMap);

                    if (item != null)
                        list.Add(item);
                }
            }

            return list;
        }

        private static string Normalize(string input)
        {
            return input?.Trim().ToLower().Replace(" ", "");
        }
    }
}
