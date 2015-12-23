using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace AgilAds.Helpers
{
    public class stackFrame
    {
        public enum stackContext { businessInfoTeam, businessInfoContact, teamMemberContact, Rep, Member, Institution }
        private static Dictionary<stackContext, RouteValueDictionary> _routes =
            new Dictionary<stackContext, RouteValueDictionary>() { 
            { stackContext.Rep, new RouteValueDictionary(new { controller = "Rep", action = "Index" }) } ,
            { stackContext.Member, new RouteValueDictionary(new { controller = "Mem", action = "Index" }) } ,
            { stackContext.Institution, new RouteValueDictionary(new { controller = "Ins", action = "Index" }) } ,
            { stackContext.businessInfoTeam, new RouteValueDictionary(new { controller = "BIT", action = "Index" }) } ,
            { stackContext.businessInfoContact, new RouteValueDictionary(new { controller = "BIC", action = "Index" }) } ,
            { stackContext.teamMemberContact, new RouteValueDictionary(new { controller = "TMC", action = "Index" }) } 
            };
        private stackContext _context;
        private object _param;
        private string _callerId;
        public object param { get { return _param; } }
        public string callerId { get { return _callerId; } }
        public stackFrame(stackContext cntxt, object prm, string caller)
        {
            _context = cntxt;
            _param = prm;
            _callerId = caller;
        }
        public static void PushContext(stackFrame frame)
        {
            ((Stack<stackFrame>)HttpContext.Current.Session["stack"]).Push(frame);
        }
        public static stackFrame PopContext()
        {
            ((Stack<stackFrame>)HttpContext.Current.Session["stack"]).Pop();
            return ((Stack<stackFrame>)HttpContext.Current.Session["stack"]).Peek();
        }
        public static stackFrame PeekContext()
        {
            Stack<stackFrame> stack = HttpContext.Current.Session["stack"] as Stack<stackFrame>;
            if (stack == null) return null;
            return stack.Peek();
        }

        public static bool VerifyContext(object controller, stackContext expectedContext)
        {
            var pc = PeekContext();
            if (pc != null && pc._context.Equals(expectedContext)) return true;
            
            IStackable iam = controller as IStackable;
            if (iam == null)
            {
                InitStack(expectedContext, "root");
                return true;
            }
            return false;
        }

        public static RouteValueDictionary Invoke(stackContext context, object parameter, string callerId)
        {
            PushContext(new stackFrame(context, parameter, callerId));
            return _routes[PeekContext()._context];
        }

        public static RouteValueDictionary Backup()
        {
            return _routes[PopContext()._context];
        }

        public static void InitStack(stackContext initialState, string callerId)
        {
            HttpContext.Current.Session["stack"] = new Stack<stackFrame>();
            PushContext(new stackFrame(initialState, -1, callerId));
        }
    }
}