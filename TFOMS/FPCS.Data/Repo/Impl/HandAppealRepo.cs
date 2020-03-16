using System;
using System.Linq;

using FPCS.Data.Entities;
using FPCS.Data.Exceptions;
using FPCS.Data.Enums;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using Npgsql;

namespace FPCS.Data.Repo.Impl
{
    internal class HandAppealRepo : RepoBase<HandAppeal, StudentManagementContext>, IHandAppealRepo
    {
        public HandAppealRepo(UnitOfWork<StudentManagementContext> unitOfWork) : base(unitOfWork) { }

        #region override methods

        public override IQueryable<HandAppeal> GetAll()
        {
            return DbSet.Where(x => !x.IsDeleted/* && x.IsShow*/);
        }

        #endregion override methods

        //public IQueryable<HandAppeal> GetByApplicantFIO(String surname, String name, String patronymicName)
        //{
        //    return GetAll().Where(x => x.ApplicantSurname.Trim().ToUpper() == surname && x.ApplicantName.Trim().ToUpper() == name && x.ApplicantSecondName.Trim().ToUpper() == patronymicName);
        //}

        //public IQueryable<HandAppeal> GetByReceivedTreatmentPersonFIO(Int32 uniqueNumberPart1, String surname, String name, String patronymicName)
        //{
        //    return GetAll().Where(x => x.ReceivedTreatmentPersonSurname.Trim().ToUpper() == surname && x.ReceivedTreatmentPersonName.Trim().ToUpper() == name 
        //                                && x.ReceivedTreatmentPersonSecondName.Trim().ToUpper() == patronymicName && x.UniqueNumberPart1 == uniqueNumberPart1);
        //}

        public Int32 GetMaxUniqueNumberPart1()
        {
            return GetMaxUniqueNumber() + 1;
        }

        //public Int32 GetMaxUniqueNumberPart2(Int32 uniqueNumberPart1)
        //{
        //    var data = GetAll().Where(x => x.UniqueNumberPart1 == uniqueNumberPart1).ToList();
        //    return data.Count == 0 ? 1 : data.Max(x => x.UniqueNumberPart2) + 1;
        //}

        //public Int32 GetMaxUniqueNumberPart3(Int32 uniqueNumberPart1, Int32 uniqueNumberPart2)
        //{
        //    var data = GetAll().Where(x => x.UniqueNumberPart1 == uniqueNumberPart1 && x.UniqueNumberPart2 == uniqueNumberPart2).ToList();
        //    return data.Count == 0 ? 1 : data.Max(x => x.UniqueNumberPart3) + 1;
        //}

        //public Boolean CheckUniqueNumberPart(Int32 uniqueNumberPart1)
        //{
        //    var data = GetAll().Where(x => x.UniqueNumberPart1 == uniqueNumberPart1).ToList();
        //    return data.Count == 0;
        //}

        //public Boolean CheckUniqueNumberPart(Int32 uniqueNumberPart1, Int32 uniqueNumberPart2)
        //{
        //    var data = GetAll().Where(x => x.UniqueNumberPart1 == uniqueNumberPart1 && x.UniqueNumberPart2 == uniqueNumberPart2).ToList();
        //    return data.Count == 0;
        //}

        //public Boolean CheckUniqueNumberPart(Int32 uniqueNumberPart1, Int32 uniqueNumberPart2, Int32 uniqueNumberPart3)
        //{
        //    var data = GetAll().Where(x => x.UniqueNumberPart1 == uniqueNumberPart1 && x.UniqueNumberPart2 == uniqueNumberPart2 && x.UniqueNumberPart3 == uniqueNumberPart3).ToList();
        //    return data.Count == 0;
        //}

        //public Boolean CheckUniqueNumberPart(params Int32[] uniqueNumberParts)
        //{
        //    List<HandAppeal> data = new List<HandAppeal>();

        //    switch(uniqueNumberParts.Count())
        //    {
        //        case 1:
        //            {
        //                Int32 uniqueNumberPart1 = uniqueNumberParts[0];

        //                data = GetAll().Where(x => x.UniqueNumberPart1 == uniqueNumberPart1).ToList();
        //                break;
        //            }
        //        case 2:
        //            {
        //                Int32 uniqueNumberPart1 = uniqueNumberParts[0];
        //                Int32 uniqueNumberPart2 = uniqueNumberParts[1];

        //                data = GetAll().Where(x => x.UniqueNumberPart1 == uniqueNumberPart1 && x.UniqueNumberPart2 == uniqueNumberPart2).ToList();
        //                break;
        //            }
        //        case 4:
        //            {
        //                Int32 uniqueNumberPart1 = uniqueNumberParts[0];
        //                Int32 uniqueNumberPart2 = uniqueNumberParts[1];
        //                Int32 uniqueNumberPart3 = uniqueNumberParts[2];
        //                Int32 excludeNumberPart3 = uniqueNumberParts[3];

        //                data = GetAll().Where(x => x.UniqueNumberPart1 == uniqueNumberPart1 && x.UniqueNumberPart2 == uniqueNumberPart2
        //                                        && x.UniqueNumberPart3 == uniqueNumberPart3 && x.UniqueNumberPart3 != excludeNumberPart3).ToList();
        //                break;
        //            }
        //    }

        //    return data.Count == 0;
        //}

        public Boolean CheckUniqueNumber(String uniqueNumber, String baseNumber = "")
        {
            List<HandAppeal> data = new List<HandAppeal>();
            data = GetAll().Where(x => x.AppealUniqueNumber == uniqueNumber && x.AppealUniqueNumber != baseNumber).ToList();
                     
            return data.Count == 0;
        }

        private Int32 GetMaxUniqueNumber()
        {
            string connStr = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string cmdText = @"SELECT max(unique_number_part_1)
                                FROM dbo.handappeal";
            //stream.WriteLine(cmdText);
            NpgsqlConnection conn = new NpgsqlConnection(connStr);
            NpgsqlCommand cmd = new NpgsqlCommand(cmdText, conn);
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                da.Fill(dt);
                if (dt != null && dt.Rows.Count > 0)
                    return Convert.ToInt32(dt.Rows[0][0]);
                else 
                    return 1;
            }
            catch (Exception e)
            {
                return 1;
            }
        }
    }
}
