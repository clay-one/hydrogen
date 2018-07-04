using System;
using System.Collections.Generic;
using hydrogen.General.Collections;
using hydrogen.General.Text;

namespace hydrogen.General.Validation
{
    public class ApiValidatedResult<T> : ApiValidationResult
	{
	    #region Properties

	    public T Result { get; set; }
	    
	    #endregion

	    #region Constructors

	    public ApiValidatedResult()
	    {
	    }

	    public ApiValidatedResult(T result)
	    {
	        Result = result;
	    }

	    public ApiValidatedResult(ApiValidationError error) : base(error)
	    {
	    }

	    public ApiValidatedResult(IEnumerable<ApiValidationError> errors) : base(errors)
	    {
	    }

        #endregion

	    #region Construction helpers

	    public new static ApiValidatedResult<T> Ok()
		{
			return new ApiValidatedResult<T>();
		}

	    public static ApiValidatedResult<T> Ok(T result)
		{
			return new ApiValidatedResult<T>(result);
		}

        public new static ApiValidatedResult<T> Failure()
        {
            return new ApiValidatedResult<T> {Success = false};
        }

	    public new static ApiValidatedResult<T> Failure(ApiValidationError error)
	    {
	        if (error == null)
	            throw new ArgumentNullException(nameof(error));

	        return new ApiValidatedResult<T>(error);
	    }

	    public new static ApiValidatedResult<T> Failure(IEnumerable<ApiValidationError> errors)
        {
            if (errors == null)
                throw new ArgumentNullException(nameof(errors));

            return new ApiValidatedResult<T>(errors);
        }

	    public new static ApiValidatedResult<T> Failure(string errorKey)
        {
            if (errorKey.IsNullOrWhitespace())
                throw new ArgumentNullException(nameof(errorKey));

            return Failure(new ApiValidationError(errorKey));
        }

        public new static ApiValidatedResult<T> Failure(string errorKey, IEnumerable<string> errorParameters)
        {
            if (errorKey.IsNullOrWhitespace())
                throw new ArgumentNullException(nameof(errorKey));

            return Failure(new ApiValidationError(errorKey, errorParameters));
        }

        public new static ApiValidatedResult<T> Failure(string propertyPath, string errorKey)
        {
            if (errorKey.IsNullOrWhitespace())
                throw new ArgumentNullException(nameof(errorKey));

            return Failure(new ApiValidationError(propertyPath, errorKey));
        }

        public new static ApiValidatedResult<T> Failure(string propertyPath, string errorKey, IEnumerable<string> errorParameters)
        {
            if (errorKey.IsNullOrWhitespace())
                throw new ArgumentNullException(nameof(errorKey));

            return Failure(new ApiValidationError(propertyPath, errorKey, errorParameters));
        }

	    public new static ApiValidatedResult<T> Aggregate(params ApiValidationError[] errors)
	    {
	        if (errors == null)
	            return Ok();

	        var result = new ApiValidatedResult<T>();
	        errors.SafeForEach(e => result.Append(e));

	        return result;
	    }

	    public new static ApiValidatedResult<T> Aggregate(params ApiValidationResult[] results)
	    {
	        if (results == null)
	            return Ok();

	        var result = new ApiValidatedResult<T>();
	        results.SafeForEach(r => result.Append(r));

	        return result;
	    }

        #endregion
    }
}