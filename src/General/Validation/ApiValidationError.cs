using System.Collections.Generic;
using Hydrogen.General.Collections;

namespace Hydrogen.General.Validation
{
	public class ApiValidationError
	{
		#region Initialization

		public ApiValidationError()
		{
		}

		public ApiValidationError(string errorKey)
		{
			ErrorKey = errorKey;
		}

		public ApiValidationError(string propertyPath, string errorKey)
		{
			PropertyPath = propertyPath;
			ErrorKey = errorKey;
		}

		public ApiValidationError(string errorKey, IEnumerable<string> errorParameters)
		{
			ErrorKey = errorKey;
			ErrorParameters = errorParameters.SafeToList();
		}

		public ApiValidationError(string propertyPath, string errorKey, IEnumerable<string> errorParameters)
		{
			PropertyPath = propertyPath;
			ErrorKey = errorKey;
			ErrorParameters = errorParameters.SafeToList();
		}

		#endregion

		public string PropertyPath { get; set; }
		public string ErrorKey { get; set; }
		public List<string> ErrorParameters { get; set; }
        public string LocalizedMessage { get; set; }
	}
}