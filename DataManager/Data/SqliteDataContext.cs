using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataManager.Data
{
    public class SqliteDataContext : SQLDatacontext
    {
        private string dataPath { get; }

        public SqliteDataContext(IConfiguration configuration) : base(configuration)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            dataPath = System.IO.Path.Join(path, "TaxCalcLocal.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={dataPath}");
        }
    }
}