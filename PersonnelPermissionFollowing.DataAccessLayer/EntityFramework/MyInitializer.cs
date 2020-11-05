using PersonnelPermissionFollowing.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelPermissionFollowing.DataAccessLayer.EntityFramework
{
    public class MyInitializer : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            // adding fake personel permission tips...
            //for (int k = 0; k < 5; k++)
            //{
            //    context.Personnels.Add(new Personnels()
            //    {
            //        Name = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 2)),
            //        CreatedOnDatetime = DateTime.Now,
            //        ModifiedOnDatetime = DateTime.Now,
            //        ModifiedUsername = "welatbaran"
            //    });
            //}
            //context.SaveChanges();
            //List<PersonnelDegrees> personnelDegreesList = context.PersonnelDegrees.ToList();
            //List<PersonnelTasks> personnelTasksList = context.PersonnelTasks.ToList();
            //List<PersonnelPositions> personnelPositionsList = context.PersonnelPositions.ToList();

            //for (int i = 0; i < 10; i++)
            //{
            //    //PersonnelDegrees pd = personnelDegreesList[FakeData.NumberData.GetNumber(0, personnelDegreesList.Count - 1)];
            //    //PersonnelTasks pt = personnelTasksList[FakeData.NumberData.GetNumber(0, personnelTasksList.Count - 1)];
            //    //PersonnelPositions pp = personnelPositionsList[FakeData.NumberData.GetNumber(0, personnelPositionsList.Count - 1)];

            //    // adding fake personel tasks ...{
            //    PersonnelTasks personnelTasks = new PersonnelTasks()
            //    {
            //        Name = FakeData.TextData.GetSentence(),
            //        CreatedOnDatetime = DateTime.Now,
            //        ModifiedOnDatetime = DateTime.Now,
            //        ModifiedUsername = "welatbaran"
            //    };
            //    context.PersonnelTasks.Add(personnelTasks);

            //    // adding fake personel degrees ...
            //    PersonnelDegrees personnelDegrees = new PersonnelDegrees()
            //    {
            //        Name = FakeData.TextData.GetSentence(),
            //        CreatedOnDatetime = DateTime.Now,
            //        ModifiedOnDatetime = DateTime.Now,
            //        ModifiedUsername = "welatbaran"
            //    };
            //    context.PersonnelDegrees.Add(personnelDegrees);

            //    // adding fake personel positions
            //    PersonnelPositions personnelPositions = new PersonnelPositions()
            //    {
            //        Name = FakeData.TextData.GetSentence(),
            //        CreatedOnDatetime = DateTime.Now,
            //        ModifiedOnDatetime = DateTime.Now,
            //        ModifiedUsername = "welatbaran"
            //    };
            //    context.PersonnelPositions.Add(personnelPositions);

            //    for (int j = 0; j < FakeData.NumberData.GetNumber(10, 20); j++)
            //    {
            //        Personnels personnels = new Personnels()
            //        {
            //            Name = FakeData.NameData.GetFirstName(),
            //            Surname = FakeData.NameData.GetSurname(),
            //            ProfilImage = "user.png",
            //            StartToJobDateTime = FakeData.DateTimeData.GetDatetime(DateTime.Now.AddYears(-50), DateTime.Now),
            //            Description = FakeData.TextData.GetSentences(FakeData.NumberData.GetNumber(1, 4)),
            //            PersonnelDegrees = personnelDegrees,
            //            PersonnelTasks = personnelTasks,
            //            PersonnelPositions = personnelPositions,
            //            CreatedOnDatetime = DateTime.Now,
            //            ModifiedOnDatetime = DateTime.Now,
            //            ModifiedUsername = "welatbaran"
            //        };
            //        context.Personnels.Add(personnels);
            //    }
            //}
            //context.SaveChanges();

        }

    }
}
