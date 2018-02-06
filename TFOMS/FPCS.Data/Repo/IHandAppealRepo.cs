using System;

using FPCS.Data.Entities;
using System.Linq;
using FPCS.Data.Enums;

namespace FPCS.Data.Repo
{
    public interface IHandAppealRepo : IRepoBase<HandAppeal>
    {
        //IQueryable<HandAppeal> GetByApplicantFIO(String surname, String name, String patronymicName);

        //IQueryable<HandAppeal> GetByReceivedTreatmentPersonFIO(Int32 uniqueNumberPart1, String surname, String name, String patronymicName);

        Int32 GetMaxUniqueNumberPart1();

        //Int32 GetMaxUniqueNumberPart2(Int32 uniqueNumberPart1);

        //Int32 GetMaxUniqueNumberPart3(Int32 uniqueNumberPart1, Int32 uniqueNumberPart2);

        //Boolean CheckUniqueNumberPart(Int32 uniqueNumberPart1);

        //Boolean CheckUniqueNumberPart(Int32 uniqueNumberPart1, Int32 uniqueNumberPart2);

        //Boolean CheckUniqueNumberPart(Int32 uniqueNumberPart1, Int32 uniqueNumberPart2, Int32 uniqueNumberPart3);

        //Boolean CheckUniqueNumberPart(params Int32[] uniqueNumberParts);

        Boolean CheckUniqueNumber(String uniqueNumber, String baseNumber = "");
    }
}
