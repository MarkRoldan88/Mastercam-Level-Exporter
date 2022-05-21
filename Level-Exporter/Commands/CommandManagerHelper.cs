// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandManagerHelper.cs">
//   Copyright (c) 2022
// </copyright>
// <summary>
//   This class contains methods for the CommandManager that help avoid memory leaks by using weak references.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;

namespace Level_Exporter.Commands
{
    /// <summary>
    /// This class allows delegating the commanding logic to methods passed as parameters,
    /// and enables a View to bind commands to objects that are not part of the element tree.
    /// </summary>
    public class DelegateCommand : ICommand
    {
        #region Fields

        /// <summary>
        /// Action Execute Method
        /// </summary>
        private readonly Action executeMethod;

        /// <summary>
        /// Can Execute Boolean
        /// </summary>
        private readonly Func<bool> canExecuteMethod;

        /// <summary>
        /// Boolean isAutomaticRequeryDisabled
        /// </summary>
        private bool isAutomaticRequeryDisabled;

        /// <summary>
        /// List of Weak References
        /// </summary>
        private List<WeakReference> canExecuteChangedHandlers;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the DelegateCommand class.
        /// </summary>
        /// <param name="executeMethod">Method to Execute</param>
        public DelegateCommand(Action executeMethod)
            : this(executeMethod, null, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the DelegateCommand class.
        /// </summary>
        /// <param name="executeMethod">Method to Execute</param>
        /// <param name="canExecuteMethod">Can Execute</param>
        public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod)
            : this(executeMethod, canExecuteMethod, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the DelegateCommand class.
        /// </summary>
        /// <param name="executeMethod">Method to Execute</param>
        /// <param name="canExecuteMethod">Can Execute</param>
        /// <param name="isAutomaticRequeryDisabled">Boolean Automatic Requery Disabled</param>
        public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod, bool isAutomaticRequeryDisabled)
        {
            this.executeMethod = executeMethod ?? throw new ArgumentNullException("executeMethod");
            this.canExecuteMethod = canExecuteMethod;
            this.isAutomaticRequeryDisabled = isAutomaticRequeryDisabled;
        }

        #endregion

        #region ICommand Events

        /// <summary>
        /// ICommand.CanExecuteChanged implementation
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (!this.isAutomaticRequeryDisabled)
                {
                    CommandManager.RequerySuggested += value;
                }

                CommandManagerHelper.AddWeakReferenceHandler(ref this.canExecuteChangedHandlers, value, 2);
            }

            remove
            {
                if (!this.isAutomaticRequeryDisabled)
                {
                    CommandManager.RequerySuggested -= value;
                }

                CommandManagerHelper.RemoveWeakReferenceHandler(this.canExecuteChangedHandlers, value);
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether the Property to enable or disable CommandManager's automatic requery on this command
        /// </summary>
        public bool IsAutomaticRequeryDisabled
        {
            get => this.isAutomaticRequeryDisabled;

            set
            {
                if (this.isAutomaticRequeryDisabled != value)
                {
                    if (value)
                    {
                        CommandManagerHelper.RemoveHandlersFromRequerySuggested(this.canExecuteChangedHandlers);
                    }
                    else
                    {
                        CommandManagerHelper.AddHandlersToRequerySuggested(this.canExecuteChangedHandlers);
                    }

                    this.isAutomaticRequeryDisabled = value;
                }
            }
        }

        #endregion

        #region ICommand Methods

        /// <summary>
        /// CanExecute ICommand
        /// </summary>
        /// <param name="parameter">Param not used</param>
        /// <returns>True to execute</returns>
        bool ICommand.CanExecute(object parameter) => this.CanExecute();

        /// <summary>
        /// Execute Method
        /// </summary>
        /// <param name="parameter">Param not used</param>
        void ICommand.Execute(object parameter) => this.Execute();

        #endregion

        #region Public Methods

        /// <summary>
        /// Method to determine if the command can be executed
        /// </summary>
        /// <returns>Boolean can execute</returns>
        public bool CanExecute()
        {
            if (this.canExecuteMethod != null)
            {
                return this.canExecuteMethod();
            }

            return true;
        }

        /// <summary>
        /// Execution of the command
        /// </summary>
        public void Execute() => this.executeMethod?.Invoke();

        /// <summary>
        /// Raises the CanExecuteChanged event
        /// </summary>
        public void RaiseCanExecuteChanged() => this.OnCanExecuteChanged();

        #endregion

        #region Protected Methods

        /// <summary>
        /// Protected virtual method to raise CanExecuteChanged event
        /// </summary>
        protected virtual void OnCanExecuteChanged()
            => CommandManagerHelper.CallWeakReferenceHandlers(this.canExecuteChangedHandlers);

        #endregion
    }

    /// <summary>
    ///     This class allows delegating the commanding logic to methods passed as parameters,
    ///     and enables a View to bind commands to objects that are not part of the element tree.
    /// </summary>
    /// <typeparam name="T">Type of the parameter passed to the delegates</typeparam>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed. Suppression is OK here.")]
    public class DelegateCommand<T> : ICommand
    {
        #region Data

        /// <summary>
        /// Action Execute Template Method
        /// </summary>
        private readonly Action<T> executeMethod;

        /// <summary>
        /// Can Execute Template
        /// </summary>
        private readonly Func<T, bool> canExecuteMethod;

        /// <summary>
        /// Boolean isAutomaticRequeryDisabled
        /// </summary>
        private bool isAutomaticRequeryDisabled;

        /// <summary>
        /// List of Weak References
        /// </summary>
        private List<WeakReference> canExecuteChangedHandlers;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the DelegateCommand class.
        /// </summary>
        /// <param name="executeMethod">Template execute method</param>
        public DelegateCommand(Action<T> executeMethod)
            : this(executeMethod, null, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the DelegateCommand class.
        /// </summary>
        /// <param name="executeMethod">Template execute Method</param>
        /// <param name="canExecuteMethod">Template can Execute Method</param>
        public DelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
            : this(executeMethod, canExecuteMethod, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the DelegateCommand class.
        /// </summary>
        /// <param name="executeMethod">Template execute Method</param>
        /// <param name="canExecuteMethod">Template can Execute Method</param>
        /// <param name="isAutomaticRequeryDisabled">Boolean Automatic Requery Disabled</param>
        public DelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod, bool isAutomaticRequeryDisabled)
        {
            this.executeMethod = executeMethod ?? throw new ArgumentNullException("executeMethod");
            this.canExecuteMethod = canExecuteMethod;
            this.isAutomaticRequeryDisabled = isAutomaticRequeryDisabled;
        }

        #endregion

        #region ICommand Event

        /// <summary>
        /// ICommand.CanExecuteChanged implementation
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (!this.isAutomaticRequeryDisabled)
                {
                    CommandManager.RequerySuggested += value;
                }

                CommandManagerHelper.AddWeakReferenceHandler(ref this.canExecuteChangedHandlers, value, 2);
            }

            remove
            {
                if (!this.isAutomaticRequeryDisabled)
                {
                    CommandManager.RequerySuggested -= value;
                }

                CommandManagerHelper.RemoveWeakReferenceHandler(this.canExecuteChangedHandlers, value);
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether the Property to enable or disable CommandManager's automatic requery on this command
        /// </summary>
        public bool IsAutomaticRequeryDisabled
        {
            get => this.isAutomaticRequeryDisabled;

            set
            {
                if (this.isAutomaticRequeryDisabled != value)
                {
                    if (value)
                    {
                        CommandManagerHelper.RemoveHandlersFromRequerySuggested(this.canExecuteChangedHandlers);
                    }
                    else
                    {
                        CommandManagerHelper.AddHandlersToRequerySuggested(this.canExecuteChangedHandlers);
                    }

                    this.isAutomaticRequeryDisabled = value;
                }
            }
        }

        #endregion

        #region ICommand Methods

        /// <summary>
        /// ICommand.CanExecute implementation
        /// </summary>
        /// <param name="parameter">Object to test</param>
        /// <returns>Boolean can execute</returns>
        bool ICommand.CanExecute(object parameter)
        {
            // If T is a value type then the parameter cannot be null
            // and must be of the same type. In case of reference types
            // we will just make the parameter null if it doesn't match
            // the type expected by the command.
            // This is done because for some reason the Actipro controls
            // inject a CheckableCommandParameter object before the command
            // bindings are fully realized. Since those are reference types,
            // we cannot use them for commands that expect value types.
            var type = typeof(T);
            if (type.IsValueType && !(type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>)))
            {
                if (!(parameter is T))
                {
                    return false;
                }
            }
            else if (!(parameter is T))
            {
                parameter = null;
            }

            return this.CanExecute((T)parameter);
        }

        /// <summary>
        /// ICommand.Execute implementation
        /// </summary>
        /// <param name="parameter">Object to test</param>
        void ICommand.Execute(object parameter)
        {
            if (!(parameter is T))
            {
                // Someone has wired up the XAML wrong, or is overriding it (I'm looking at you Actipro),
                // so just assume this is null.
                parameter = null;
            }

            this.Execute((T)parameter);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Method to determine if the command can be executed
        /// </summary>
        /// <param name="parameter">Template param</param>
        /// <returns>Boolean can execute</returns>
        public bool CanExecute(T parameter)
        {
            if (this.canExecuteMethod != null)
            {
                return this.canExecuteMethod(parameter);
            }

            return true;
        }

        /// <summary>
        /// Execution of the command
        /// </summary>
        /// <param name="parameter">Template param</param>
        public void Execute(T parameter)
        {
            this.executeMethod?.Invoke(parameter);
        }

        /// <summary>
        /// Raises the CanExecuteChanged event
        /// </summary>
        public void RaiseCanExecuteChanged() => this.OnCanExecuteChanged();

        #endregion

        #region Protected Methods

        /// <summary>
        /// Protected virtual method to raise CanExecuteChanged event
        /// </summary>
        protected virtual void OnCanExecuteChanged()
            => CommandManagerHelper.CallWeakReferenceHandlers(this.canExecuteChangedHandlers);

        #endregion
    }

    /// <summary>
    /// This class contains methods for the CommandManager that help avoid memory leaks by using weak references.
    /// </summary>
    internal static class CommandManagerHelper
    {
        /// <summary>
        /// AddHandlersToRequerySuggested Method
        /// </summary>
        /// <param name="handlers">List of WeakReference Handlers</param>
        internal static void AddHandlersToRequerySuggested(List<WeakReference> handlers)
        {
            if (handlers != null)
            {
                foreach (var handlerRef in handlers)
                {
                    if (handlerRef.Target is EventHandler handler)
                    {
                        CommandManager.RequerySuggested += handler;
                    }
                }
            }
        }

        /// <summary>
        /// AddWeakReferenceHandler Method
        /// </summary>
        /// <param name="handlers">List of WeakReference Handlers</param>
        /// <param name="handler">WeekReference Handler</param>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Currently unused, but useful.")]
        internal static void AddWeakReferenceHandler(ref List<WeakReference> handlers, EventHandler handler)
        => AddWeakReferenceHandler(ref handlers, handler, -1);

        /// <summary>
        /// AddWeakReferenceHandler Method
        /// </summary>
        /// <param name="handlers">List of WeakReference Handlers</param>
        /// <param name="handler">WeekReference Handler</param>
        /// <param name="defaultListSize">Default size of the List</param>
        internal static void AddWeakReferenceHandler(ref List<WeakReference> handlers, EventHandler handler, int defaultListSize)
        {
            if (handlers == null)
            {
                handlers = defaultListSize > 0 ? new List<WeakReference>(defaultListSize) : new List<WeakReference>();
            }

            handlers.Add(new WeakReference(handler));
        }

        /// <summary>
        /// CallWeakReferenceHandlers Methods
        /// </summary>
        /// <param name="handlers">List of WeakReference Handlers</param>
        internal static void CallWeakReferenceHandlers(List<WeakReference> handlers)
        {
            if (handlers != null)
            {
                // Take a snapshot of the handlers before we call out to them since the handlers
                // could cause the array to me modified while we are reading it.
                var callees = new EventHandler[handlers.Count];
                var count = 0;

                for (var i = handlers.Count - 1; i >= 0; i--)
                {
                    var reference = handlers[i];
                    if (!(reference.Target is EventHandler handler))
                    {
                        // Clean up old handlers that have been collected
                        handlers.RemoveAt(i);
                    }
                    else
                    {
                        callees[count] = handler;
                        count++;
                    }
                }

                // Call the handlers that we snapshotted
                for (var i = 0; i < count; i++)
                {
                    var handler = callees[i];
                    handler(null, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// RemoveHandlersFromRequerySuggested Method
        /// </summary>
        /// <param name="handlers">List of WeakReference Handlers</param>
        internal static void RemoveHandlersFromRequerySuggested(List<WeakReference> handlers)
        {
            if (handlers != null)
            {
                foreach (var handlerRef in handlers)
                {
                    if (handlerRef.Target is EventHandler handler)
                    {
                        CommandManager.RequerySuggested -= handler;
                    }
                }
            }
        }

        /// <summary>
        /// RemoveWeakReferenceHandler Method
        /// </summary>
        /// <param name="handlers">List of WeakReference Handlers</param>
        /// <param name="handler">WeekReference Handler</param>
        internal static void RemoveWeakReferenceHandler(List<WeakReference> handlers, EventHandler handler)
        {
            if (handlers != null)
            {
                for (var i = handlers.Count - 1; i >= 0; i--)
                {
                    var reference = handlers[i];
                    if (!(reference.Target is EventHandler existingHandler) || existingHandler == handler)
                    {
                        // Clean up old handlers that have been collected
                        // in addition to the handler that is to be removed.
                        handlers.RemoveAt(i);
                    }
                }
            }
        }
    }
}
