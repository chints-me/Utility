using Newtonsoft.Json;
using Utility.WebApp.Models;

namespace Utility.WebApp.Helpers
{
    public static class UtilityHelper
    {
        #region JSON Response
        public static string ResponseAsJsonString(ResponseViewModel viewModel)
        {
            if (viewModel.IsSuccess == false && string.IsNullOrWhiteSpace(viewModel.Message))
                viewModel.Message = "Enter required fields";

            if (string.IsNullOrWhiteSpace(viewModel.ValidationErrors))
                viewModel.ValidationErrors = "";

            if (string.IsNullOrWhiteSpace(viewModel.RedirectUrl))
            {
                viewModel.RedirectUrl = "";
                viewModel.IsRedirectionRequired = false;
            }
            else
            {
                viewModel.IsRedirectionRequired = true;
            }

            return JsonConvert.SerializeObject(viewModel);

        }

        #endregion

        #region Guid 

        public static Guid TryParseGuidId(this string Id)
        {
            if (string.IsNullOrWhiteSpace(Id))
                return Guid.Empty;
            else
            {
                //Guid.TryParse(Utility.DecryptString(Id.ToUrlDecodeBase64()), out Guid guid);
                Guid.TryParse(Id, out Guid guid);
                return guid;
            }
        }

        public static bool IsNullOrEmpty(this Guid? guid)
        {
            if (guid == null)
                return true;

            if (guid == Guid.Empty)
                return true;

            return false;
        }

        #endregion


    }
}
