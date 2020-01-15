using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace MyPlatform.Model
{
    //Sys_Tables
    public class Sys_Tables
    {

        /// <summary>
        /// ID
        /// </summary>		
        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// CreatedBy
        /// </summary>		
        private string _createdby;
        public string CreatedBy
        {
            get { return _createdby; }
            set { _createdby = value; }
        }

        /// <summary>
        /// CreatedDate
        /// </summary>		
        private DateTime _createddate;
        public DateTime CreatedDate
        {
            get
            {
                if (_createddate == DateTime.MinValue)
                {
                    return System.Data.SqlTypes.SqlDateTime.MinValue.Value;
                }
                else
                {
                    return _createddate;
                }
            }
            set
            {
                _createddate = value;
            }
        }

        /// <summary>
        /// UpdatedBy
        /// </summary>		
        private string _updatedby;
        public string UpdatedBy
        {
            get { return _updatedby; }
            set { _updatedby = value; }
        }

        /// <summary>
        /// UpdatedDate
        /// </summary>		
        private DateTime _updateddate;
        public DateTime UpdatedDate
        {
            get
            {
                if (_updateddate == DateTime.MinValue || _updateddate == null)
                {
                    return System.Data.SqlTypes.SqlDateTime.MinValue.Value;
                }
                else
                {
                    return _updateddate;
                }
            }
            set { _updateddate = value; }
        }

        /// <summary>
        /// Deleted
        /// </summary>		
        private int _deleted;
        public int Deleted
        {
            get { return _deleted; }
            set { _deleted = value; }
        }

        /// <summary>
        /// TableName
        /// </summary>		
        private string _tablename;
        public string TableName
        {
            get { return _tablename; }
            set { _tablename = value; }
        }

        /// <summary>
        /// TableName_EN
        /// </summary>		
        private string _tablename_en;
        public string TableName_EN
        {
            get { return _tablename_en; }
            set { _tablename_en = value; }
        }

        /// <summary>
        /// TableName_CN
        /// </summary>		
        private string _tablename_cn;
        public string TableName_CN
        {
            get { return _tablename_cn; }
            set { _tablename_cn = value; }
        }

        /// <summary>
        /// Remark
        /// </summary>		
        private string _remark;
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }

        private string _dbType;
        public string DBType
        {
            get
            {
                return _dbType;
            }

            set
            {
                _dbType = value;
            }
        }

        private string _dbName;
        public string DBName
        {
            get
            {
                return _dbName;
            }

            set
            {
                _dbName = value;
            }
        }
        private string _dbTypeCode;
        public string DBTypeCode
        {
            get
            {
                return _dbTypeCode;
            }

            set
            {
                _dbTypeCode = value;
            }
        }

    }
}