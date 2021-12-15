using System; 
using System.Data;
using System.Collections.Generic; 
using MyPlatform.Common;
using MyPlatform.Model;
using MyPlatform.DALFactory;
using MyPlatform.IDAL;
namespace MyPlatform.BLL {
	 	//Sys_Dictionary
		public partial class Sys_Dictionary
	{
		private readonly ISys_Dictionary dal=DataAccess.CreateInstance<ISys_Dictionary>("Sys_Dictionary");
		public Sys_Dictionary()
		{}
		
	}
}