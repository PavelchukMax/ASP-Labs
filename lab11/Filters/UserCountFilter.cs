namespace lab_11.Filters
{
    using lab_11.Controllers;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class UserCountAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.Controller as HomeController;
            if (controller != null)
            {
                controller.UpdateUserCount(); // Викликайте метод контролера, якщо він доступний
            }

            base.OnActionExecuting(context);
        }
    }
}
