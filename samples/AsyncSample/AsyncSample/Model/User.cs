using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;

namespace AsyncSample.Model
{
	public class User:PropertyChangedHelper
	{
		#region FullNameProperty
		public static readonly PropertyChangedEventArgs FullNameArgs = PropertyChangedHelper.CreateArgs<User>(c => c.FullName);
		private string _FullName;

		public string FullName
		{
			get
			{
				return _FullName;
			}
			set
			{
				var oldValue = FullName;
				_FullName = value;
				if (oldValue != value)
				{
					OnFullNameChanged(oldValue, value);
					OnPropertyChanged(FullNameArgs);
				}
			}
		}

		protected virtual void OnFullNameChanged(string oldValue, string newValue)
		{
		}
		#endregion

		#region AddressProperty
		public static readonly PropertyChangedEventArgs AddressArgs = PropertyChangedHelper.CreateArgs<User>(c => c.Address);
		private string _Address;

		public string Address
		{
			get
			{
				return _Address;
			}
			set
			{
				var oldValue = Address;
				_Address = value;
				if (oldValue != value)
				{
					OnAddressChanged(oldValue, value);
					OnPropertyChanged(AddressArgs);
				}
			}
		}

		protected virtual void OnAddressChanged(string oldValue, string newValue)
		{
		}
		#endregion

		#region EmailProperty
		public static readonly PropertyChangedEventArgs EmailArgs = PropertyChangedHelper.CreateArgs<User>(c => c.Email);
		private string _Email;

		public string Email
		{
			get
			{
				return _Email;
			}
			set
			{
				var oldValue = Email;
				_Email = value;
				if (oldValue != value)
				{
					OnEmailChanged(oldValue, value);
					OnPropertyChanged(EmailArgs);
				}
			}
		}

		protected virtual void OnEmailChanged(string oldValue, string newValue)
		{
		}
		#endregion

		#region PhoneProperty
		public static readonly PropertyChangedEventArgs PhoneArgs = PropertyChangedHelper.CreateArgs<User>(c => c.Phone);
		private string _Phone;

		public string Phone
		{
			get
			{
				return _Phone;
			}
			set
			{
				var oldValue = Phone;
				_Phone = value;
				if (oldValue != value)
				{
					OnPhoneChanged(oldValue, value);
					OnPropertyChanged(PhoneArgs);
				}
			}
		}

		protected virtual void OnPhoneChanged(string oldValue, string newValue)
		{
		}
		#endregion
	}

	public class UsersProvider
	{
		public User CreateUser()
		{
			Thread.Sleep(1500);//Some serious business
			var user = new User();
			return user;
		}

		public void RemoveUser(User usr)
		{
			Thread.Sleep(1500);//And another one
		}
	}
}
