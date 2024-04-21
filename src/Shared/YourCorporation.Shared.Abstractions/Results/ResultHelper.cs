using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourCorporation.Shared.Abstractions.Results
{
    public static class ResultHelper
    {
        public static Result<IResult> AggregateErrors(params Result<Result>[] results)
        {
            var errors = new List<Error>();

            foreach (var result in results)
            {
                if (result.IsFailure)
                {
                    errors.AddRange(result.Errors);
                }
            }

            if (errors.Count > 0)
            {
                return errors;
            }

            return Result.Success();
        }
    }
}
