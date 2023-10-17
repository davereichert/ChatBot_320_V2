using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System;

public static class ScrollHelper
{
    public static void ScrollToBottom(ListView listView)
    {
        var scrollViewer = GetDescendantByType(listView, typeof(ScrollViewer)) as ScrollViewer;
        scrollViewer?.ScrollToEnd();
    }

    private static Visual GetDescendantByType(Visual element, Type type)
    {
        if (element == null) return null;
        if (element.GetType() == type) return element;
        Visual foundElement = null;
        if (element is FrameworkElement frameworkElement)
        {
            frameworkElement.ApplyTemplate();
        }
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
        {
            Visual visual = VisualTreeHelper.GetChild(element, i) as Visual;
            foundElement = GetDescendantByType(visual, type);
            if (foundElement != null)
                break;
        }
        return foundElement;
    }
}
