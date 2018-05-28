using System.Data.Entity;
using Presentation.DAL.EF;

namespace DAL
{
   public class DbInitializer : DropCreateDatabaseAlways<DatabaseContext>
    {
        protected override void Seed(DatabaseContext db)
        {
            var TestInfo =
                new[]
                {
                    new SomeInfo() { Name = "Нормативно-справочная информация"},
                    new SomeInfo() { Name = "Шаблоны документов"},
                    new SomeInfo() { Name = "Руководство оператора"}
                };
            db.SomeInfos.AddRange(TestInfo);
        }
    }
}