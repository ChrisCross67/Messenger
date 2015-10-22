﻿using System.Windows;
using System.Windows.Media;

namespace Messenger.Extensions
{
    public static class DependencyObjectExtentions
    {
        /// <summary>
        /// Tries to locate a given item within the visual tree,
        /// starting with the dependency object at a given position.
        /// </summary>
        /// <typeparam name="T">The type of the element to be found
        /// on the visual tree of the element at the given location.</typeparam>
        /// <param name="reference">The main element which is used to perform
        /// hit testing.</param>
        /// <param name="point">The position to be evaluated on the origin.</param>
        public static T TryFindFromPoint<T>(this UIElement reference, Point point)
          where T : DependencyObject
        {
            DependencyObject element = reference.InputHitTest(point)
                                         as DependencyObject;
            if (element == null)
                return null;
            if (element is T)
                return (T)element;
            return element.TryFindParent<T>();
        }

        /// <summary>
        /// Finds a parent of a given item on the visual tree.
        /// </summary>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="child">A direct or indirect child of the
        /// queried item.</param>
        /// <returns>The first parent item that matches the submitted
        /// type parameter. If not matching item can be found, a null
        /// reference is being returned.</returns>
        public static T TryFindParent<T>(this DependencyObject child)
          where T : DependencyObject
        {
            //get parent item
            DependencyObject parentObject = GetParentObject(child);

            //we've reached the end of the tree
            if (parentObject == null) return null;

            //check if the parent matches the type we're looking for
            var parent = parentObject as T;
            return parent ?? TryFindParent<T>(parentObject);
        }

        /// <summary>
        /// This method is an alternative to WPF's
        /// <see cref="VisualTreeHelper.GetParent"/> method, which also
        /// supports content elements. Do note, that for content element,
        /// this method falls back to the logical tree of the element!
        /// </summary>
        /// <param name="child">The item to be processed.</param>
        /// <returns>The submitted item's parent, if available. Otherwise
        /// null.</returns>
        public static DependencyObject GetParentObject(this DependencyObject child)
        {
            if (child == null) return null;
            ContentElement contentElement = child as ContentElement;

            if (contentElement == null)
                return VisualTreeHelper.GetParent(child);

            DependencyObject parent = ContentOperations.GetParent(contentElement);
            if (parent != null) return parent;

            FrameworkContentElement fce = contentElement as FrameworkContentElement;
            return fce != null ? fce.Parent : null;
        }
    }
}
