

using Microsoft.AspNetCore.Mvc;

namespace Common.Commons
{
    public static class JsonResponseStatus
    {
        public static JsonResult Success()
        {
            return new JsonResult(new { status = "Success" });
        }

        public static JsonResult Success(object returnData)
        {
            return new JsonResult(new { status = "Success", data = returnData });
        }
        public static JsonResult Done()
        {
            return new JsonResult(new { status = "Done" });
        }

        public static JsonResult Done(object returnData)
        {
            return new JsonResult(new { status = "Done", data = returnData });
        }

        public static JsonResult NotFound()
        {
            return new JsonResult(new { status = "NotFound" });
        }

        public static JsonResult NotFound(object returnData)
        {
            return new JsonResult(new { status = "NotFound", data = returnData });
        }

        public static JsonResult Error()
        {
            return new JsonResult(new { status = "Error" });
        }

        public static JsonResult Error(object returnData)
        {
            return new JsonResult(new { status = "Error", data = returnData });
        }
        public static JsonResult UnAuthorized()
        {
            return new JsonResult(new { status = "Error" });
        }

        public static JsonResult UnAuthorized(object returnData)
        {
            return new JsonResult(new { status = "UnAuthorized", data = returnData });
        }
        public static JsonResult NoAccess()
        {
            return new JsonResult(new { status = "NoAccess" });
        }

        public static JsonResult NoAccess(object returnData)
        {
            return new JsonResult(new { status = "NoAccess", data = returnData });
        }
        public static JsonResult NoAccessDelete()
        {
            return new JsonResult(new { status = "NoAccessDelete" });
        }

        public static JsonResult NoAccessDelete(object returnData)
        {
            return new JsonResult(new { status = "NoAccessDelete", data = returnData });
        }
    }
}
