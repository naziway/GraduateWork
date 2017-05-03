using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using AsyncSample.Misc;
using AsyncSample.Model;

namespace AsyncSample
{
	public enum StateEnum
	{
		Busy, Idle
	}

	class MainWindowController:PropertyChangedHelper
	{
		UsersProvider _provider = new UsersProvider();

		public MainWindowController()
		{
			if (!WPFHelper.IsInDesignMode)
			{
				var tsk = Task.Factory.StartNew(InitialStart);
				tsk.ContinueWith(t => { MessageBox.Show(t.Exception.InnerException.Message); }, CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.FromCurrentSynchronizationContext());
			}
		}

		#region SelectedUserProperty
		public static readonly PropertyChangedEventArgs SelectedUserArgs = PropertyChangedHelper.CreateArgs<MainWindowController>(c => c.SelectedUser);
		private User _SelectedUser;

		public User SelectedUser
		{
			get
			{
				return _SelectedUser;
			}
			set
			{
				var oldValue = SelectedUser;
				_SelectedUser = value;
				if (oldValue != value)
				{
					OnSelectedUserChanged(oldValue, value);
					OnPropertyChanged(SelectedUserArgs);
				}
			}
		}

		protected virtual void OnSelectedUserChanged(User oldValue, User newValue)
		{
		}
		#endregion


		#region UsersProperty
		public static readonly PropertyChangedEventArgs UsersArgs = PropertyChangedHelper.CreateArgs<MainWindowController>(c => c.Users);
		private DispatcherCollection<User> _Users;

		public DispatcherCollection<User> Users
		{
			get
			{
				if (_Users == null)
					DispatcherCollection<User>.Create(ref _Users);
				return _Users;
			}
		}

		protected virtual void OnUsersChanged(DispatcherCollection<User> oldValue, DispatcherCollection<User> newValue)
		{
		}
		#endregion


		void InitialStart()
		{
			try
			{
				State = StateEnum.Busy;
				User user = _provider.CreateUser();
				user.FullName = "John Smith";
				user.Address = "Some address goes here";
				user.Email = "unconsious@mail.com";
				Users.Add(user);

				user = _provider.CreateUser();
				user.FullName = "Jane Smith";
				user.Address = "Same of John's";
				user.Email = "family_crash@mail.com";
				Users.Add(user);
			}
			finally
			{
				State = StateEnum.Idle;
			}
		}

		#region StateProperty
		public static readonly PropertyChangedEventArgs StateArgs = PropertyChangedHelper.CreateArgs<MainWindowController>(c => c.State);
		private StateEnum _State;

		public StateEnum State
		{
			get
			{
				return _State;
			}
			set
			{
				var oldValue = State;
				_State = value;
				if (oldValue != value)
				{
					OnStateChanged(oldValue, value);
					OnPropertyChanged(StateArgs);
				}
			}
		}

		protected virtual void OnStateChanged(StateEnum oldValue, StateEnum newValue)
		{
		}
		#endregion

		private ActionCommand _AddUserCommand;
		public ICommand AddUserCommand
		{
			get
			{
				if (_AddUserCommand == null)
					_AddUserCommand = new ActionCommand(OnAddUserExecute, OnAddUserCanExecute);
				return _AddUserCommand;
			}
		}

		protected virtual bool OnAddUserCanExecute()
		{
			return State == StateEnum.Idle;
		}

		protected virtual  void OnAddUserExecute()
		{
			Task tsk = Task.Factory.StartNew(()=>
			                                 	{
			                                 		try
			                                 		{
			                                 			State = StateEnum.Busy;
			                                 			var usr = _provider.CreateUser();
			                                 			usr.FullName = "new user";
														Users.Add(usr);
			                                 			SelectedUser = usr;
			                                 		}
			                                 		finally
			                                 		{
			                                 			State = StateEnum.Idle;
			                                 		}
			                                 	});

			tsk.ContinueWith(t => { MessageBox.Show(t.Exception.InnerException.Message); }, CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.FromCurrentSynchronizationContext());
		}

		private ActionCommand _RemoveUserCommand;
		public ICommand RemoveUserCommand
		{
			get
			{
				if (_RemoveUserCommand == null)
					_RemoveUserCommand = new ActionCommand(OnRemoveUserCommandExecute, OnRemoveUserCommandCanExecute);
				return _RemoveUserCommand;
			}
		}

		protected virtual bool OnRemoveUserCommandCanExecute()
		{
			return State == StateEnum.Idle && SelectedUser!=null;
		}

		protected virtual void OnRemoveUserCommandExecute()
		{
			Task tsk = Task.Factory.StartNew(() =>
			{
				try
				{
					State = StateEnum.Busy;
					Users.Remove(SelectedUser);
					_provider.RemoveUser(SelectedUser);
					SelectedUser = Users.FirstOrDefault();
				}
				finally
				{
					State = StateEnum.Idle;
				}
			});

			tsk.ContinueWith(t => { MessageBox.Show(t.Exception.InnerException.Message); }, CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.FromCurrentSynchronizationContext());
		}
	}
}
