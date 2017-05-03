using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace AsyncSample.Model
{

	public abstract class PropertyChangedHelper : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		
		public void OnPropertyChanged(string name)
		{
			PropertyChangedEventArgs args = new PropertyChangedEventArgs(name);
			OnPropertyChanged(args);
		}

		public void OnPropertyChanged(PropertyChangedEventArgs args)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, args);
		}

		public static PropertyChangedEventArgs CreateArgs<T>(Expression<Func<T, Object>> propertyExpression)
		{
			return new PropertyChangedEventArgs(GetNameFromLambda(propertyExpression));
		}

		private static string GetNameFromLambda<T>(Expression<Func<T, object>> propertyExpression)
		{
			var expr = propertyExpression as LambdaExpression;
			MemberExpression member = expr.Body is UnaryExpression ? ((UnaryExpression)expr.Body).Operand as MemberExpression :  expr.Body as MemberExpression;
			var propertyInfo = member.Member as PropertyInfo;
			return propertyInfo.Name;
		}
	}

}
