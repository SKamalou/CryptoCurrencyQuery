using System.Globalization;
using CryptoCurrencyQuery.Application.Common.Interfaces;
using CryptoCurrencyQuery.Application.TodoLists.Queries.ExportTodos;
using CryptoCurrencyQuery.Infrastructure.Files.Maps;
using CsvHelper;

namespace CryptoCurrencyQuery.Infrastructure.Files;

public class CsvFileBuilder : ICsvFileBuilder
{
    public byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records)
    {
        using var memoryStream = new MemoryStream();
        using (var streamWriter = new StreamWriter(memoryStream))
        {
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

            csvWriter.Context.RegisterClassMap<TodoItemRecordMap>();
            csvWriter.WriteRecords(records);
        }

        return memoryStream.ToArray();
    }
}
