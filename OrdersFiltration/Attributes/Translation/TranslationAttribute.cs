using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersFiltration.Attributes.Translation
{
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
	public class TranslationAttribute : Attribute
	{
		public string Translation { get; }
		public TranslationAttribute(string translation)
		{
			Translation = translation;
		}
	}
}
