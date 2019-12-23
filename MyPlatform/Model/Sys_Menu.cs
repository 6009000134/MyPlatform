using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace MyPlatform.Model{
	 	//Sys_Menu
	public class Sys_Menu
	{
   		     
      	/// <summary>
		/// ID
        /// </summary>		
		private int _id;
				public int ID
        {   
            get{ return _id; }
            set{ _id =value; }
        } 
		       
		/// <summary>
		/// CreatedBy
        /// </summary>		
		private string _createdby;
				public string CreatedBy
        {   
            get{ return _createdby; }
            set{ _createdby =value; }
        } 
		       
		/// <summary>
		/// CreatedDate
        /// </summary>		
		private DateTime _createddate;
		public DateTime CreatedDate
        {   
            get
            {
                if (_createddate == DateTime.MinValue || _createddate  == null)
                {
                    return System.Data.SqlTypes.SqlDateTime.MinValue.Value;
                }
                else
                {
                    return _createddate;
                }
            }
            set{ _createddate =value; }
        } 
				       
		/// <summary>
		/// UpdatedBy
        /// </summary>		
		private string _updatedby;
				public string UpdatedBy
        {   
            get{ return _updatedby; }
            set{ _updatedby =value; }
        } 
		       
		/// <summary>
		/// UpdatedDate
        /// </summary>		
		private DateTime _updateddate;
		public DateTime UpdatedDate
        {   
            get
            {
                if (_updateddate == DateTime.MinValue || _updateddate  == null)
                {
                    return System.Data.SqlTypes.SqlDateTime.MinValue.Value;
                }
                else
                {
                    return _updateddate;
                }
            }
            set{ _updateddate =value; }
        } 
				       
		/// <summary>
		/// Deleted
        /// </summary>		
		private int _deleted;
				public int Deleted
        {   
            get{ return _deleted; }
            set{ _deleted =value; }
        } 
		       
		/// <summary>
		/// MenuName
        /// </summary>		
		private string _menuname;
				public string MenuName
        {   
            get{ return _menuname; }
            set{ _menuname =value; }
        } 
		       
		/// <summary>
		/// Uri
        /// </summary>		
		private string _uri;
				public string Uri
        {   
            get{ return _uri; }
            set{ _uri =value; }
        } 
		       
		   
	}
}