/* Author:      Jack McNally
 * Page Name:   INotification
 * Purpose:     notification service.
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace Wildfire.Services
{
    public interface INotification
    {
        void CreateNotification(String title, String message);
    }
}
