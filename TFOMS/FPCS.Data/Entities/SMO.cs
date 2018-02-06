using System;
using FPCS.Data.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FPCS.Data.Entities
{
    public class SMO : BaseEntity
    {
        /// <summary>
        /// Id записи
        /// </summary>
        [Column("smoid")]
        [Required]
        public Int64 SmoId { get; set; }

        /// <summary>
        /// Код СМО
        /// </summary>
        [Column("smocode")]
        [MaxLength(10)]
        [Required]
        public String SmoCode { get; set; }

        /// <summary>
        /// КПП - код причины постановки на учет
        /// </summary>
        [Column("kpp")]
        [MaxLength(9)]
        [Required]
        public String KPP { get; set; }

        /// <summary>
        /// Полное наименование
        /// </summary>
        [Column("fullname")]
        [MaxLength(100)]
        [Required]
        public String FullName { get; set; }

        /// <summary>
        /// Краткое наименование
        /// </summary>
        [Column("shortname")]
        [MaxLength(100)]
        [Required]
        public String ShortName { get; set; }

        /// <summary>
        /// Фактисекий адрес
        /// </summary>
        [Column("factaddress")]
        [MaxLength(150)]
        [Required]
        public String FactAddress { get; set; }

        /// <summary>
        /// ФИО, № телефона, факса, адрес электронной почты руководителя
        /// </summary>
        [Column("director")]
        [MaxLength(150)]
        [Required]
        public String Director { get; set; }

        /// <summary>
        /// ФИО, № телефона, факса, адрес электронной почты руководителя обособленного подразделения (филиала)
        /// </summary>
        [Column("filialdirector")]
        [MaxLength(150)]
        public String FilialDirector { get; set; }

        /// <summary>
        /// Сведения о лицензии
        /// </summary>
        [Column("licenseinfo")]
        [MaxLength(100)]
        [Required]
        public String LicenseInfo { get; set; }

        /// <summary>
        /// Фамилия руководителя
        /// </summary>
        [Column("head_surname")]
        [MaxLength(150)]
        [Required]
        public String HeadSurname { get; set; }

        /// <summary>
        /// Имя руководителя
        /// </summary>
        [Column("head_name")]
        [MaxLength(150)]
        [Required]
        public String HeadName { get; set; }

        /// <summary>
        /// Отчетсво руководителя
        /// </summary>
        [Column("head_second_name")]
        [MaxLength(150)]
        [Required]
        public String HeadSecondName { get; set; }

        /// <summary>
        /// Должность руководителя
        /// </summary>
        [Column("head_position")]
        [MaxLength(150)]
        [Required]
        public String HeadPosition { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<JournalAppeal> JournalAppeals { get; set; }
    }
}
