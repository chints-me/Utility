using Microsoft.AspNetCore.Mvc.Rendering;

namespace Utility.WebApp.Enums
{
    public class Enums
    {
    }

    public enum TaskStatusEnum
    {
        Pending,
        InProgress,
        Done
    }

    public static class TaskStatusEnumExtensions
    {
        private static string _pending = "Pending";
        private static string _inProgress = "InProgress";
        private static string _done = "Done";

        private static string _pendingTitle = "Pending";
        private static string _inProgressTitle = "In Progress";
        private static string _doneTitle = "Done";
        public static string ToText(this TaskStatusEnum status)
        {
            switch (status)
            {
                case TaskStatusEnum.Pending:
                    return _pendingTitle;
                case TaskStatusEnum.InProgress:
                    return _inProgressTitle;
                case TaskStatusEnum.Done:
                    return _doneTitle;
                default:
                    throw new Exception("Unknown status has been passed");
            }
        }

        public static string ToValue(this TaskStatusEnum status)
        {
            switch (status)
            {
                case TaskStatusEnum.Pending:
                    return _pending;
                case TaskStatusEnum.InProgress:
                    return _inProgress;
                case TaskStatusEnum.Done:
                    return _done;
                default:
                    throw new Exception("Unknown status has been passed");
            }
        }

        public static List<SelectListItem> GetTaskStatus()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = _pending, Text = _pendingTitle });
            list.Add(new SelectListItem { Value = _inProgress, Text = _inProgressTitle });
            list.Add(new SelectListItem { Value = _done, Text = _doneTitle });
            return list;
        }
    }
}
