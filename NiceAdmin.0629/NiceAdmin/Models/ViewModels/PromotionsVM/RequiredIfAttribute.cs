using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

public class RequiredIfAttribute : ValidationAttribute
{
	private string[] dependentProperties;
	private object[] targetValues;

	public RequiredIfAttribute(string dependentProperty, params object[] targetValues)
	{
		this.dependentProperties = new[] { dependentProperty };
		this.targetValues = targetValues;
	}

	public RequiredIfAttribute(string[] dependentProperties, params object[] targetValues)
	{
		this.dependentProperties = dependentProperties;
		this.targetValues = targetValues;
	}

	protected override ValidationResult IsValid(object value, ValidationContext validationContext)
	{
		foreach (var dependentProperty in dependentProperties)
		{
			var dependentPropertyValue = GetDependentPropertyValue(dependentProperty, validationContext.ObjectInstance);
			foreach (var targetValue in targetValues)
			{
				if (Equals(dependentPropertyValue, targetValue) && IsValueNullOrEmpty(value))
				{
					return new ValidationResult(ErrorMessage);
				}
			}
		}

		return ValidationResult.Success;
	}

	private object GetDependentPropertyValue(string dependentProperty, object obj)
	{
		var propertyInfo = obj.GetType().GetProperty(dependentProperty);
		if (propertyInfo != null)
		{
			return propertyInfo.GetValue(obj);
		}
		throw new ArgumentException("Property with this name not found");
	}

	private bool IsValueNullOrEmpty(object value)
	{
		if (value == null)
		{
			return true;
		}

		if (value is string stringValue)
		{
			return string.IsNullOrWhiteSpace(stringValue);
		}

		return false;
	}
}
