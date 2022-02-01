using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApi.Helper
{
    public class TimeHelper
    {
        public string CalculateTime(DateTime something)
        {
            int a = (int)DateTime.Now.Subtract(something).TotalDays;
            string tt = null;
            if (a > 365)
            {
                if (a >= 365 && a < 730)
                {
                    tt = " 1 year ago";
                }
                else
                {
                    tt = a / 365 + " years ago";
                }
            }
            else if (a < 365 && a > 0)
            {
                if (a == 1)
                {
                    tt = " 1 day ago";
                }
                else
                {
                    tt = a + " days ago";
                }
            }
            else
            {
                a = (int)DateTime.Now.Subtract(something).TotalHours;
                if (a > 1)
                {
                    tt = a + " hours ago";
                }
                else if (a == 1)
                {
                    tt = a + " hour ago";
                }
                else
                {
                    a = (int)DateTime.Now.Subtract(something).TotalMinutes;
                    if (a > 1)
                    {
                        tt = a + " minutes ago";
                    }
                    else if (a == 1)
                    {
                        tt = a + " minute ago";
                    }
                    else
                    {
                        tt = " less than a minute ago";
                    }
                }
            }
        return tt;

        }
    }
}
